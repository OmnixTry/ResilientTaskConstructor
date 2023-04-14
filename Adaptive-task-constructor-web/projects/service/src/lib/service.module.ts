import { NgModule } from '@angular/core';
import { ServiceComponent } from './service.component';
import { GroupListComponent } from './group/component/group-list/group-list.component';
import { CommonModule, DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { SharedModule } from 'projects/shared/src/public-api';
import { TestListComponent } from './test/components/test-list/test-list.component';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { TestResultComponent } from './completion/components/test-result/test-result.component';
import { StudTestResultsComponent } from './completion/components/stud-test-results/stud-test-results.component';
import { RouterModule, Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'result',
    component: TestResultComponent
  },
  {
    path: 'stud-result',
    component: StudTestResultsComponent
  }
];

@NgModule({
  declarations: [
    ServiceComponent,
    GroupListComponent,
    TestListComponent,
    TestResultComponent,
    StudTestResultsComponent
  ],
  imports: [
    CommonModule,
    MatButtonModule,
    SharedModule,
    MatInputModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    ServiceComponent,
    GroupListComponent,
    TestListComponent,
    TestResultComponent,
  ],
  providers: [
    DatePipe
  ]
})
export class ServiceModule { }
