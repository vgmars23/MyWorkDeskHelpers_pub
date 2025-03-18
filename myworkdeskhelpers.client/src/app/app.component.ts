import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient, private router: Router) {}

  checkSession() {
    const expiry = localStorage.getItem('tokenExpiry');
    if (expiry && Date.now() >= parseInt(expiry)) {
      localStorage.removeItem('authToken'); 
      localStorage.removeItem('tokenExpiry');
      this.router.navigate(['/login']); 
    }
  }

  ngOnInit() {
    setInterval(() => this.checkSession(), 60000); 
  }


  title = 'myworkdeskhelpers.client';
}
