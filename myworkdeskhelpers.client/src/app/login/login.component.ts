import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private router: Router) {}


  onLogin() {
    if (this.username === 'admin' && this.password === '1234') {
      const token = 'your-jwt-token'; 
      localStorage.setItem('authToken', token);
      localStorage.setItem('tokenExpiry', (Date.now() + 3600000).toString()); 
      this.router.navigate(['/']);
    } else {
      alert('Неверный логин или пароль');
    }
  }
  
}
