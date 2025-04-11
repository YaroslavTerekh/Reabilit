import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public $showLoginModalSubject: ReplaySubject<boolean> = new ReplaySubject<boolean>();

  constructor() { }
}
