import { Component } from '@angular/core';
import { AuthService } from '../../services/authorization/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginModel } from '../../services/requestModels/LoginModel';
import { AuthToken } from '../../services/responseModels/AuthToken';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastService } from '../../services/error-handling/toast.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly toastService: ToastService
  ) {
    this.loginForm = this.fb.group({
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+380\d{9}$/)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  protected hideLoginModal(): void {
    this.authService.$showLoginModalSubject.next(false);
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const loginData: LoginModel = this.loginForm.value;

    this.authService.login(loginData).subscribe({
      next: (token: AuthToken) => {
        this.hideLoginModal();

        localStorage.setItem('token', token.token);
        localStorage.setItem('expires', token.expires);

        this.toastService.show(`Ваша сесія активна до ${new DatePipe("uk-UA").transform(token.expires, "hh:mm dd/MM/yyyy")}`, 'info');

        this.authService.$isAuthorized.next(true);

        this.authService.getCurrentUserRole()
          .subscribe({
            next: res => {
              this.authService.$currentRole.next(res.appRole);
            }
          })
      }
    });
  }
}
