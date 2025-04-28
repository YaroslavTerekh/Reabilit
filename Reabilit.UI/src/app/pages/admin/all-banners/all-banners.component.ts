import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BannerDTO } from '../../../services/responseModels/BannerDTO';
import { ContentService } from '../../../services/content/content.service';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../../services/admin/admin.service';

@Component({
  selector: 'app-all-banners',
  imports: [CommonModule],
  templateUrl: './all-banners.component.html',
  styleUrl: './all-banners.component.scss'
})
export class AllBannersComponent implements OnInit {
  banners: BannerDTO[] = [];

  constructor(
    private readonly contentService: ContentService,
    private readonly adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.contentService.getBanners()
      .subscribe({
        next: res => this.banners = res
      })
  }

  deleteBanner(id: string): void {
    this.adminService.deleteBanner(id)
      .subscribe({
        next: res => {
          this.contentService.getBanners()
          .subscribe({
            next: res => this.banners = res
          })
        }
      })
  }
}