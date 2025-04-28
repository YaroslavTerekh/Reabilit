import { Component, CUSTOM_ELEMENTS_SCHEMA, OnDestroy, OnInit } from '@angular/core';
import { AnalyzeCardComponent } from "../../common-ui/analyze-card/analyze-card.component";
import { QuestionBoxComponent } from "../../common-ui/question-box/question-box.component";
import { RouterLink } from '@angular/router';
import { ContentService } from '../../services/content/content.service';
import { BannerDTO } from '../../services/responseModels/BannerDTO';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [AnalyzeCardComponent, QuestionBoxComponent, RouterLink, CommonModule],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss',
  schemas: []
})
export class MainPageComponent implements OnInit, OnDestroy {
  banners: BannerDTO[] = [];
  currentBannerIndex: number = 0;
  interval: any;

  constructor(private readonly contentService: ContentService) {}

  ngOnInit(): void {
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

  startBannerRotation() {
    this.interval = setInterval(() => {
      this.currentBannerIndex =
        (this.currentBannerIndex + 1) % this.banners.length;
    }, 10000); // 10 секунд
  }

  get currentBanner(): BannerDTO {
    return this.banners[this.currentBannerIndex];
  }
}
