import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/authorization/auth.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';
import { NotificationsService } from '../../services/notifications/notifications.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink, CommonModule, MatIconModule, MatIconModule, MatBadgeModule, MatIconModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  protected isAuthorized: boolean = false;
  protected isDoctor: boolean = false;
  protected newNotificationCount: number = 0;

  constructor(
    private readonly authService: AuthService,
    private readonly notificationsService: NotificationsService
  ) { }

  ngOnInit(): void {

    this.notificationsService.$newNotificationsCountSubject
    .subscribe({
      next: res => {
        this.newNotificationCount = res
      }
    })

    this.authService.$isAuthorized.subscribe({
      next: res => {
        this.isAuthorized = res;

        if(res) {
          this.notificationsService.getEventNewNotifications()
            .subscribe({
              next: res => this.newNotificationCount = res.length
            })
        }
      }
    })

    this.authService.$currentRole.subscribe({
      next: res => {
        this.isDoctor = res == "Doctor" ? true : false;
      }
    })
  }

  protected showLoginModal(): void {
    this.authService.$showLoginModalSubject.next(true);
  }

  protected showNotifications(): void {
    this.authService.$showNotificationsModalSubject.next(true);
  }
}
