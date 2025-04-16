import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PatientDTO } from '../responseModels/PatientDTO';
import { GetPatient } from '../requestModels/GetPatient';
import { config } from '../../config';

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
}
