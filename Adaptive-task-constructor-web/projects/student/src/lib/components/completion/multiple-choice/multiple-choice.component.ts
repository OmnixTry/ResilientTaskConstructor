import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatRadioChange } from '@angular/material/radio';
import { CompletionFormService } from 'projects/service/src/lib/completion/service/completion-form.service';
import { Option } from 'projects/service/src/lib/test/entity/option';
import { TestTask } from 'projects/service/src/lib/test/entity/testTask';

@Component({
  selector: 'multiple-choice',
  templateUrl: './multiple-choice.component.html',
  styleUrls: ['./multiple-choice.component.scss']
})
export class MultipleChoiceComponent implements OnInit {
  @Input() taskForm: FormGroup;
  @Input() task: TestTask;

  constructor(private formService: CompletionFormService) { }

  ngOnInit(): void {
  }

  onCheckChange(change: MatCheckboxChange, option: Option) {
    if (change.checked)
      this.formService.checkOption(this.taskForm, option.id);
    else
      this.formService.uncheckOption(this.taskForm, option.id);
  }

  onRadioChange(change: MatRadioChange) {
    this.formService.switchOption(this.taskForm, change.value.id);
  }
}
