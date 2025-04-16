import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { config } from '../../config';
import { LoginModel } from '../requestModels/LoginModel';
import { AuthToken } from '../responseModels/AuthToken';
import { HttpClient } from '@angular/common/http';
import { UserRoleDTO } from '../responseModels/UserRoleDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public $showLoginModalSubject: ReplaySubject<boolean> = new ReplaySubject<boolean>();
  public $isAuthorized: ReplaySubject<boolean> = new ReplaySubject<boolean>();
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) {
    let token = localStorage.getItem("token");
    if(token) {
      this.$isAuthorized.next(true);
    }
  }

  public login(request: LoginModel): Observable<AuthToken> {
    return this.http.post<AuthToken>(`${this.baseUrl}/Auth/login`, request);
  }

  public getCurrentUserRole(): Observable<UserRoleDTO> {
    return this.http.get<UserRoleDTO>(`${this.baseUrl}/Auth/role/get`);
  }
}
