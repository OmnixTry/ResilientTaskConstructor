import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BeUrlService } from "projects/shared/src/lib/services/be-url.service";
import { HttpRequestService } from "projects/shared/src/public-api";
import { Observable } from "rxjs";
import { Test } from "../entity/test";

@Injectable({ providedIn: 'root' })
export class TestRepository extends HttpRequestService {
    constructor(http: HttpClient, beUrlService: BeUrlService) {
        super(http, beUrlService)
    }

    createTest(test: Test): Observable<Test> {
        return this.makeAuthenticatedPost<Test>('api/tests', test);
    }

    updateTest(test: Test): Observable<Test> {
        return this.makeAuthenticatedPut<Test>('api/tests', test);
    }

    deleteTest(testName: string) {
        return this.makeAuthenticatedDelete<Test>(`api/tests/${testName}`);
    }

    getFullTest(testName: string) {
        return this.makeAuthenticatedGet<Test>(`api/tests/${testName}`);
    }

    getMysteryTest(id: number) {
        return this.makeAuthenticatedGet<Test>(`api/tests/mystery/${id}`);
    }
}