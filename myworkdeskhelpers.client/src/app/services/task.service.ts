import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

export interface Task {
  id?: string;
  title: string;
  board: string; 
  description?: string;
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'https://localhost:5062/api/task'; 

  constructor(private http: HttpClient) {}

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }

  createTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.apiUrl, { 
      title: task.title, 
      board: task.board,
      description: task.description || '' 
    });
  } 

  updateTask(task: Task): Observable<void> {
    if (!task.id) {
      console.warn('Обновление невозможно: у задачи нет ID, возможно, она ещё не создана.');
      return throwError(() => new Error('Ошибка: у задачи нет ID, используйте createTask()!'));
    }
    return this.http.put<void>(`${this.apiUrl}/${task.id}`, task);
  }

  deleteTask(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  
}
