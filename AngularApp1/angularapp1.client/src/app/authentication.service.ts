import { Injectable } from '@angular/core';
import Constants, { User, UserDetails } from './types';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private readonly baseUrl = Constants.API;
  constructor(private http: HttpClient) { }

  register(userDetails: UserDetails): Observable<any> {
    return this.http.post(`${this.baseUrl}/Accounts/register`, userDetails);
    
  }

  login(user: User): Observable<any> {
    return this.http.post(`${this.baseUrl}/Accounts/login`, user);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.baseUrl}/Accounts/logout`, null);
  }
}
