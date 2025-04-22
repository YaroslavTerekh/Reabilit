import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../../../services/doctor/doctor.service';
import { PatientDTO } from '../../../services/responseModels/PatientDTO';

@Component({
  selector: 'app-patient-list',
  imports: [CommonModule],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.scss'
})
export class PatientListComponent implements OnInit {
  public patients: PatientDTO[] = [];

  constructor(
    private readonly doctorService: DoctorService
  ) {}

  ngOnInit(): void {
    this.doctorService.GetMyPatients()
      .subscribe({
        next: res => {
          this.patients = res;
        }
      })
  }
}
