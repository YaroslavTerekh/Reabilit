import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { config } from '../../config';
import { ReplaySubject } from 'rxjs';
import { MessageNotificationDTO } from '../responseModels/MessageNotificationDTO';
import { ChatMessageDTO } from '../responseModels/ChatMessageDTO';
import { ProcedureEventDTO } from '../responseModels/ProcedureEventDTO';
import { TreatmentNotificationDTO } from '../responseModels/TreatmentNotificationDTO';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private readonly baseUrl: string = config.apiUrl;
  public $receiveChatMessageSubject: ReplaySubject<ChatMessageDTO> = new ReplaySubject<ChatMessageDTO>();
  public $receiveProcedureEventSubject: ReplaySubject<ProcedureEventDTO> = new ReplaySubject<ProcedureEventDTO>();
  public $receiveTreatmentEventSubject: ReplaySubject<TreatmentNotificationDTO> = new ReplaySubject<TreatmentNotificationDTO>();

  public $newNotificationsCount: ReplaySubject<number> = new ReplaySubject<number>(1);
  private lastNotificationCount: number = 0;
  
  constructor(){
    this.$newNotificationsCount.subscribe({ next: res => this.lastNotificationCount = res })
  }

  private hubConnection!: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`https://localhost:7269/notifications?token=${localStorage.getItem('token')}`)
      .withAutomaticReconnect()
      .build();
    this.hubConnection.serverTimeoutInMilliseconds = 100000;
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {

    this.hubConnection.on('CreateAndSendMessageNotificationAsync', (data: any) => {
      this.$receiveChatMessageSubject.next(data);
      this.$newNotificationsCount.next(++this.lastNotificationCount);
    });

    this.hubConnection.on('CreateAndSendEventNotificationAsync', (data: any) => {
      this.$receiveProcedureEventSubject.next(data);    
      this.$newNotificationsCount.next(++this.lastNotificationCount);
    });

        this.hubConnection.on('CreateAndSendTreatmentNotificationAsync', (data: any) => {
      this.$receiveTreatmentEventSubject.next(data);    
      this.$newNotificationsCount.next(++this.lastNotificationCount);
    });
  }

}
