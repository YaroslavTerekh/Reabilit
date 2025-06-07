import { Component, OnInit } from '@angular/core';
import { EventCardComponent } from "../../common-ui/event-card/event-card.component";
import { ProcedureEventDTO } from '../../services/responseModels/ProcedureEventDTO';
import { PatientService } from '../../services/patient/patient.service';
import { ToastService } from '../../services/error-handling/toast.service';

@Component({
  selector: 'app-events-page',
  imports: [EventCardComponent],
  templateUrl: './events-page.component.html',
  styleUrl: './events-page.component.scss'
})
export class EventsPageComponent implements OnInit {
  protected events: ProcedureEventDTO[] = [];

  constructor(
    private readonly patientService: PatientService,
    private readonly toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.getAllEvents()
  }

  getAllEvents(): void {
    this.patientService.GetMyEvents()
    .subscribe({
      next: res => {
        this.events = res;
      }
    })
  }
}
