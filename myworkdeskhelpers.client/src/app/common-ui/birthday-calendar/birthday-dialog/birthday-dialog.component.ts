import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Birthday, BirthdayService } from '../../../services/birthday.service';

@Component({
  selector: 'app-birthday-dialog',
  templateUrl: './birthday-dialog.component.html',
  styleUrls: ['./birthday-dialog.component.scss']
})
export class BirthdayDialogComponent {
  newBirthdayName: string = '';
  newBirthdayDetails: string = '';
  newBirthdayReminder: number = 1;

  constructor(
    public dialogRef: MatDialogRef<BirthdayDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { date: number; birthdays: Birthday[] }, // 🟢 Теперь дата — timestamp
    private birthdayService: BirthdayService
  ) {}

/** ✅ Добавление новой записи (сохранение в БД) */
addBirthday() {
  if (!this.newBirthdayName.trim()) return;

  const selectedDateUTC = new Date(this.data.date);
  selectedDateUTC.setUTCHours(12, 0, 0, 0); // 🕛 Устанавливаем UTC-12, чтобы избежать смещения  

  const newBirthday: Birthday = {
    id: '', // ✅ MongoDB создаст _id автоматически
    name: this.newBirthdayName,
    dateTimestamp: this.data.date, // ⏳ Unix Timestamp (в секундах)
    details: this.newBirthdayDetails,
    reminderDays: this.newBirthdayReminder,
  };

  this.birthdayService.addBirthday(newBirthday).subscribe((added) => {
    this.data.birthdays.push(added);
    this.newBirthdayName = '';
    this.newBirthdayDetails = '';
    this.newBirthdayReminder = 1;
  });
}


  /** ✅ Обновление записи */
  updateBirthday(birthday: Birthday) {
    this.birthdayService.updateBirthday(birthday).subscribe(() => {
      console.log('Обновлено:', birthday);
    });
  }

  /** ✅ Удаление записи */
  deleteBirthday(id: string) {
    if (!id) return;

    this.birthdayService.deleteBirthday(id).subscribe(() => {
      this.data.birthdays = this.data.birthdays.filter(b => b.id !== id);
    });
  }

  /** ✅ Закрытие диалога */
  close(): void {
    this.dialogRef.close('reload'); // Обновляем данные после закрытия
  }
}
