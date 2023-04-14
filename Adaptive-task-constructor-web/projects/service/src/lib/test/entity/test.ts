import { TestBed } from "@angular/core/testing";
import { TestTask } from "./testTask";

export interface Test {
    id: number;
    name: string;
    topic: string;
    tasks: TestTask[]
}




