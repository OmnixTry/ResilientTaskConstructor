import { TestTask } from "../../test/entity/testTask";
import { AttemptTask } from "./attemptTask";

export class Attempt {
    id: number;
    date: string;
    testId: number;
    studentId: string;
    score: number | null;
    maxScore: number | null;
    tasks: AttemptTask[];
}