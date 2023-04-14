import { AfterViewInit, Component, ContentChild, Input, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';
import { MatFormField, MatFormFieldControl } from '@angular/material/form-field';
import { SmartMessages, ValidationMessages } from '../../enum/validation-messages.enum';

@Component({
  selector: 'lib-form-field',
  templateUrl: './form-field.component.html',
  styleUrls: ['./form-field.component.scss']
})
export class FormFieldComponent implements OnInit, AfterViewInit {
  @Input() label: string;
  @Input() control: AbstractControl;
  @Input() appearance: 'legacy' | 'fill' | 'standard' | 'outline' = 'outline';
  @Input() errorMessages: { [key: string]: string }

  @ContentChild(MatFormFieldControl, { static: true })
  public formFieldControl: MatFormFieldControl<any>;

  @ViewChild('materialFormField', { static: true })
  public matFormField: MatFormField;

  validationMessages = new ValidationMessages();
  smartMessages = new SmartMessages();

  constructor() { }

  get error() {
    if (this.control?.errors) {
      return Object.keys(this.control.errors)[0];
    }
    return '';
  }

  get errorMessage(): string {
    if (this.error) {
      let message = !!this.errorMessages ? this.errorMessages[this.error] : null;
      if ((this.smartMessages as any)[this.error]) {
        message = (this.smartMessages as any)[this.error](this.control.errors[this.error]);
      }
      return message ?? (this.validationMessages as any)[this.error];
    }
    return '';
  }

  public ngOnInit() {
    this.matFormField._control = this.formFieldControl;
  }

  public ngAfterViewInit() {
    this.matFormField._control = this.formFieldControl;
  }

}
