import { Component, OnInit } from '@angular/core';
import { FreeSlotsDTO } from '../../../../services/responseModels/FreeSlotsDTO';
import { PatientService } from '../../../../services/patient/patient.service';
import { DoctorService } from '../../../../services/doctor/doctor.service';
import { CommonModule } from '@angular/common';
import { AddPatientProcedureEvent } from '../../../../services/requestModels/AddPatientProcedureEvent';
import { ToastService } from '../../../../services/error-handling/toast.service';

@Component({
  selector: 'app-schedule',
  imports: [CommonModule],
  templateUrl: './schedule.component.html',
  styleUrl: './schedule.component.scss'
})
export class ScheduleComponent implements OnInit {
  docWeekSchedule: FreeSlotsDTO[] = [];
  weekDates: string[] = [];
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
    private readonly doctorService: DoctorService,
    private readonly patientService: PatientService,
    private readonly toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.generateWeekDates();
  
    this.doctorService.GetLoggedInUserDoctorFreeSchedule()
      .subscribe({
        next: res => {
          this.docWeekSchedule = res;
        }
      });
  }
  
  generateWeekDates(): void {
    const today = new Date();
    const startOfWeek = new Date(today);
    const currentDay = today.getDay();
    const offset = (currentDay === 0 ? -6 : 1 - currentDay); // перший день — Понеділок
    startOfWeek.setDate(today.getDate() + offset);
  
    const ukrMonths = [
      'січня', 'лютого', 'березня', 'квітня', 'травня', 'червня',
      'липня', 'серпня', 'вересня', 'жовтня', 'листопада', 'грудня'
    ];
  
    this.weekDates = [];
  
    for (let i = 0; i < 7; i++) {
      const day = new Date(startOfWeek);
      day.setDate(startOfWeek.getDate() + i);
  
      const formatted = `${this.daysOfWeek[i]}, ${day.getDate()} ${ukrMonths[day.getMonth()]}`;
      this.weekDates.push(formatted);
    }
  }

  getFullDateTime(dayIndex: number, time: string): string {
    const today = new Date();
    const startOfWeek = new Date(today);
    const currentDay = today.getDay();
    const offset = (currentDay === 0 ? -6 : 1 - currentDay); // Понеділок — перший
    startOfWeek.setDate(today.getDate() + offset + dayIndex - 1);
  
    const [hours, minutes] = time.split(':');
    startOfWeek.setHours(+hours, +minutes, 0, 0);
  
    const pad = (n: number) => n.toString().padStart(2, '0');
  
    const localISOString = `${startOfWeek.getFullYear()}-${pad(startOfWeek.getMonth() + 1)}-${pad(startOfWeek.getDate())}T${pad(startOfWeek.getHours())}:${pad(startOfWeek.getMinutes())}:00`;
  
    return localISOString;
  }

  reserveSlot(time: string): void {
    let request: AddPatientProcedureEvent = {
      startsOn: time
    };

    this.patientService.AddProcedureEvent(request)
      .subscribe({
        next: res => {
          this.doctorService.GetLoggedInUserDoctorFreeSchedule()
          .subscribe({
            next: res => {
              this.docWeekSchedule = res;
            }
          });       
        },
        error: err => {
          this.toastService.show(err.error?.message, "error");
        }
      })
  }
}