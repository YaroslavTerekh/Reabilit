import { Component, OnInit } from '@angular/core';
import { FreeSlotsDTO } from '../../../../services/responseModels/FreeSlotsDTO';
import { PatientService } from '../../../../services/patient/patient.service';
import { DoctorService } from '../../../../services/doctor/doctor.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-schedule',
  imports: [CommonModule],
  templateUrl: './schedule.component.html',
  styleUrl: './schedule.component.scss'
})
export class ScheduleComponent implements OnInit {
  docWeekSchedule: FreeSlotsDTO[] = [];
  daysOfWeek: string[] = [
    'Понеділок',
    'Вівторок',
    'Середа',
    'Четвер',
    'П’ятниця',
    'Субота',
    'Неділя'
  ];
  
  constructor(
    private readonly doctorService: DoctorService
  ) {}

  ngOnInit(): void {
    this.doctorService.GetLoggedInUserDoctorFreeSchedule()
      .subscribe({
        next: res => {
          this.docWeekSchedule = res;
          console.log(JSON.stringify(res));
          
        }
      })
  }
}