import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ProcedureEventDTO } from '../../services/responseModels/ProcedureEventDTO';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../services/patient/patient.service';
import { CancelEvent } from '../../services/requestModels/CancelEvent';

@Component({
  selector: 'app-event-card',
  imports: [CommonModule],
  templateUrl: './event-card.component.html',
  styleUrl: './event-card.component.scss'
})
export class EventCardComponent {
  @Input()
  public event: ProcedureEventDTO | undefined;
  @Output()
  public needToReload: EventEmitter<boolean> = new EventEmitter<boolean>();
  public ProcedureEventStatusUkr: Record<number, string> = {
    0: 'Заплановано',
    1: 'Скасовано',
    2: 'Завершено'
  };

  constructor(
    private readonly patientService: PatientService
  ) {}

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

  onCancel() {
    if(this.event) {
      let request: CancelEvent = {
        procedureEventId: this.event.id
      }

      this.patientService.CancelEvent(request)
        .subscribe({
          next: res => {
            this.needToReload.emit();
          }
        })
    }
  }
}
