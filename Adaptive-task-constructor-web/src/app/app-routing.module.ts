import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'projects/auth/src/lib/guards/auth.guard';
import { StudentGuard, TeacherGuard } from 'projects/auth/src/lib/guards/role.guard';
import { AuthModule } from 'projects/auth/src/public-api';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';


const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('projects/auth/src/public-api').then(m => m.AuthModule),
  },
  {
    path: '',
    component: AppComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'teacher',
    loadChildren: () => import('projects/teacher/src/public-api').then(m => m.TeacherModule),
    canActivate: [AuthGuard, TeacherGuard]
  },
  {
    path: 'student',
    loadChildren: () => import('projects/student/src/public-api').then(m => m.StudentModule),
    canActivate: [AuthGuard, StudentGuard]
  },
  {
    path: '',
    loadChildren: () => import('projects/service/src/public-api').then(m => m.ServiceModule),
    canActivate: [AuthGuard]
  },
  {
    path: '',
    loadChildren: () => import('projects/error-pages/src/public-api').then(m => m.ErrorPagesModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), AuthModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
