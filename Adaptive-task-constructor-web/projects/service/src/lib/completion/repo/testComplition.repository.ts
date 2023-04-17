import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BeUrlService } from "projects/shared/src/lib/services/be-url.service";
import { HttpRequestService } from "projects/shared/src/public-api";
import { Attempt } from "../entity/attempt";
import { StudentResult } from "../entity/studentResult";

@Injectable({ providedIn: "root" })
export class TestCompletionRepo extends HttpRequestService {
    constructor(http: HttpClient, beUrlService: BeUrlService) {
        super(http, beUrlService)
    }

    sendForCheck(attempt: Attempt) {
        return this.makeAuthenticatedPost<Attempt>(`api/tests/${attempt.testId}/check`, attempt);
    }

    sendForCheckAsync(attempt: Attempt) {
        var route = this.beUrlService.checkMessengerUrlAddress + '/api/message';
        return this.http.post<Attempt>(route, attempt, this.authOptions);
    }

    updateTeacherCheck(attempt: Attempt) {
        return this.makeAuthenticatedPost<Attempt>(`api/tests/${attempt.testId}/results`, attempt);
    }

    getCurrentStudentResults(testId: number) {
        return this.makeAuthenticatedGet<Attempt[]>(`api/tests/${testId}/results/user`);
    }

    getStudentResults(testId: number, studentId: string) {
        return this.makeAuthenticatedGet<Attempt[]>(`api/tests/${testId}/results/user`, { userId: studentId });
    }

    getGroupTestResults(testId: number, groupId: number) {
        return this.makeAuthenticatedGet<StudentResult[]>(`api/tests/${testId}/results`, { groupId: groupId });
    }

    getFullResult(resultId: number) {
        return this.makeAuthenticatedGet<Attempt>(`api/results/${resultId}`);
    }

    getTestResultsByGroup(testId: number, groupId: number) {
        return this.makeAuthenticatedGet<Attempt[]>(`api/tests/${testId}/results`, { groupId: groupId });
    }
}