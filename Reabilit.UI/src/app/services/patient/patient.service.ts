import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PatientDTO } from '../responseModels/PatientDTO';
import { GetPatient } from '../requestModels/GetPatient';
import { config } from '../../config';
import { GetDoctor } from '../requestModels/GetDoctor';
import { DoctorDTO } from '../responseModels/DoctorDTO';
import { AddPatientProcedureEvent } from '../requestModels/AddPatientProcedureEvent';
import { ProcedureEventDTO } from '../responseModels/ProcedureEventDTO';
import { CancelEvent } from '../requestModels/CancelEvent';
import { AnalyzeDTO } from '../responseModels/AnalyzeDTO';

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

  public AddProcedureEvent(req: AddPatientProcedureEvent): Observable<any> {
    return this.http.post(`${this.baseUrl}/Patient/doctor/slots/reserve`, req);
  }

  public GetMyEvents(): Observable<ProcedureEventDTO[]> {
    return this.http.get<ProcedureEventDTO[]>(`${this.baseUrl}/Patient/events/get`);
  }

  public CancelEvent(request: CancelEvent): Observable<any> {
    return this.http.post(`${this.baseUrl}/Patient/events/cancel`, request);
  }

  public GetMyAnalyzes(): Observable<AnalyzeDTO[]> {
    return this.http.get<AnalyzeDTO[]>(`${this.baseUrl}/Patient/analyzes/get`)
  }
}
