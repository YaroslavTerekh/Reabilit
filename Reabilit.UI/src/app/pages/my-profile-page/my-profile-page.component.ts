import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { PatientDTO } from '../../services/responseModels/PatientDTO';
import { AuthService } from '../../services/authorization/auth.service';
import { PatientService } from '../../services/patient/patient.service';
import { GetPatient } from '../../services/requestModels/GetPatient';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CityDTO } from '../../services/responseModels/CityDTO';
import { ContentService } from '../../services/content/content.service';
import { ModifyPatientInfo } from '../../services/requestModels/ModifyPatientInfo';
import { ChatService } from '../../services/chat/chat.service';
import { SendMessageToSupport } from '../../services/requestModels/SendMessageToSupport';
import { ToastService } from '../../services/error-handling/toast.service';

@Component({
  selector: 'app-my-profile-page',
  imports: [RouterLink, CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './my-profile-page.component.html',
  styleUrl: './my-profile-page.component.scss',
})
export class MyProfilePageComponent implements OnInit {
  protected patientInfoForm: FormGroup;
  protected patient: PatientDTO | null = null;
  protected cities: CityDTO[] = [];

  isReportModalOpen = false;
  reportText = '';

  constructor(
    private readonly router: Router,
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly patientService: PatientService,
    private readonly contentService: ContentService,
    private readonly chatService: ChatService,
    private readonly toastService: ToastService
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

  openReportModal(): void {
    this.isReportModalOpen = true;
  }

  closeReportModal(): void {
    this.isReportModalOpen = false;
    this.reportText = ''; 
  }

   submitReport(): void {
    if (this.reportText.trim()) {
      const request: SendMessageToSupport = {
        theme: "Скарга на лікаря",
        message: this.reportText
      };

      this.chatService.sendMessageToSupport(request)
        .subscribe({
          next: () => {
            this.closeReportModal(); 
            this.toastService.show("Вашу скаргу успішно відправлено!", "success")
          }
        });
    } else {
      this.toastService.show("Будь ласка, напишіть Вашу скаргу", "info")
    }
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

  askForChange(): void {
    let request: SendMessageToSupport = {
      theme: "Змінити мого лікаря",
      message: "Прохання замінити лікаря для мене"
    }

    this.chatService.sendMessageToSupport(request)
      .subscribe({
        next: res => {
          this.toastService.show("Ваш запит успішно відправлено!", "success")
        }
      })
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

  onExit(): void {
    this.authService.logOut();
    this.router.navigate(['']);
  }
}
