import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/authorization/auth.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

    constructor(
      private readonly authService: AuthService
    ) {}

    protected showLoginModal(): void {
      this.authService.$showLoginModalSubject.next(true);
    }
}
