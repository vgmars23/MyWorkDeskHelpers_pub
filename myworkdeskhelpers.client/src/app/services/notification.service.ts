import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotificationType } from '../models/notification-type';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private apiUrl = 'https://localhost:7268/api/notifications/send';

  constructor(private http: HttpClient) {}


sendNotification(notification: { Type: NotificationType }) {
  const requestBody = {
    Type: notification.Type, 
  };
  console.log(requestBody);
    return this.http.post("https://localhost:7268/api/notifications/send", requestBody);
  }
}
