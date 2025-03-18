import { Component, OnInit } from '@angular/core';
import { UserContactInfo, UserService } from '../../services/user.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  userInfo: UserContactInfo = { email: '', telegram: '', phone: '' };

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserInfo();
  }

  loadUserInfo(): void {
    this.userService.getUserContactInfo().subscribe(data => {
      this.userInfo = data;
    });
  }

  saveChanges(): void {
    this.userService.updateUserContactInfo(this.userInfo).subscribe(() => {
      alert("✅ Данные обновлены!");
    });
  }
}
