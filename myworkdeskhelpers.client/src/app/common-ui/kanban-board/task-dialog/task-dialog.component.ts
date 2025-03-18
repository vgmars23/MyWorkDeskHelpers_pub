import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Task, TaskService } from '../../../services/task.service';

@Component({
  selector: 'app-task-dialog',
  templateUrl: './task-dialog.component.html',
  styleUrls: ['./task-dialog.component.css'],
})
export class TaskDialogComponent {
  task: Task;

  constructor(
    public dialogRef: MatDialogRef<TaskDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private taskService: TaskService
  ) {
    this.task = { ...data.task }; // Копируем данные, чтобы не менять оригинал
  }

  save() {
    if (this.task.id) {
      this.taskService.updateTask(this.task).subscribe({
        next: () => {
          this.dialogRef.close(this.task); // ✅ Теперь закрывается после успешного запроса
        },
        error: (err) => console.error('Ошибка при обновлении задачи:', err)
      });
    } else {
      this.taskService.createTask(this.task).subscribe({
        next: (newTask) => {
          this.dialogRef.close(newTask); // ✅ Теперь закрывается после успешного запроса
        },
        error: (err) => console.error('Ошибка при создании задачи:', err)
      });
    }
  }
  

  delete() {
    if (confirm('Вы уверены, что хотите удалить задачу?')) {
      this.taskService.deleteTask(this.task.id!).subscribe(() => {
        this.dialogRef.close({ deleted: true });
      });
    }
  }

  cancel() {
    this.dialogRef.close();
  }
}
