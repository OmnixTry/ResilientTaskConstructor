import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';

@Component({
  selector: 'ui-validation-error',
  templateUrl: './validation-error.component.html',
  styleUrls: ['./validation-error.component.scss']
})
export class ValidationErrorComponent implements OnInit {

  @Input() control: AbstractControl | null;
  @Input() error: string;


  constructor() { }

  get hasError(){
    let x =this.control?.hasError(this.error); 
    return x;
  }

  ngOnInit(): void {
  }

}
