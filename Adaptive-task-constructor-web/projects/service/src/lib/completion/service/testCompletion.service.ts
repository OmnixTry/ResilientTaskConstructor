import { Injectable } from "@angular/core";
import { Answer } from "../entity/answer";
import { switchMap, tap, combineLatest } from 'rxjs/operators';
import { Attempt } from "../entity/attempt";
import { TestCompletionRepo } from "../repo/testComplition.repository";
import { AuthenticationService } from "projects/auth/src/lib/services/authentication.service";

@Injectable({ providedIn: "root" })
export class TestCompletionService {
    constructor(private completionRepository: TestCompletionRepo,
        private authService: AuthenticationService) {

    }

    checkTest(attempt: Attempt) {
        return this.completionRepository.sendForCheck(attempt);
    }

    checkTestAsync(attempt: Attempt) {
        return this.authService.getUserId()
            .pipe(switchMap((userId) => {
                attempt.studentId = userId.userId;
                return this.completionRepository.sendForCheckAsync(attempt);
            }));
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