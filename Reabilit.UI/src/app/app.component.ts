import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from "./common-ui/header/header.component";
import { Router, RouterOutlet } from '@angular/router';
import { LoginComponent } from "./modals/login/login.component";
import { AuthService } from './services/authorization/auth.service';
import { ToastErrorComponent } from "./modals/toast-error/toast-error.component";
import localeUk from '@angular/common/locales/uk';
import { registerLocaleData } from '@angular/common';
import { NotificationsComponent } from "./modals/notifications/notifications.component";
import { SignalrService } from './services/chat/signalr.service';

registerLocaleData(localeUk, 'uk-UA');

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, LoginComponent, ToastErrorComponent, NotificationsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'Реабілітаційний центр';
  protected showLoginModal: boolean = false;
  protected currentRole!: string;
  protected isAuthorized: boolean = false;
  protected showNotificationsModal: boolean = false;

  constructor(
    private readonly authService: AuthService,
    private readonly signalrService: SignalrService,
    private readonly router: Router
  ) { }

  ngOnInit(): void {
    this.authService.$showLoginModalSubject
      .subscribe({
        next: res => {
          this.showLoginModal = res
        }
      });

    this.authService.$showNotificationsModalSubject
      .subscribe({
        next: res => {
          this.showNotificationsModal = res;
        }
      })

    this.authService.$currentRole.subscribe({
      next: res => this.currentRole = res
    })

    this.authService.$isAuthorized.subscribe({
      next: res => {
        this.isAuthorized = res;

        if (res) {
          this.signalrService.startConnection();
          this.signalrService.addTransferChartDataListener();

          this.authService.getCurrentUserRole()
            .subscribe({
              next: res => {
                this.authService.$currentRole.next(res.appRole);

                if(res.appRole == "Admin") {
                  this.router.navigate(['admin']);
                }

                if(res.appRole == "Support") {
                  this.router.navigate(['chats']);
                }
              }
            })
        }
      }
    })


  }
}
