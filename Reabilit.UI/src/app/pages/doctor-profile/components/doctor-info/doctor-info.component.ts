import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../../../services/error-handling/toast.service';
import { PatientService } from '../../../../services/patient/patient.service';
import { DoctorDTO } from '../../../../services/responseModels/DoctorDTO';

@Component({
  selector: 'app-doctor-info',
  imports: [],
  templateUrl: './doctor-info.component.html',
  styleUrl: './doctor-info.component.scss'
})
export class DoctorInfoComponent implements OnInit {
  protected doctor: DoctorDTO | undefined = undefined;

  constructor(
    private readonly patientService: PatientService,
    private readonly toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.patientService.GetDoctor()
      .subscribe({
        next: res => {
          this.doctor = res;
        },
        error: err => {
          this.toastService.show(err.error?.message, "error");
        }
      });
  } 
}