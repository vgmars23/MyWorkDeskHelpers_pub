import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { MatListModule } from '@angular/material/list';
import { MatListItem } from '@angular/material/list';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [HeaderComponent],
  imports: [
    CommonModule, 
    MatListModule, 
    MatIconModule, 
    RouterModule
  ],
  exports: [HeaderComponent], 
})
export class HeaderModule {}
