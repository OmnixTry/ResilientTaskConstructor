import { Injectable } from "@angular/core";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ValidationMessages } from "projects/shared/src/lib/enum/validation-messages.enum";
import { numericValidator } from "projects/shared/src/lib/validators/numeric.validator";
import { Test } from "../../test/entity/test";
import { TestTask } from "../../test/entity/testTask";
import { Answer } from "../entity/answer";
import { Attempt } from "../entity/attempt";
import { AttemptTask } from "../entity/attemptTask";
import { TestCompletionService } from "./testCompletion.service";

@Injectable({ providedIn: 'root' })
export class CompletionFormService {
    constructor(private testCompletionService: TestCompletionService, private formBuilder: FormBuilder) { }

    createForm(test: Test) {
        return this.formBuilder.group({
            id: [0],
            date: [null],
            testId: [test.id],
            tasks: this.createTasksForm(test)
        });
    }

    checkOption(taskForm: FormGroup, optionId: number) {
        (taskForm.get('answers') as FormArray)
            .push(this.createAnswerForm(optionId, null));
    }

    uncheckOption(taskForm: FormGroup, optionId: number) {
        const arr = taskForm.get('answers') as FormArray;
        const control = arr.controls.filter(x => x.value.taskOptionId == optionId)[0];
        arr.removeAt(arr.controls.indexOf(control));
    }

    switchOption(taskForm: FormGroup, optionId: number) {
        const arr = taskForm.get('answers') as FormArray;
        if (arr.length != 0) {
            arr.removeAt(0);
        }
        this.checkOption(taskForm, optionId);
    }

    addTextAnswer(taskForm: FormGroup) {
        const arr = taskForm.get('answers') as FormArray;
        arr.push(this.createAnswerForm(null, ''));
    }

    createFormFromAttempt(attempt: Attempt, test: Test, disabled = false) {
        return this.formBuilder.group({
            id: [attempt.id],
            date: [attempt.date],
            testId: [attempt.testId],
            studentId: [attempt.studentId],
            tasks: this.createTasksFormFromAttempt(attempt, test, disabled),
            score: [attempt.score],
        });
    }

    private createTasksFormFromAttempt(attempt: Attempt, test: Test, disabled: boolean) {
        let taskForms = attempt.tasks.map(t => this.createTaskForm(t, test, disabled));
        const missingTasks = test.tasks.filter(t => attempt.tasks.filter(at => at.taskId == t.id).length == 0);
        taskForms = taskForms.concat(missingTasks.map(t => { return { id: 0, taskId: t.id, attemptId: 0, score: '0', answers: [] as Answer[], task: null } }).map(t => this.createTaskForm(t, test, disabled)));
        return this.formBuilder.array(taskForms);
    }

    private createTaskForm(attempt: AttemptTask, test: Test, disabled: boolean) {
        return this.formBuilder.group({
            id: [attempt.id],
            taskId: [attempt.taskId],
            attemptId: [attempt.attemptId],
            score: [{ value: attempt.score, disabled: disabled }, [Validators.min(0), Validators.max(test.tasks.find(task => task.id == attempt.taskId).score), numericValidator()]],
            answers: this.createAnswersFromAttempt(attempt),
        })
    }

    private createAnswersFromAttempt(task: AttemptTask) {
        const answers = task.answers.map(a => this.formBuilder.group({
            id: [a.id],
            taskOptionId: [a.taskOptionId],
            taskDtoId: [a.taskDtoId],
            value: [a.value],
            correct: [a.corect]
        }))
        return this.formBuilder.array(answers);
    }

    private createTasksForm(test: Test) {
        const taskForms = test.tasks.map(t => this.createTask(t));
        return this.formBuilder.array(taskForms);
    }

    private createTask(task: TestTask) {
        return this.formBuilder.group({
            id: [0],
            taskId: [task.id],
            attemptId: [0],
            score: [0],
            answers: this.formBuilder.array([]),
        });
    }

    private createAnswerForm(optionId: number = null, value: string = null) {
        return this.formBuilder.group({
            id: [0],
            taskOptionId: [optionId],
            taskDtoId: [0],
            value: [value],
            correct: [false]
        });
    }
}