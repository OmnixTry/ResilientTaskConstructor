import { Injectable } from "@angular/core";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Test } from "../entity/test";
import { Option } from "../entity/option";
import { TestTask } from "../entity/testTask";
import { AnswerType, TestTaskType } from "../entity/testType";
import { atLeastOneValidator } from "projects/shared/src/lib/validators/atLeastOne.validator";

@Injectable({ providedIn: 'root' })
export class TestFormService {
    constructor(private formBuilder: FormBuilder) {
    }

    createFormFromObject(test: Test) {
        return this.formBuilder.group({
            id: [test.id,],
            name: [test.name, Validators.required],
            topic: [test.topic, Validators.required],
            tasks: this.createTaskArray(test)
        });
    }

    createEmptyForm() {
        const emptyTest: Test = this.createEmptyTest();
        return this.createFormFromObject(emptyTest);
    }

    importTask(form: FormGroup, task: TestTask) {
        task.id = 0;
        for (let i = 0; i < task.taskOptions.length; i++) {
            const element = task.taskOptions[i];
            element.id = 0;
        }
        this.addTask(form, task);
    }

    addEmptyTask(testForm: FormGroup) {
        const emptyTask: TestTask = {
            id: 0,
            testId: 0,
            type: TestTaskType.General,
            answerType: AnswerType.Open,
            score: 1,
            description: 'Answer the question',
            question: '',
            gapIndex: null,
            gapText: '',
            taskOptions: [this.createEmptyOption()],
            correct: false,
            allowMultiple: false,
        };

        this.addTask(testForm, emptyTask);
    }

    addTask(testForm: FormGroup, task: TestTask) {
        const testArr = testForm.get('tasks') as FormArray;
        testArr.push(this.createTaskControl(task));
    }

    addEmptyOption(taskForm: FormArray) {
        const emptyOption: Option = this.createEmptyOption();
        this.addOption(taskForm, emptyOption);
    }

    addOption(taskForm: FormArray, option: Option) {
        taskForm.push(this.createOptionControl(option));
    }

    private createEmptyOption(): Option {
        return {
            id: 0,
            testTaskId: 0,
            value: 'Value',
            correct: true,
        }
    }

    private createTaskArray(test: Test): FormArray {
        var controls = test.tasks.map(t => this.createTaskControl(t));
        return this.formBuilder.array(controls);
    }

    private createTaskControl(task: TestTask): FormGroup {
        return this.formBuilder.group({
            id: [task.id],
            testId: [task.testId],
            type: [task.type],
            answerType: [task.answerType],
            score: [task.score, [Validators.min(0), Validators.required],],
            description: [task.description, Validators.required],
            question: [task.question, Validators.required],
            gapIndex: [task.gapIndex],
            gapText: [task.gapText],
            allowMultiple: [task.allowMultiple],
            taskOptions: this.createOptionArray(task.taskOptions),
        });
    }

    private createOptionArray(options: Option[]): FormArray {
        var controls = options.map(o => this.createOptionControl(o));
        return this.formBuilder.array(controls, atLeastOneValidator(v => v.correct));
    }

    private createOptionControl(option: Option) {
        return this.formBuilder.group({
            id: [option.id],
            testTaskId: [option.testTaskId],
            value: [option.value],
            correct: [option.correct],
        })
    }

    private createEmptyTest(): Test {
        return { id: 0, name: 'New Test', topic: '', tasks: [] };
    }
}