import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DoctorDTO } from '../../../services/responseModels/DoctorDTO';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../../services/admin/admin.service';
import { GetUsersRequest } from '../../../services/requestModels/GetUsersRequest';
import { FormsModule } from '@angular/forms';
import { Subject, debounceTime } from 'rxjs';

@Component({
  selector: 'app-all-doctors',
  imports: [CommonModule, FormsModule],
  templateUrl: './all-doctors.component.html',
  styleUrl: './all-doctors.component.scss'
})
export class AllDoctorsComponent implements OnInit {
  doctors: DoctorDTO[] = [];
  searchText: string = '';
  private searchSubject = new Subject<string>();

  constructor(private readonly adminService: AdminService) {}

  ngOnInit(): void {
    this.searchSubject.pipe(debounceTime(300)).subscribe(text => {
      this.loadDoctors(text);
    });

    this.loadDoctors(null);
  }

  onSearchChange(): void {
    this.searchSubject.next(this.searchText);
  }

  loadDoctors(text: string | null): void {
    const request: GetUsersRequest = { searchText: text };
    this.adminService.getDoctorsByText(request).subscribe({
      next: res => (this.doctors = res)
    });
  }
}
