import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../../../services/doctor/doctor.service';
import { ProcedureEventDTO, ProcedureEventStatus } from '../../../services/responseModels/ProcedureEventDTO';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctor-events-page',
  imports: [CommonModule],
  templateUrl: './doctor-events-page.component.html',
  styleUrl: './doctor-events-page.component.scss'
})
export class DoctorEventsPageComponent implements OnInit{
  protected procedureEvents: ProcedureEventDTO[] = [];

  constructor(
    private readonly doctorService: DoctorService
  ) {}

  ngOnInit(): void {
    this.doctorService.GetMyEvents()
      .subscribe({
        next: res => {
          this.procedureEvents = res;
        }
      })
  }

  formatUADateTime(isoString: string): string {
    const date = new Date(isoString);

    const day = date.getDate();
    const monthNames = [
      'січня', 'лютого', 'березня', 'квітня', 'травня', 'червня',
      'липня', 'серпня', 'вересня', 'жовтня', 'листопада', 'грудня'
    ];
    const month = monthNames[date.getMonth()];
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');

    return `${day} ${month} ${hours}:${minutes}`;
  }
}
