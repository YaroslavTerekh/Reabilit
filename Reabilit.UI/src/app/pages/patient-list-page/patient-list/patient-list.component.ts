import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../../../services/doctor/doctor.service';
import { PatientDTO } from '../../../services/responseModels/PatientDTO';
import { FormsModule } from '@angular/forms';
import { DoctorDTO } from '../../../services/responseModels/DoctorDTO';
import { AnalyzeDTO } from '../../../services/responseModels/AnalyzeDTO';

@Component({
  selector: 'app-patient-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.scss'
})
export class PatientListComponent implements OnInit {
  public patients: PatientDTO[] = [];
  selectedPatient: PatientDTO | null = null;
  doctors: DoctorDTO[] = [];
  analyzes: AnalyzeDTO[] = [];
  selectedFile: File | null = null;
  addingAnalysis = false;
  newAnalysis: AnalyzeDTO = { title: '', value: '', unit: '', isNormal: true, iconPath: '', id: '' };

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

  openPatientModal(patient: PatientDTO) {
    this.selectedPatient = { ...patient }; 
    this.analyzes = this.selectedPatient.analyzes;
  }
  
  closeModal() {
    this.selectedPatient = null;
  }
  
  addAnalyze() {
    console.log('Додати аналіз');
  }

  onAddAnalysis() {
    this.addingAnalysis = true;
  }

  onFileSelected(event: Event) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
      this.selectedFile = target.files[0];
    }
  }
  
  
  saveAnalysis() {
    if (!this.selectedPatient) {
      return;
    }
  
    const formData = new FormData();
    formData.append('Title', this.newAnalysis.title);
    formData.append('Value', this.newAnalysis.value);
    formData.append('Unit', this.newAnalysis.unit);
    formData.append('IsNormal', this.newAnalysis.isNormal.toString());
    formData.append('PatientId', this.selectedPatient.id.toString());
  
    if (this.selectedFile) {
      formData.append('Icon', this.selectedFile);
    }
  
    this.doctorService.AddAnalyze(formData)
      .subscribe({
        next: (updatedAnalyzes) => {
          this.analyzes = updatedAnalyzes;
          this.newAnalysis = { title: '', value: '', unit: '', isNormal: true, iconPath: '', id: '' };
          this.selectedFile = null;
          this.addingAnalysis = false;
        },
        error: (error) => {
          console.error('Помилка при додаванні аналізу:', error);
        }
      });
  }

  deleteAnalysis(id: string) {
    this.doctorService.DeleteAnalyze(id)
      .subscribe({
        next: res => {
          this.analyzes = res;
        }
      })
  }  
  
  cancelAdding() {
    this.newAnalysis = { title: '', value: '', unit: '', isNormal: true, iconPath: '', id: '' };
    this.addingAnalysis = false;
  }
}
