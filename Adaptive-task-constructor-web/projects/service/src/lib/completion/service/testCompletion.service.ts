import { Injectable } from "@angular/core";
import { Answer } from "../entity/answer";
import { Attempt } from "../entity/attempt";
import { TestCompletionRepo } from "../repo/testComplition.repository";

@Injectable({ providedIn: "root" })
export class TestCompletionService {
    constructor(private completionRepository: TestCompletionRepo) {

    }

    checkTest(attempt: Attempt) {
        return this.completionRepository.sendForCheck(attempt);
    }

    saveManualCheck(attempt: Attempt) {
        return this.completionRepository.updateTeacherCheck(attempt);
    }

    getCurrentStudentResults(testId: number) {
        return this.completionRepository.getCurrentStudentResults(testId);
    }

    getStudentResults(testId: number, studentId: string) {
        return this.completionRepository.getStudentResults(testId, studentId);
    }

    getFullAttempt(attemptId: number) {
        return this.completionRepository.getFullResult(attemptId);
    }

    getTestResults(testId: number, groupId: number) {
        return this.completionRepository.getGroupTestResults(testId, groupId);
    }

}