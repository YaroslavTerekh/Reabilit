import { Component, OnInit } from '@angular/core';
import { FreeSlotsDTO, SlotHourDTO } from '../../../../services/responseModels/FreeSlotsDTO';
import { PatientService } from '../../../../services/patient/patient.service';
import { DoctorService } from '../../../../services/doctor/doctor.service';
import { CommonModule } from '@angular/common';
import { AddPatientProcedureEvent } from '../../../../services/requestModels/AddPatientProcedureEvent';
import { ToastService } from '../../../../services/error-handling/toast.service';
import { NotificationsService } from '../../../../services/notifications/notifications.service';

@Component({
  selector: 'app-schedule',
  imports: [CommonModule],
  templateUrl: './schedule.component.html',
  styleUrl: './schedule.component.scss'
})
export class ScheduleComponent implements OnInit {
  confirmationData: { time: string } | null = null;
  docWeekSchedule: FreeSlotsDTO[] = [];
  weekDates: any[] = [];
  daysOfWeek: string[] = [
    'Неділя',
    'Понеділок',
    'Вівторок',
    'Середа',
    'Четвер',
    'П’ятниця',
    'Субота',
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
    const offset = (currentDay === 0 ? -6 : 0 - currentDay); 
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
      const isPast = today > day; 
      this.weekDates.push({ formatted, isPast });
    }    
  }

  getFullDateTime(dayIndex: number, time: string): string {
    const today = new Date();
    const currentDay = today.getDay();
    const offset = (currentDay === 0 ? -6 : 0 - currentDay);
  
    const day = new Date(today);
    day.setDate(today.getDate() + offset + dayIndex);
  
    const [hours, minutes] = time.split(':');
    day.setHours(+hours, +minutes, 0, 0);

    return day.toISOString(); 
  }

  isSlotInPast(dayIndex: number, time: string): boolean {
    const slotDateTime = new Date();
    const currentDay = slotDateTime.getDay();
    const offset = currentDay === 0 ? -6 : 0 - currentDay;

    slotDateTime.setDate(slotDateTime.getDate() + offset + dayIndex);

    const [hours, minutes] = time.split(':').map(Number);
    slotDateTime.setHours(hours, minutes, 0, 0);

    return slotDateTime < new Date();
  }

  reserveSlot(dayIndex: number, time: SlotHourDTO): void {    
    if(!time.isAvailable || this.weekDates[dayIndex].isPast) return;

    const fullDateTime = this.getFullDateTime(dayIndex, time.time);
    this.confirmationData = { time: fullDateTime };
  }

  confirmReservation(): void {
    if (!this.confirmationData) return;
  
    const request: AddPatientProcedureEvent = {
      startsOn: this.confirmationData.time
    };
  
    this.patientService.AddProcedureEvent(request)
      .subscribe({
        next: res => {
          this.doctorService.GetLoggedInUserDoctorFreeSchedule()
            .subscribe({
              next: res => {
                this.docWeekSchedule = res;
                this.confirmationData = null;
              }
            });
        },
        error: err => {
          this.toastService.show(err.error?.message, "error");
          this.confirmationData = null;
        }
      });
  }
  
  cancelReservation(): void {
    this.confirmationData = null;
  }
  
  getUkrainianDateTime(iso: string): string {
    const date = new Date(iso);
    const options: Intl.DateTimeFormatOptions = {
      weekday: 'long',
      day: 'numeric',
      month: 'long',
      hour: '2-digit',
      minute: '2-digit'
    };
    return date.toLocaleDateString('uk-UA', options);
  }
  
}