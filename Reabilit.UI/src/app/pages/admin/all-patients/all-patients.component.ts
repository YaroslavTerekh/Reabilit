import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PatientDTO } from '../../../services/responseModels/PatientDTO';
import { AdminService } from '../../../services/admin/admin.service';
import { FormsModule } from '@angular/forms';
import { debounceTime, Subject } from 'rxjs';
import { GetUsersRequest } from '../../../services/requestModels/GetUsersRequest';

@Component({
  selector: 'app-all-patients',
  imports: [CommonModule, FormsModule],
  templateUrl: './all-patients.component.html',
  styleUrl: './all-patients.component.scss'
})
export class AllPatientsComponent implements OnInit {
  patients: PatientDTO[] = [];
  searchText: string = '';
  private searchSubject = new Subject<string>();

  constructor(private readonly adminService: AdminService) {}

  ngOnInit(): void {
    this.searchSubject.pipe(debounceTime(300)).subscribe(text => {
      this.loadPatients(text);
    });

    this.loadPatients(null);
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