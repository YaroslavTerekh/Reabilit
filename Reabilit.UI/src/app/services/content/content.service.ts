import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CityDTO } from '../responseModels/CityDTO';
import { config } from '../../config';

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
}
