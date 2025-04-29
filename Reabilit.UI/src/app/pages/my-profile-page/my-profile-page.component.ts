import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PatientDTO } from '../../services/responseModels/PatientDTO';
import { AuthService } from '../../services/authorization/auth.service';
import { PatientService } from '../../services/patient/patient.service';
import { GetPatient } from '../../services/requestModels/GetPatient';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CityDTO } from '../../services/responseModels/CityDTO';
import { ContentService } from '../../services/content/content.service';
import { ModifyPatientInfo } from '../../services/requestModels/ModifyPatientInfo';

@Component({
  selector: 'app-my-profile-page',
  imports: [RouterLink, CommonModule, ReactiveFormsModule],
  templateUrl: './my-profile-page.component.html',
  styleUrl: './my-profile-page.component.scss',
})
export class MyProfilePageComponent implements OnInit {
  protected patientInfoForm: FormGroup;
  protected patient: PatientDTO | null = null;
  protected cities: CityDTO[] = [];

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly patientService: PatientService,
    private readonly contentService: ContentService
  ) {    
    this.patientInfoForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+380\d{9}$/)]],
      age: ['', [Validators.required]],
      cityId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.contentService.GetAllCities()
      .subscribe({
        next: cities => {
          this.cities = cities;
  
          this.authService.getCurrentUserRole()
            .subscribe({
              next: user => {
                const request: GetPatient = {
                  appUserId: user.userId
                };
  
                this.patientService.GetUserPatient(request)
                  .subscribe({
                    next: patient => {
                      this.patient = patient;
                      
                      this.initForm(patient);
                    }
                  });
              }
            });
        }
      });
  }

  initForm(patient: PatientDTO): void {
    this.patientInfoForm = this.fb.group({
      firstName: [patient.firstName, [Validators.required]],
      lastName: [patient.lastName, [Validators.required]],
      phoneNumber: [patient.phoneNumber, [Validators.required, Validators.pattern(/^\+380\d{9}$/)]],
      age: [patient.age, [Validators.required]],
      cityId: [patient.cityId, [Validators.required]]
    });
  }

  onSubmit(): void {
    if(this.patientInfoForm.valid) {
      let request: ModifyPatientInfo = {
        firstName: this.patientInfoForm.get('firstName')?.value,
        lastName: this.patientInfoForm.get('lastName')?.value,
        age: this.patientInfoForm.get('age')?.value,
        phoneNumber: this.patientInfoForm.get('phoneNumber')?.value,
        cityId: this.patientInfoForm.get('cityId')?.value
      };

      this.patientService.ModifyPatientInfo(request)
        .subscribe({
          next: patient => {
            this.patient = patient;
            
            this.initForm(patient);
          }
        })
    }
  }
}
