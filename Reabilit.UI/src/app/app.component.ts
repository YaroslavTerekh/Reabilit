import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from "./common-ui/header/header.component";
import { RouterOutlet } from '@angular/router';
import { LoginComponent } from "./modals/login/login.component";
import { AuthService } from './services/authorization/auth.service';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'Реабілітаційний центр';
  protected showLoginModal: boolean = false;

  constructor(
    private readonly authService: AuthService,
  ) {}

  ngOnInit(): void {
    this.authService.$showLoginModalSubject
      .subscribe({
        next: res => {
          this.showLoginModal = res
        }
      });
  }
}
