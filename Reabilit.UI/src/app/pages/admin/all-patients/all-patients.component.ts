import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PatientDTO } from '../../../services/responseModels/PatientDTO';
import { AdminService } from '../../../services/admin/admin.service';
import { FormsModule } from '@angular/forms';
import { debounceTime, Subject } from 'rxjs';
import { GetUsersRequest } from '../../../services/requestModels/GetUsersRequest';
import { ContentService } from '../../../services/content/content.service';
import { GetDoctor } from '../../../services/requestModels/GetDoctor';
import { DoctorDTO } from '../../../services/responseModels/DoctorDTO';
import { AssingDoctorToPatient } from '../../../services/requestModels/AssingDoctorToPatient';
import { ToastService } from '../../../services/error-handling/toast.service';

@Component({
  selector: 'app-all-patients',
  imports: [CommonModule, FormsModule],
  templateUrl: './all-patients.component.html',
  styleUrl: './all-patients.component.scss'
})
export class AllPatientsComponent implements OnInit {
  patients: PatientDTO[] = [];
  doctors: DoctorDTO[] = [];
  searchText: string = '';
  private searchSubject = new Subject<string>();

  constructor(
    private readonly adminService: AdminService,
    private readonly contentService: ContentService,
    private readonly toastService: ToastService
  ) { }

  ngOnInit(): void {
    this.searchSubject.pipe(debounceTime(300)).subscribe(text => {
      this.loadPatients(text);
    });

    this.loadPatients(null);

    let request: GetUsersRequest = {
      searchText: null
    }

    this.contentService.getDoctors(request)
      .subscribe({
        next: res => {
          this.doctors = res;
        }
      })
  }

  onDoctorChange(patient: PatientDTO): void {
    const request: AssingDoctorToPatient = {
      patientId: patient.id,
      doctorId: patient.doctorId!
    };

    this.adminService.assignDoctorToPatient(request).subscribe({
      next: () => {
        this.toastService.show("Лікаря оновлено", "success");
      },
      error: () => {
        this.toastService.show("Помилка при оновленні лікаря", "error");
      }
    });
  }

  onSearchChange(): void {
    this.searchSubject.next(this.searchText);
  }

  loadPatients(text: string | null): void {
    const request: GetUsersRequest = { searchText: text };
    this.adminService.getPatientsByText(request).subscribe({
      next: res => (this.patients = res)
    });
  }
}