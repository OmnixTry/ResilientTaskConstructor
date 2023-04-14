import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Test } from "../entity/test";
import { TestRepository } from '../repo/test.repository';

@Injectable({ providedIn: 'root' })
export class TestService {
    constructor(private testRepository: TestRepository) { }

    updateTest(test: Test): Observable<Test> {
        if (test.id == 0) {
            return this.testRepository.createTest(test);
        }
        else {
            return this.testRepository.updateTest(test);
        }
    }

    deleteTest(testName: string) {
        return this.testRepository.deleteTest(testName);
    }

    getFullTest(testName: string) {
        return this.testRepository.getFullTest(testName);
    }

    getMysteryTest(id: number) {
        return this.testRepository.getMysteryTest(id);
    }
}