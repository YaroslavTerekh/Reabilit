import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../../../services/doctor/doctor.service';
import { ProcedureEventDTO, ProcedureEventStatus } from '../../../services/responseModels/ProcedureEventDTO';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddEventResult } from '../../../services/requestModels/AddEventResult';
import { AddTreatmentRecommendation } from '../../../services/requestModels/AddTreatmentRecommendation';
import { ToastService } from '../../../services/error-handling/toast.service';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-doctor-events-page',
  imports: [CommonModule, FormsModule, MatIconModule],
  templateUrl: './doctor-events-page.component.html',
  styleUrl: './doctor-events-page.component.scss'
})
export class DoctorEventsPageComponent implements OnInit {
  protected procedureEvents: ProcedureEventDTO[] = [];

  activeProcedure: ProcedureEventDTO | null = null;
  examinationResult: string = '';
  treatmentAdvice: string = '';

  constructor(
    private readonly doctorService: DoctorService,
    private readonly toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.doctorService.GetMyEvents().subscribe({
      next: res => {
        this.procedureEvents = res;
      }
    });
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

  openModal(item: ProcedureEventDTO): void {
    this.activeProcedure = item;
    this.examinationResult = '';
    this.treatmentAdvice = '';
  }

  closeModal(): void {
    this.activeProcedure = null;
  }

  sendExaminationResult(): void {
    let request: AddEventResult = {
      result: this.examinationResult,
      procedureEventId: this.activeProcedure!.id
    }

    this.doctorService.AddEventResult(request)
      .subscribe({
        next: res => {
          this.activeProcedure = null;

          this.toastService.show("Результат прийому успішно додано", "success")

          this.procedureEvents.map(pe => {
            if(pe.id == request.procedureEventId) {
              pe.result = request.result
            }
          })
        }
      })
  }

  sendTreatmentAdvice(): void {
    let request: AddTreatmentRecommendation = {
      procedureEventId: this.activeProcedure?.id!,
      receiverId: this.activeProcedure?.patient?.appUserId!,
      recommendation: this.treatmentAdvice
    }

    this.doctorService.AddTreatmentRecommendation(request)
      .subscribe({
        next: res => {
          this.activeProcedure = null;
          this.toastService.show("Пораду з лікування успішно надіслано", "success")
        }
      });
  }
}
