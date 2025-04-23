import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DoctorDTO } from '../../services/responseModels/DoctorDTO';
import { DoctorService } from '../../services/doctor/doctor.service';
import { ModifyDoctorInfo } from '../../services/requestModels/ModifyDoctorInfo';

@Component({
  selector: 'app-my-profile-page-doctor',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './my-profile-page-doctor.component.html',
  styleUrl: './my-profile-page-doctor.component.scss'
})
export class MyProfilePageDoctorComponent implements OnInit {
  public doctorForm!: FormGroup;
  public doctor!: DoctorDTO;

  constructor(
    private readonly fb: FormBuilder,
    private readonly doctorService: DoctorService
  ) {
    this.doctorForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+380\d{9}$/)]],
      age: [null, [Validators.min(0)]],
      degree: ['', Validators.required],
      experienceInYear: [null, [Validators.min(0)]],
      biography: ['']
    });
  }

  ngOnInit(): void {
    this.doctorService.GetMyAccount()
      .subscribe({
        next: res => {
          this.doctor = res;

          this.reloadForm(res);
        }
      })
  }

  onSubmit(): void {
    if (this.doctorForm.valid) {
      let request: ModifyDoctorInfo = {
        firstName: this.doctorForm.get('firstName')?.value,
        lastName: this.doctorForm.get('lastName')?.value,
        age: this.doctorForm.get('age')?.value,
        phoneNumber: this.doctorForm.get('phoneNumber')?.value,
        biography: this.doctorForm.get('biography')?.value,
        degree: this.doctorForm.get('degree')?.value,
        experienceInYear: this.doctorForm.get('experienceInYear')?.value,
      };

      this.doctorService.ModifyDoctorInfo(request)
        .subscribe({
          next: res => {
            this.reloadForm(res);
          }
        })
      
    } else {
      this.doctorForm.markAllAsTouched();
    }
  }

  reloadForm(res: DoctorDTO): void {
    this.doctorForm = this.fb.group({
      firstName: [res.firstName, Validators.required],
      lastName: [res.lastName, Validators.required],
      phoneNumber: [res.phoneNumber, [Validators.required, Validators.pattern(/^\+380\d{9}$/)]],
      age: [res.age, [Validators.min(0)]],
      degree: [res.degree, Validators.required],
      experienceInYear: [res.experienceInYear, [Validators.min(0)]],
      biography: [res.biography]
    });
  }
}