import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CityDTO } from '../responseModels/CityDTO';
import { config } from '../../config';
import { DoctorClassDTO } from '../responseModels/DoctorClassDTO';
import { BannerDTO } from '../responseModels/BannerDTO';
import { GetUsersRequest } from '../requestModels/GetUsersRequest';
import { DoctorDTO } from '../responseModels/DoctorDTO';

@Injectable({
  providedIn: 'root'
})
export class ContentService {
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  public GetAllCities(): Observable<CityDTO[]> {
    return this.http.get<CityDTO[]>(`${this.baseUrl}/Content/cities/get`);
  }

  public GetAllDoctorClasses(): Observable<DoctorClassDTO[]> {
    return this.http.get<DoctorClassDTO[]>(`${this.baseUrl}/Content/doctor-classes/get`);
  }
  
  public getBanners(): Observable<BannerDTO[]> {
    return this.http.get<BannerDTO[]>(`${this.baseUrl}/Content/banners/get`);
  }

  public getDoctors(req: GetUsersRequest): Observable<DoctorDTO[]> {
    return this.http.post<DoctorDTO[]>(`${this.baseUrl}/Content/doctors/get`, req);
  }
}
