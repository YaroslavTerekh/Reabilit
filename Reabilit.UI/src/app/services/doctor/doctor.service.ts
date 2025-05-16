import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorDTO } from '../responseModels/DoctorDTO';
import { GetDoctor } from '../requestModels/GetDoctor';
import { config } from '../../config';
import { FreeSlotsDTO } from '../responseModels/FreeSlotsDTO';
import { ProcedureEventDTO } from '../responseModels/ProcedureEventDTO';
import { PatientDTO } from '../responseModels/PatientDTO';
import { ModifyDoctorInfo } from '../requestModels/ModifyDoctorInfo';
import { AnalyzeDTO } from '../responseModels/AnalyzeDTO';
import { AddEventResult } from '../requestModels/AddEventResult';
import { AddTreatmentRecommendation } from '../requestModels/AddTreatmentRecommendation';
import { AddDoctorScheduleRequest } from '../requestModels/AddDoctorScheduleRequest';
import { GetDoctorSlotsRequest } from '../requestModels/GetDoctorSlotsRequest';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  public AddSchedule(request: AddDoctorScheduleRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/Doctor/schedule/add`, request);
  }

  public GetDoctorSchedule(request: GetDoctorSlotsRequest): Observable<FreeSlotsDTO[]> {
    return this.http.post<FreeSlotsDTO[]>(`${this.baseUrl}/DoctorSchedule/slots/get`, request);
  }

    public DeleteDoctorSchedule(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/DoctorSchedule/slots/delete/${id}`);
  }

  public GetMyPatients(): Observable<PatientDTO[]> {
    return this.http.get<PatientDTO[]>(`${this.baseUrl}/Doctor/my-patients/get`);
  }

  public GetMyEvents(): Observable<ProcedureEventDTO[]> {
    return this.http.get<ProcedureEventDTO[]>(`${this.baseUrl}/Doctor/my-events/get`);
  }
  
  public GetMyTodaysEvents(): Observable<ProcedureEventDTO[]> {
    return this.http.get<ProcedureEventDTO[]>(`${this.baseUrl}/Doctor/events/today/get`);
  }

  public GetMyAccount(): Observable<DoctorDTO> {
    return this.http.get<DoctorDTO>(`${this.baseUrl}/Doctor/info/get`);
  }

  public GetLoggedInUserDoctorFreeSchedule(): Observable<FreeSlotsDTO[]> {
    return this.http.get<FreeSlotsDTO[]>(`${this.baseUrl}/Patient/doctor/slots/get`);
  }

  public ModifyDoctorInfo(req: ModifyDoctorInfo): Observable<DoctorDTO> {
    return this.http.put<DoctorDTO>(`${this.baseUrl}/Doctor/info/modify`, req);
  }

  public AddAnalyze(request: FormData): Observable<AnalyzeDTO[]> {
    return this.http.post<AnalyzeDTO[]>(`${this.baseUrl}/Doctor/analyzes/add`, request);
  }

  public AddEventResult(request: AddEventResult): Observable<any> {
    return this.http.patch(`${this.baseUrl}/Doctor/events/result/change`, request);
  }
  
  public AddTreatmentRecommendation(request: AddTreatmentRecommendation): Observable<any> {
    return this.http.post(`${this.baseUrl}/Doctor/treatment/add`, request);
  }

  public DeleteAnalyze(id: string): Observable<AnalyzeDTO[]> {
    return this.http.post<AnalyzeDTO[]>(`${this.baseUrl}/Doctor/analyzes/${id}/delete`, null);
  }
}
