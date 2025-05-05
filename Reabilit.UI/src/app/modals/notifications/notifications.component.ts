import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/authorization/auth.service';
import { NotificationsService } from '../../services/notifications/notifications.service';
import { ProcedureEventNotificationDTO } from '../../services/responseModels/ProcedureEventNotificationDTO';

@Component({
  selector: 'app-notifications',
  imports: [CommonModule],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.scss'
})
export class NotificationsComponent implements OnInit {
  eventNotifications: ProcedureEventNotificationDTO[] = [];

  constructor(
    private readonly authService: AuthService,
    private readonly notificationsService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.initEventNotifications();
  }

  initEventNotifications(): void {
    this.notificationsService
      .getEventAllNotifications()
      .subscribe({
        next: res => {
          this.eventNotifications = res;
        }
      })
  }

  readAllNotifications(): void {
    this.notificationsService.readAllNotifications()
      .subscribe({
        next: res => {
          this.eventNotifications.map(e => {
            e.isRead = true;
          })

          this.notificationsService.$newNotificationsCountSubject.next(0);
        }
      })
  }

  readNotification(id: string): void {
    this.notificationsService.readNotification(id)
      .subscribe({
        next: res => {
          this.eventNotifications.map(e => {
            e.isRead = e.id == id ? true : e.isRead;
          })          

          this.notificationsService.$newNotificationsCountSubject.next(this.eventNotifications.filter(e => !e.isRead).length);
        }
      })
  }

  deleteEventNotification(id: string): void {
    this.notificationsService.deleteEventNotification(id)
      .subscribe({
        next: res => {
          this.eventNotifications = this.eventNotifications.filter(n => n.id != id);

          this.notificationsService.$newNotificationsCountSubject.next(this.eventNotifications.filter(e => !e.isRead).length);
        }
      })
  }

  closeNotifications(): void {
    this.authService.$showNotificationsModalSubject.next(false)
  }
}
