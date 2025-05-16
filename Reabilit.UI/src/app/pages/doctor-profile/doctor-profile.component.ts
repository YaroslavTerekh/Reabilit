import { Component, OnInit } from '@angular/core';
import { DoctorInfoComponent } from "./components/doctor-info/doctor-info.component"
import { ScheduleComponent } from "./components/schedule/schedule.component"

@Component({
  selector: 'app-doctor-profile',
  imports: [DoctorInfoComponent, ScheduleComponent],
  templateUrl: './doctor-profile.component.html',
  styleUrl: './doctor-profile.component.scss'
})
export class DoctorProfileComponent{
  
}
