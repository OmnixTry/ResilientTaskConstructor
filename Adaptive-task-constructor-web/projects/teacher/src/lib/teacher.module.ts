import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeacherComponent } from './teacher.component';
import { MainGroupComponent } from './group/main/main.component';
import { GroupEditFormComponent } from './group/group-edit-form/group-edit-form.component';
import { ServiceModule } from 'projects/service/src/public-api';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'projects/shared/src/public-api';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTable, MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckbox, MatCheckboxModule } from '@angular/material/checkbox';
import { AddUserDialogComponent } from './group/add-user-dialog/add-user-dialog.component';
import { AddTestDialogComponent } from './group/add-test-dialog/add-test-dialog.component';
import { CreateGroupComponent } from './group/create-group/create-group.component';
import { CreateTestComponent } from './test/create-test/create-test.component';
import { TestEditFormComponent } from './test/test-edit-form/test-edit-form.component';
import { TestMainComponent } from './test/test-main/test-main.component';
import { OpenTaskComponent } from './test/tasks/open-task/open-task.component';
import { TaskGeneralComponent } from './test/tasks/task-general/task-general.component';
import { TaskOptionsComponent } from './test/tasks/task-options/task-options.component';
import { QuizzTaskComponent } from './test/tasks/quizz-task/quizz-task.component';
import { MatSelect, MatSelectModule } from '@angular/material/select';
import { ConstructorComponent } from './test/constructor/constructor.component';
import { RightPanelComponent } from './test/right-panel/right-panel.component';
import { StudTestResultsComponent } from 'projects/service/src/lib/completion/components/stud-test-results/stud-test-results.component';
import { TestResultsComponent } from './completion/test-results/test-results.component';

export const routes: Routes = [
  {
    path: '',
    component: TeacherComponent
  },
  {
    path: 'group',
    component: MainGroupComponent
  },
  {
    path: 'group/new',
    component: CreateGroupComponent
  },
  {
    path: 'test',
    component: TestMainComponent
  },
  {
    path: 'test/new',
    component: ConstructorComponent
  },
  {
    path: 'student-results',
    component: StudTestResultsComponent
  },
  {
    path: 'test-results',
    component: TestResultsComponent
  },
];

@NgModule({
  declarations: [
    TeacherComponent,
    MainGroupComponent,
    GroupEditFormComponent,
    AddUserDialogComponent,
    AddTestDialogComponent,
    CreateGroupComponent,
    CreateTestComponent,
    TestEditFormComponent,
    TestMainComponent,
    OpenTaskComponent,
    TaskGeneralComponent,
    TaskOptionsComponent,
    QuizzTaskComponent,
    ConstructorComponent,
    RightPanelComponent,
    TestResultsComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    ServiceModule,
    CommonModule,
    SharedModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatTabsModule,
    MatTableModule,
    MatDialogModule,
    MatCheckboxModule,
    MatSelectModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    TeacherComponent
  ]
})
export class TeacherModule { }
