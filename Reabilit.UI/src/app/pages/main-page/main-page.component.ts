import { Component, CUSTOM_ELEMENTS_SCHEMA, OnDestroy, OnInit } from '@angular/core';
import { AnalyzeCardComponent } from "../../common-ui/analyze-card/analyze-card.component";
import { QuestionBoxComponent } from "../../common-ui/question-box/question-box.component";
import { RouterLink } from '@angular/router';
import { ContentService } from '../../services/content/content.service';
import { BannerDTO } from '../../services/responseModels/BannerDTO';
import { CommonModule } from '@angular/common';
import { AnalyzeDTO } from '../../services/responseModels/AnalyzeDTO';
import { PatientService } from '../../services/patient/patient.service';
import { AuthService } from '../../services/authorization/auth.service';
import { DoctorService } from '../../services/doctor/doctor.service';
import { ProcedureEventDTO } from '../../services/responseModels/ProcedureEventDTO';
import { EventCardComponent } from '../../common-ui/event-card/event-card.component';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [AnalyzeCardComponent, QuestionBoxComponent, RouterLink, CommonModule, FormsModule],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss',
  schemas: []
})
export class MainPageComponent implements OnInit, OnDestroy {
  banners: BannerDTO[] = [];
  patientAnalyzes: AnalyzeDTO[] = [];
  myEvents$!: Observable<ProcedureEventDTO[]>;
  currentBannerIndex: number = 0;
  interval: any;
  currentRole!: string;
  searchTerm: string | null = null;

  constructor(
    private readonly contentService: ContentService,
    private readonly authService: AuthService,
    private readonly patientService: PatientService,
    private readonly doctorService: DoctorService
  ) { }

  ngOnInit(): void {
    this.authService.$currentRole.subscribe({
      next: res => {
        this.currentRole = res;


        if (this.currentRole == "Patient") {
          this.patientService.GetMyAnalyzes()
            .subscribe({
              next: res => {
                this.patientAnalyzes = res;
              }
            })
        }

        if (this.currentRole == "Doctor") {
          this.loadEvents();
        }
      }
    })


    this.contentService.getBanners().subscribe({
      next: (res) => {
        this.banners = res;
        this.startBannerRotation();
      },
    });
  }

  ngOnDestroy(): void {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }

  loadEvents(): void {
    this.myEvents$ = this.doctorService.GetMyTodaysEvents();
  }

  startBannerRotation() {
    this.interval = setInterval(() => {
      this.currentBannerIndex =
        (this.currentBannerIndex + 1) % this.banners.length;
    }, 10000);
  }

  get currentBanner(): BannerDTO {
    return this.banners[this.currentBannerIndex];
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
