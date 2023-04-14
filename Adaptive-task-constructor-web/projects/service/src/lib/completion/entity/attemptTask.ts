import { TestTask } from "../../test/entity/testTask";
import { Answer } from "./answer";

export class AttemptTask {
    id: number;
    taskId: number;
    attemptId: number;
    score: string;
    task: TestTask;
    answers: Answer[];
}