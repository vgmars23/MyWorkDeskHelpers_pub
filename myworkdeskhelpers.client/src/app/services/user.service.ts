import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UserContactInfo {
  email: string;
  telegram: string;
  phone: string;
}

@Injectable({ providedIn: 'root' })
export class UserService {
  private apiUrl = 'https://localhost:5062/api/user'; 

  constructor(private http: HttpClient) {}

  getUserContactInfo(): Observable<UserContactInfo> {
    return this.http.get<UserContactInfo>(`${this.apiUrl}/contact-info`);
  }

  updateUserContactInfo(userInfo: UserContactInfo): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/contact-info`, userInfo);
  }
}
