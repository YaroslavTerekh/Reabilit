import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { config } from '../../config';
import { RegisterDoctor } from '../requestModels/RegisterDoctor';
import { Observable } from 'rxjs';
import { BannerDTO } from '../responseModels/BannerDTO';
import { DeleteBanner } from '../requestModels/DeleteBanner';
import { GetUsersRequest } from '../requestModels/GetUsersRequest';
import { PatientDTO } from '../responseModels/PatientDTO';
import { RegisterPatient } from '../requestModels/RegisterPatient';
import { DoctorDTO } from '../responseModels/DoctorDTO';
import { AssingDoctorToPatient } from '../requestModels/AssingDoctorToPatient';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  public registerDoctor(request: RegisterDoctor): Observable<any> {
    return this.http.post(`${this.baseUrl}/Admin/regiter/doctor`, request);
  }
  
  public registerPatient(request: RegisterPatient): Observable<any> {
    return this.http.post(`${this.baseUrl}/Admin/regiter/patient`, request);
  }

  public addBanner(request: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}/Banner/add`, request);
  }

  public deleteBanner(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Banner/delete/${id}`);
  }

  public getPatientsByText(request: GetUsersRequest): Observable<PatientDTO[]> {
    return this.http.post<PatientDTO[]>(`${this.baseUrl}/Patient/patients/get`, request)
  }

  public getDoctorsByText(request: GetUsersRequest): Observable<DoctorDTO[]> {
    return this.http.post<DoctorDTO[]>(`${this.baseUrl}/Doctor/doctors/get`, request)
  }

  public assignDoctorToPatient(request: AssingDoctorToPatient): Observable<any> {
    return this.http.post(`${this.baseUrl}/Admin/patient/attach-doctor`, request)
  }
}
