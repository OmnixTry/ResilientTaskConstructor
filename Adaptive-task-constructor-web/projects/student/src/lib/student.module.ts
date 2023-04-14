import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentComponent } from './student.component';
import { MainComponent } from './components/group/main/main.component';
import { SelectedGroupComponent } from './components/group/selected-group/selected-group.component';
import { ServiceModule } from 'projects/service/src/public-api';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'projects/shared/src/public-api';
import { CommonModule } from '@angular/common';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatRadioModule } from '@angular/material/radio';
import { TestRootComponent } from './components/completion/test-root/test-root.component';
import { OpenTaskComponent } from './components/completion/open-task/open-task.component';
import { MultipleChoiceComponent } from './components/completion/multiple-choice/multiple-choice.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { TestResultComponent } from 'projects/shared/src/lib/completion/components/test-result/test-result.component';
import { ResultComponent } from './components/completion/result/result.component';
import { StudTestResultsComponent } from './components/stud-test-results/stud-test-results.component';

export const routes: Routes = [
  {
    path: '',
    component: StudentComponent
  },
  {
    path: 'takeTest',
    component: TestRootComponent
  },
  {
    path: 'result',
    component: ResultComponent
  },
  {
    path: 'result-list',
    component: StudTestResultsComponent
  },
];

@NgModule({
  declarations: [
    StudentComponent,
    MainComponent,
    SelectedGroupComponent,
    TestRootComponent,
    OpenTaskComponent,
    MultipleChoiceComponent,
    ResultComponent,
    StudTestResultsComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    ServiceModule,
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    ReactiveFormsModule,
    MatInputModule,
    MatCheckboxModule,
    MatRadioModule,
  ],
  exports: [
    StudentComponent
  ]
})
export class StudentModule { }
