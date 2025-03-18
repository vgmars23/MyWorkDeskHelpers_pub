import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { MatDialog } from '@angular/material/dialog';
import { Task, TaskService } from '../../services/task.service';
import { TaskDialogComponent } from './task-dialog/task-dialog.component';

@Component({
  selector: 'app-kanban-board',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css'],
})
export class KanbanBoardComponent implements OnInit {
  columns: { title: string; tasks: Task[] }[] = [
    { title: 'To Do', tasks: [] as Task[] }, 
    { title: 'In Progress', tasks: [] as Task[] }, 
    { title: 'Review', tasks: [] as Task[] }, 
    { title: 'Done', tasks: [] as Task[] }
  ];
  
  get connectedLists(): string[] {
    return this.columns.map((_, index) => `cdk-drop-list-${index}`);
  }
  constructor(private taskService: TaskService, private dialog: MatDialog) {}

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getTasks().subscribe(tasks => {
      this.columns.forEach(column => column.tasks = []);
      tasks.forEach(task => {
        const column = this.columns.find(c => c.title === task.board);
        if (column) column.tasks.push(task);
      });
    });
  }

  onDrop(event: CdkDragDrop<Task[]>) {
    if (event.previousContainer !== event.container) {
      const task = event.previousContainer.data[event.previousIndex];
      task.board = this.columns.find(c => c.tasks === event.container.data)?.title || 'To Do';

      this.taskService.updateTask(task).subscribe(() => {
        transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
      });
    } else {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    }
  }

  openCreateTaskDialog(columnIndex: number) {
    const dialogRef = this.dialog.open(TaskDialogComponent, { 
      data: { isEdit: false, task: { title: '', board: this.columns[columnIndex].title, description: '' } }
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result && result.id) {
        this.loadTasks(); 
      }
    });
  }
  
  

  openEditTaskDialog(columnIndex: number, taskIndex: number) {
    const task = this.columns[columnIndex].tasks[taskIndex];
  
    const dialogRef = this.dialog.open(TaskDialogComponent, {
      data: { task }
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadTasks(); 
      }
    });
  }
  
}
