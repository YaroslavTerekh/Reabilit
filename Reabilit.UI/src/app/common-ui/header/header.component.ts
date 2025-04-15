import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/authorization/auth.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  protected isAuthorized: boolean = false;

  constructor(
    private readonly authService: AuthService
  ) { }

  ngOnInit(): void {
    this.authService.$isAuthorized.subscribe({
      next: res => {
        this.isAuthorized = res;
      }
    })
  }

  protected showLoginModal(): void {
    this.authService.$showLoginModalSubject.next(true);
  }
}
