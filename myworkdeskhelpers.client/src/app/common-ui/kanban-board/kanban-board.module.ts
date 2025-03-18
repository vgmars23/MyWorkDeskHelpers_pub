import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KanbanBoardComponent } from './kanban-board.component';
import { TaskDialogComponent } from './task-dialog/task-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [
    KanbanBoardComponent,
    TaskDialogComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,   
    MatDialogModule,   
    MatFormFieldModule, 
    MatInputModule,    
    MatButtonModule,   
    FormsModule,
    DragDropModule,  
    ReactiveFormsModule
  ],
  exports: [KanbanBoardComponent]
})
export class KanbanBoardModule { }
