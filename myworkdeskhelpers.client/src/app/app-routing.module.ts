import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './common-ui/layout/layout.component';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
  { path: '', component: LayoutComponent, children: [
      { path: 'birthdays', loadChildren: () => import('./common-ui/birthday-calendar/birthday-calendar.module').then(m => m.BirthdayCalendarModule), canActivate: [AuthGuard] },
      { path: 'kanban', loadChildren: () => import('./common-ui/kanban-board/kanban-board.module').then(m => m.KanbanBoardModule), canActivate: [AuthGuard] },
      { path: 'settings', loadChildren: () => import('./common-ui/settings/settings.module').then(m => m.SettingsModule), canActivate: [AuthGuard] },
    ], canActivate: [AuthGuard] 
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
