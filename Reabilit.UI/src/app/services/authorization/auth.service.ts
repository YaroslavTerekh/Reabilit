import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { config } from '../../config';
import { LoginModel } from '../requestModels/LoginModel';
import { AuthToken } from '../responseModels/AuthToken';
import { HttpClient } from '@angular/common/http';
import { UserRoleDTO } from '../responseModels/UserRoleDTO';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public $showLoginModalSubject: ReplaySubject<boolean> = new ReplaySubject<boolean>();
  public $showNotificationsModalSubject: ReplaySubject<boolean> = new ReplaySubject<boolean>();
  public $isAuthorized: ReplaySubject<boolean> = new ReplaySubject<boolean>();
  public $currentRole: ReplaySubject<string> = new ReplaySubject<string>();
  private readonly baseUrl: string = config.apiUrl;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
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

  public logOut(): void {
    localStorage.removeItem("token");
    localStorage.removeItem("expires");

    this.$isAuthorized.next(false);
    this.$currentRole.next("");

    this.router.navigate(['']);
  }
}
