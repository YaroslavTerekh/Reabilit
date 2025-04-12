import { Component } from '@angular/core';
import { ToastService } from '../../services/error-handling/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-toast-error',
  imports: [CommonModule],
  templateUrl: './toast-error.component.html',
  styleUrl: './toast-error.component.scss'
})
export class ToastErrorComponent {
  
  constructor(public toastService: ToastService) {}
  
}
