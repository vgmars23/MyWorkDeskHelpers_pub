import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingsComponent } from './settings.component';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [SettingsComponent],
  imports: [
    CommonModule,
    MatCardModule,        
    MatFormFieldModule,   
    MatInputModule,       
    MatButtonModule,    
    FormsModule, 
  ],
})
export class SettingsModule {}
