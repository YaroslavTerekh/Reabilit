import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AdminService } from '../../../services/admin/admin.service';

@Component({
  selector: 'app-add-banner',
  imports: [ReactiveFormsModule],
  templateUrl: './add-banner.component.html',
  styleUrl: './add-banner.component.scss'
})
export class AddBannerComponent {
  bannerForm: FormGroup;
  selectedFile: File | null = null;

  constructor(
    private readonly fb: FormBuilder,
    private readonly adminService: AdminService
  ) {
    this.bannerForm = this.fb.group({
      description: ['', Validators.required]
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(): void {
    if (!this.bannerForm.valid || !this.selectedFile) return;

    const formData = new FormData();
    formData.append('Description', this.bannerForm.get('description')!.value);
    formData.append('Image', this.selectedFile);

    this.adminService.addBanner(formData)
      .subscribe({
        next: res => {}
      })
  }
}