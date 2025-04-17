import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PatientDTO } from '../responseModels/PatientDTO';
import { GetPatient } from '../requestModels/GetPatient';
import { config } from '../../config';
import { GetDoctor } from '../requestModels/GetDoctor';
import { DoctorDTO } from '../responseModels/DoctorDTO';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }
  
  public GetUserPatient(req: GetPatient): Observable<PatientDTO> {
    return this.http.post<PatientDTO>(`${this.baseUrl}/Patient/get`, req);
  }

    public GetDoctor(): Observable<DoctorDTO> {
      return this.http.get<DoctorDTO>(`${this.baseUrl}/Patient/doctor/get`);
    }
}
