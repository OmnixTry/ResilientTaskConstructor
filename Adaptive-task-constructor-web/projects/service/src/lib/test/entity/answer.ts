export interface Answer {
    id: number;
    testTaskId: number;
    taskOptionId: number | null;
    value: string | null;
}