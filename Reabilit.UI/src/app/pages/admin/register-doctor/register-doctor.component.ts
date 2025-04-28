import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DoctorService } from '../../../services/doctor/doctor.service';
import { ContentService } from '../../../services/content/content.service';
import { RegisterDoctor } from '../../../services/requestModels/RegisterDoctor';
import { AdminService } from '../../../services/admin/admin.service';
import { ToastService } from '../../../services/error-handling/toast.service';

@Component({
  selector: 'app-register-doctor',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-doctor.component.html',
  styleUrl: './register-doctor.component.scss'
})
export class RegisterDoctorComponent implements OnInit {
  registerDoctorForm!: FormGroup;
  doctorClasses: { id: string; className: string }[] = [];

  constructor(
    private readonly fb: FormBuilder,
    private readonly adminService: AdminService,
    private readonly contentService: ContentService,
    private readonly toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadDoctorClasses();
  }

  loadDoctorClasses(): void {
    this.contentService.GetAllDoctorClasses()
      .subscribe({
        next: res => {
          this.doctorClasses = res;
        }
      })
  }

  initForm(): void {
    this.registerDoctorForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[\d\+][\d\(\)\ -]{4,14}\d$/)]],
      age: [null, [Validators.required, Validators.min(18)]],
      degree: ['', Validators.required],
      experienceInYear: [0, [Validators.required, Validators.min(0)]],
      doctorClassId: [null, Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.registerDoctorForm.invalid) return;

    const payload = this.registerDoctorForm.value;

    let request: RegisterDoctor = {
      firstName: this.registerDoctorForm.get('firstName')?.value,
      lastName: this.registerDoctorForm.get('lastName')?.value,
      age: this.registerDoctorForm.get('age')?.value,
      phoneNumber: this.registerDoctorForm.get('phoneNumber')?.value,
      degree: this.registerDoctorForm.get('degree')?.value,
      experienceInYear: this.registerDoctorForm.get('experienceInYear')?.value,
      doctorClassId: this.registerDoctorForm.get('doctorClassId')?.value,
      password: this.registerDoctorForm.get('password')?.value,
    };

    this.adminService.registerDoctor(request)
      .subscribe({
        next: res=> {
          this.initForm();
          this.toastService.show("Лікаря зареєстровано!", "success");          
        }
      })
    
  }
}