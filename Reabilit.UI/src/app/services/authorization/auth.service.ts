import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { config } from '../../config';
import { LoginModel } from '../requestModels/LoginModel';
import { AuthToken } from '../responseModels/AuthToken';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public $showLoginModalSubject: ReplaySubject<boolean> = new ReplaySubject<boolean>();
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  public login(request: LoginModel): Observable<AuthToken> {
    return this.http.post<AuthToken>(`${this.baseUrl}/Auth/login`, request);
  }
}
