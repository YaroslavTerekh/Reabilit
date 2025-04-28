import { Injectable, ErrorHandler } from "@angular/core";
import { ToastService } from "./toast.service";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private toastService: ToastService) {}

  handleError(error: any): void {
    console.error('Global error caught:', error);
    this.toastService.show(error.error?.message, 'error');
  }
}