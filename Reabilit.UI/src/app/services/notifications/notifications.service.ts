import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { config } from '../../config';
import { Observable, ReplaySubject } from 'rxjs';
import { ProcedureEventNotificationDTO } from '../responseModels/ProcedureEventNotificationDTO';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  public $newNotificationsCountSubject: ReplaySubject<number> = new ReplaySubject<number>();
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  public getEventAllNotifications(): Observable<ProcedureEventNotificationDTO[]> {
    return this.http.get<ProcedureEventNotificationDTO[]>(`${this.baseUrl}/Notification/events`);
  }
  
  public getEventNewNotifications(): Observable<ProcedureEventNotificationDTO[]> {
    return this.http.get<ProcedureEventNotificationDTO[]>(`${this.baseUrl}/Notification/events/new`);
  }
  
  public readNotification(id: string): Observable<any> {
    return this.http.patch<any>(`${this.baseUrl}/Notification/read/${id}`, null);
  }

  public readAllNotifications(): Observable<any> {
    return this.http.patch<any>(`${this.baseUrl}/Notification/read/all`, null);
  }

  public deleteEventNotification(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/Notification/events/delete/${id}`);
  }
}
