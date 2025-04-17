import { Component, OnInit } from '@angular/core';
import { DoctorInfoComponent } from "./components/doctor-info/doctor-info.component"
import { ScheduleComponent } from "./components/schedule/schedule.component"
import { DoctorDTO } from '../../services/responseModels/DoctorDTO';
import { PatientService } from '../../services/patient/patient.service';
import { ToastErrorComponent } from '../../modals/toast-error/toast-error.component';
import { ToastService } from '../../services/error-handling/toast.service';

@Component({
  selector: 'app-doctor-profile',
  imports: [DoctorInfoComponent, ScheduleComponent],
  templateUrl: './doctor-profile.component.html',
  styleUrl: './doctor-profile.component.scss'
})
export class DoctorProfileComponent{
  
}
