import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../services/authorization/auth.service';

@Component({
  selector: 'app-main',
  imports: [RouterModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent {

  constructor(
    private readonly authService: AuthService
  ){}

  logout(): void {
    this.authService.logOut();
  }
}
