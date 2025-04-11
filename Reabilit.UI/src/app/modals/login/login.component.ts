import { Component } from '@angular/core';
import { AuthService } from '../../services/authorization/auth.service';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  
    constructor(
      private readonly authService: AuthService
    ) {}

    protected hideLoginModal(): void {
      this.authService.$showLoginModalSubject.next(false);
    }
}
