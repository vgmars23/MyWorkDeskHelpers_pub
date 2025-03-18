import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Birthday, BirthdayService } from '../../services/birthday.service';
import { BirthdayDialogComponent } from './birthday-dialog/birthday-dialog.component';
import { MatCalendar } from '@angular/material/datepicker';
import { NotificationService } from '../../services/notification.service';
import { NotificationType } from '../../models/notification-type';


@Component({
  selector: 'app-birthday-calendar',
  templateUrl: './birthday-calendar.component.html',
  styleUrls: ['./birthday-calendar.component.scss']
})


export class BirthdayCalendarComponent implements OnInit {
  @ViewChild(MatCalendar) calendar!: MatCalendar<Date>;
  birthdays: Birthday[] = [];
  todayBirthdays: Birthday[] = [];
  highlightedDates: Set<number> = new Set(); 
  isLoading: boolean = false; 
  unreadBirthdaysCount: number = 0;
  recipientEmail: string = '';
  recipientTelegram: string = '';
  NotificationType = NotificationType;

  constructor(private birthdayService: BirthdayService, 
    private notificationService: NotificationService,
    private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadBirthdays();
    this.loadTodayBirthdays();
  }

  loadBirthdays(): void {
    this.isLoading = true;

    this.birthdayService.getBirthdays().subscribe({
      next: (birthdays) => {
        this.birthdays = birthdays;
        this.highlightedDates = new Set(birthdays.map(b => b.dateTimestamp));
        this.refreshCalendar(); 
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false; 
        this.todayBirthdays = [];
        this.birthdayService.setUnreadCount(0);
      }
    });
  }

  loadTodayBirthdays(): void {
    this.birthdayService.getTodayBirthdays().subscribe({
      next: (birthdays) => {
        this.todayBirthdays = birthdays;
      },
      error: () => {
        this.todayBirthdays = [];
      }
    });
  }

  refreshCalendar() {
    if (this.calendar) {
      this.calendar.updateTodaysDate();
    }
  }

  formatDate(timestamp: number): string {
    if (!timestamp || isNaN(timestamp)) return "Ошибка даты"; 
  
    const birthdayDate = new Date(timestamp * 1000); 
    birthdayDate.setUTCHours(12, 0, 0, 0);
  
    return birthdayDate.toLocaleDateString("ru-RU", {
      day: "numeric",
      month: "long",
      year: "numeric",
    });
  }

  dateClass = (date: Date): string => {
    const timestamp = Math.floor(date.getTime() / 1000);

    return this.highlightedDates.has(timestamp) ? 'birthday-highlight' : '';
  };

  openBirthdayDialog(event: Date | null) {
    if (!event) return;

    const timestamp = Math.floor(event.getTime() / 1000);
    const birthdaysForDate = this.birthdays.filter(b => b.dateTimestamp === timestamp);

    this.dialog.open(BirthdayDialogComponent, {
      width: '800px',
      data: { date: timestamp, birthdays: birthdaysForDate }
    }).afterClosed().subscribe(() => {
      this.loadBirthdays();
    });
  }

  sendNotification(type: NotificationType) {
    const requestBody = {
      Type: type,
    };
  
    this.notificationService.sendNotification(requestBody).subscribe({
      next: () => alert(`Уведомление ${type} отправлено!`),
      error: () => alert("Ошибка при отправке уведомления.")
    });
  }
  
}
