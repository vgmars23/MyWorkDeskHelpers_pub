import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';

export interface Birthday {
  id: string;
  name: string;
  dateTimestamp: number; 
  details: string;
  reminderDays: number;
}

@Injectable({
  providedIn: 'root'
})
export class BirthdayService {
  private apiUrl = 'https://localhost:5062/api/birthdays';
  private unreadCount = new BehaviorSubject<number>(0); 
  unreadCount$ = this.unreadCount.asObservable(); 

  setUnreadCount(count: number) {
    this.unreadCount.next(count);
  }

  getTodayBirthdaysCount(): Observable<number> {
    return this.http.get<Birthday[]>(`${this.apiUrl}/today`).pipe(
      map(birthdays => birthdays.length) // ✅ Преобразуем массив в число
    );
  }
  
  constructor(private http: HttpClient) {}

  getTodayBirthdays(): Observable<Birthday[]> {
    return this.http.get<Birthday[]>(`${this.apiUrl}/today`);
  }

  getBirthdays(): Observable<Birthday[]> {
    return this.http.get<Birthday[]>(this.apiUrl);
  }

  updateBirthday(birthday: Birthday): Observable<Birthday> {
    return this.http.put<Birthday>(`${this.apiUrl}/${birthday.id}`, birthday);
  }
  
  deleteBirthday(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  
  addBirthday(birthday: Birthday): Observable<Birthday> {
    return this.http.post<Birthday>(this.apiUrl, birthday);
  }
  createBirthday(birthday: Birthday): Observable<Birthday> {
    return this.http.post<Birthday>(this.apiUrl, birthday);
  }

  // sendTodayBirthdaysEmail(email: string) {
  //   return this.http.post(`${this.apiUrl}/sendEmail`, `"${email}"`, {  
  //     headers: { 'Content-Type': 'application/json' }
  //   });
  // }

  // sendNotification(email: string) {
  //   return this.http.post(`${this.apiUrl}/birthdays/sendEmail`, { email });
  // }
}
