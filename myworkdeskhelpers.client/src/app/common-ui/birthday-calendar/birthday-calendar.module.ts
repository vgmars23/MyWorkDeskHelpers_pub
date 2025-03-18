import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BirthdayCalendarComponent } from './birthday-calendar.component';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BirthdayDialogComponent } from './birthday-dialog/birthday-dialog.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatRadioModule } from '@angular/material/radio';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';

@NgModule({
  declarations: [BirthdayCalendarComponent, BirthdayDialogComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    MatListModule, 
    MatRadioModule,
    MatIconModule,
    MatDividerModule,
    MatProgressSpinnerModule
  ],
  exports: [
    MatProgressSpinnerModule,
    MatListModule,
    MatRadioModule,
    MatIconModule,
    MatDividerModule,
    MatCardModule
  ]
})
export class BirthdayCalendarModule {}
