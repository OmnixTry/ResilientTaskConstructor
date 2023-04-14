import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedComponent } from './shared.component';
import { ValidationErrorComponent } from './ui/validation-error/validation-error.component';
import { EnumArrayPipePipe } from './pipes/enum-array-pipe.pipe';
import { ValidatedFormComponent } from './ui/validated-form/validated-form.component';
import { FormFieldComponent } from './components/form-field/form-field.component';
import { MatCommonModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectiveListComponent } from './components/selective-list/selective-list.component';
import { MatButtonModule } from '@angular/material/button';
import { TestResultComponent } from './completion/components/test-result/test-result.component';



@NgModule({
  declarations: [
    SharedComponent,
    ValidationErrorComponent,
    EnumArrayPipePipe,
    ValidatedFormComponent,
    FormFieldComponent,
    SelectiveListComponent,
    TestResultComponent
  ],  
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule
  ],
  exports: [
    SharedComponent,
    ValidationErrorComponent,
    EnumArrayPipePipe,
    FormFieldComponent,
    SelectiveListComponent
  ],
  providers: [EnumArrayPipePipe]
})
export class SharedModule { }
