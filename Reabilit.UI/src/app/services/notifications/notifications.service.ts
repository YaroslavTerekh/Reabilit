import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { config } from '../../config';
import { Observable, ReplaySubject } from 'rxjs';
import { ProcedureEventNotificationDTO } from '../responseModels/ProcedureEventNotificationDTO';
import { MessageNotificationDTO } from '../responseModels/MessageNotificationDTO';
import { TreatmentNotificationDTO } from '../responseModels/TreatmentNotificationDTO';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  public getEventAllNotifications(): Observable<ProcedureEventNotificationDTO[]> {
    return this.http.get<ProcedureEventNotificationDTO[]>(`${this.baseUrl}/Notification/events`);
  }

  public getMessageAllNotifications(): Observable<MessageNotificationDTO[]> {
    return this.http.get<MessageNotificationDTO[]>(`${this.baseUrl}/Notification/messages`);
  }

  public getTreatmentAllNotifications(): Observable<TreatmentNotificationDTO[]> {
    return this.http.get<TreatmentNotificationDTO[]>(`${this.baseUrl}/Notification/treatment`);
  }
  
  public getEventNewNotifications(): Observable<ProcedureEventNotificationDTO[]> {
    return this.http.get<ProcedureEventNotificationDTO[]>(`${this.baseUrl}/Notification/events/new`);
  }

  public getMessageNewNotifications(): Observable<MessageNotificationDTO[]> {
    return this.http.get<MessageNotificationDTO[]>(`${this.baseUrl}/Notification/messages/new`);
  }
  
  public getTreatmentNewNotifications(): Observable<TreatmentNotificationDTO[]> {
    return this.http.get<TreatmentNotificationDTO[]>(`${this.baseUrl}/Notification/treatment/new`);
  }
  
  public readNotification(id: string): Observable<any> {
    return this.http.patch<any>(`${this.baseUrl}/Notification/read/${id}`, null);
  }
  
  public readMessageNotification(id: string): Observable<any> {
    return this.http.patch<any>(`${this.baseUrl}/Notification/message/read/${id}`, null);
  }
    
  public readTreatmentNotification(id: string): Observable<any> {
    return this.http.patch<any>(`${this.baseUrl}/Notification/treatment/read/${id}`, null);
  }

  public readAllNotifications(): Observable<any> {
    return this.http.patch<any>(`${this.baseUrl}/Notification/read/all`, null);
  }

  public deleteEventNotification(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/Notification/events/delete/${id}`);
  }
  
  public deleteMessageNotification(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/Notification/messages/delete/${id}`);
  }
    
  public deleteTreatmentNotification(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/Notification/treatments/delete/${id}`);
  }
}
