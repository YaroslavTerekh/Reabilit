import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/authorization/auth.service';
import { NotificationsService } from '../../services/notifications/notifications.service';
import { ProcedureEventNotificationDTO } from '../../services/responseModels/ProcedureEventNotificationDTO';
import { MessageNotificationDTO } from '../../services/responseModels/MessageNotificationDTO';
import { SignalrService } from '../../services/chat/signalr.service';
import { TreatmentNotificationDTO } from '../../services/responseModels/TreatmentNotificationDTO';
import { MatBadgeModule } from '@angular/material/badge';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-notifications',
  imports: [CommonModule, MatIconModule],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.scss'
})
export class NotificationsComponent implements OnInit {
  eventNotifications: ProcedureEventNotificationDTO[] = [];
  messageNotifications: MessageNotificationDTO[] = [];
  treatmentNotifications: TreatmentNotificationDTO[] = [];
  private currentNewNotificationsCount: number = 0;

  activeTab: 'events' | 'messages' | 'results' = 'events';

  constructor(
    private readonly authService: AuthService,
    private readonly notificationsService: NotificationsService,
    private readonly signalrService: SignalrService
  ) { }

  ngOnInit(): void {
    this.initEventNotifications();
    this.initMessageNotifications();
    this.initTreatmentNotifications();

    this.signalrService.$newNotificationsCount
      .subscribe({
        next: res => this.currentNewNotificationsCount = res
      })
  }

  switchTab(tab: 'events' | 'messages' | 'results'): void {
    this.activeTab = tab;
  }

  initMessageNotifications(): void {
    this.notificationsService
      .getMessageAllNotifications()
      .subscribe({
        next: res => {
          this.messageNotifications = res;
        }
      });
  }

  initTreatmentNotifications(): void {
    this.notificationsService
      .getTreatmentAllNotifications()
      .subscribe({
        next: res => {
          this.treatmentNotifications = res;
        }
      });
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

          this.messageNotifications.map(e => {
            e.isRead = true;
          })
          
          this.treatmentNotifications.map(e => {
            e.isRead = true;
          })

          this.signalrService.$newNotificationsCount.next(0);
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

          this.signalrService.$newNotificationsCount.next(--this.currentNewNotificationsCount);
        }
      })
  }

  readMessageNotification(id: string): void {
    this.notificationsService.readMessageNotification(id)
      .subscribe({
        next: res => {
          this.messageNotifications.map(e => {
            e.isRead = e.id == id ? true : e.isRead;
          })

          this.signalrService.$newNotificationsCount.next(--this.currentNewNotificationsCount);
        }
      })
  }

  readTreatmentNotification(id: string): void {
    this.notificationsService.readTreatmentNotification(id)
      .subscribe({
        next: res => {
          this.treatmentNotifications.map(e => {
            e.isRead = e.id == id ? true : e.isRead;
          })

          this.signalrService.$newNotificationsCount.next(--this.currentNewNotificationsCount);
        }
      })
  }

  deleteEventNotification(id: string): void {
    this.notificationsService.deleteEventNotification(id)
      .subscribe({
        next: res => {
          if (!this.eventNotifications.filter(en => en.id == id)[0].isRead) {
            this.signalrService.$newNotificationsCount.next(--this.currentNewNotificationsCount);
          }

          this.eventNotifications = this.eventNotifications.filter(n => n.id != id);
        }
      })
  }

  deleteMessageNotification(id: string): void {
    this.notificationsService.deleteMessageNotification(id)
      .subscribe({
        next: res => {
          if (!this.messageNotifications.filter(en => en.id == id)[0].isRead) {
            this.signalrService.$newNotificationsCount.next(this.currentNewNotificationsCount--);
          }

          this.messageNotifications = this.messageNotifications.filter(n => n.id != id);
        }
      })
  }

  deleteTreatmentNotification(id: string): void {
    this.notificationsService.deleteTreatmentNotification(id)
      .subscribe({
        next: res => {
          if (!this.treatmentNotifications.filter(en => en.id == id)[0].isRead) {
            this.signalrService.$newNotificationsCount.next(this.currentNewNotificationsCount--);
          }

          this.treatmentNotifications = this.treatmentNotifications.filter(n => n.id != id);
        }
      })
  }

  closeNotifications(): void {
    this.authService.$showNotificationsModalSubject.next(false)
  }


  showInfoResult(notification: TreatmentNotificationDTO): void {
    alert(notification.recommendations)
  }
}
