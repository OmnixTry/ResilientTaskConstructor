import { Option } from './option';
import { AnswerType, TestTaskType } from './testType';

export interface TestTask {
    id: number;
    testId: number;
    type: TestTaskType;
    answerType: AnswerType;
    score: number;
    description: string;
    question: string;
    gapIndex: number | null;
    gapText: string;
    correct: boolean;
    allowMultiple: boolean;
    taskOptions: Option[];
}