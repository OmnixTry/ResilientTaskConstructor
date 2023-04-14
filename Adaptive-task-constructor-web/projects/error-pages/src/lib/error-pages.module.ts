import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  {
    path: 'forbidden',
    component: ForbiddenComponent
  },
  {
    path: 'not-found',
    component: NotFoundComponent
  }
]


@NgModule({
  declarations: [
    ForbiddenComponent,
    NotFoundComponent
  ],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
  ]
})
export class ErrorPagesModule { }
