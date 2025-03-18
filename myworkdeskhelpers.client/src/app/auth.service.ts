import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private router: Router) {}

  logout() {
    localStorage.removeItem('authToken'); 
    localStorage.removeItem('tokenExpiry'); 
    this.router.navigate(['/login']); 
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('authToken');
    const expiry = localStorage.getItem('tokenExpiry');
    return !!token && !!expiry && Date.now() < parseInt(expiry);
  }
}
