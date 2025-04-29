import { Component, OnDestroy, OnInit } from '@angular/core';
import { DoctorCardComponent } from "./components/doctor-card/doctor-card.component";
import { DoctorDTO } from '../../services/responseModels/DoctorDTO';
import { ContentService } from '../../services/content/content.service';
import { GetUsersRequest } from '../../services/requestModels/GetUsersRequest';
import { CommonModule } from '@angular/common';
import { Subject, debounceTime, takeUntil } from 'rxjs';
import { DoctorService } from '../../services/doctor/doctor.service';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-doctors-list-page',
  imports: [DoctorCardComponent, CommonModule, FormsModule],
  templateUrl: './doctors-list-page.component.html',
  styleUrl: './doctors-list-page.component.scss'
})
export class DoctorsListPageComponent implements OnInit, OnDestroy {
  doctors: DoctorDTO[] = [];
  filteredDoctors: DoctorDTO[] = [];
  private searchSubject = new Subject<string>();
  private destroy$ = new Subject<void>();
  searchTerm: string | null = null;

  constructor(private doctorService: DoctorService,
    private readonly contentService: ContentService,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.searchTerm = params.get('searchTerm');
      console.log(this.searchTerm);
      
      if (this.searchTerm) {
        this.applySearch(this.searchTerm);
      } else {
        this.loadDoctors();
      }
    });

    this.searchSubject.pipe(
      debounceTime(300),
      takeUntil(this.destroy$)
    ).subscribe(searchTerm => {
      this.applySearch(searchTerm);
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadDoctors() {
    let req: GetUsersRequest = { searchText: null }

    this.contentService.getDoctors(req).subscribe(doctors => {
      this.doctors = doctors;
      this.filteredDoctors = doctors;
    });
  }

  onSearchInput() {
    this.searchSubject.next(this.searchTerm!);
  }

  applySearch(term: string) {
    let req: GetUsersRequest = { searchText: term }

    this.contentService.getDoctors(req).subscribe(doctors => {
      this.doctors = doctors;
      this.filteredDoctors = doctors;
    });
  }
}
