import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { BirthdayService } from '../services/birthday.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  unreadBirthdaysCount: number = 0; 

  constructor(private authService: AuthService, private birthdayService: BirthdayService) {}

  ngOnInit(): void {
    this.birthdayService.getTodayBirthdaysCount().subscribe((count: number) => {
      this.unreadBirthdaysCount = count;
    });
  }

  onLogout() {
    this.authService.logout();
  }
}
