import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/authorization/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [RouterLink, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  protected isAuthorized: boolean = false;
  protected isDoctor: boolean = false;

  constructor(
    private readonly authService: AuthService
  ) { }

  ngOnInit(): void {
    this.authService.$isAuthorized.subscribe({
      next: res => {
        this.isAuthorized = res;
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
}
