import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorDTO } from '../responseModels/DoctorDTO';
import { GetDoctor } from '../requestModels/GetDoctor';
import { config } from '../../config';
import { FreeSlotsDTO } from '../responseModels/FreeSlotsDTO';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  public GetDoctor(): Observable<DoctorDTO> {
    return this.http.get<DoctorDTO>(`${this.baseUrl}/Patient/doctor/get`);
  }

  public GetLoggedInUserDoctorFreeSchedule(): Observable<FreeSlotsDTO[]> {
    return this.http.get<FreeSlotsDTO[]>(`${this.baseUrl}/Patient/doctor/slots/get`);
  }
}
