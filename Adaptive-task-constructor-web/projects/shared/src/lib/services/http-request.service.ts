import { HttpClient, HttpContext, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BeUrlService } from "./be-url.service";

@Injectable({providedIn: 'root'})
export class HttpRequestService {

    constructor(protected http: HttpClient, protected beUrlService: BeUrlService) 
    { 
    }

    protected createRoute(route: string){
        return `${this.beUrlService.urlAddress}/${route}`;
    }

    get authOptions(){
        let authToken = localStorage.getItem('token');
        return { headers: {'Authorization': `Bearer ${authToken}`}, params: new HttpParams() };
    }

    makeAuthenticatedGet<T>(addres: string, querryParams: {[key: string]: any} = null){
        let route = this.createRoute(addres);       
        this.http.options('', this.authOptions);
        let options = this.authOptions;
        this.applyQuerryParams(options, querryParams);
        return this.http.get<T>(route, options);
    }

    makeAuthenticatedPost<T>(addres: string, body: any, querryParams: {[key: string]: any} = null){
        let route = this.createRoute(addres);       
        this.http.options('', this.authOptions);
        let options = this.authOptions;
        this.applyQuerryParams(options, querryParams);
        return this.http.post<T>(route, body, options);
    }

    makeAuthenticatedPut<T>(addres: string, body: any, querryParams: {[key: string]: any} = null){
        let route = this.createRoute(addres);       
        this.http.options('', this.authOptions);
        let options = this.authOptions;
        this.applyQuerryParams(options, querryParams);
        return this.http.put<T>(route, body, options);
    }

    makeAuthenticatedDelete<T>(addres: string, querryParams: {[key: string]: any} = null){
        let route = this.createRoute(addres);       
        this.http.options('', this.authOptions);
        let options = this.authOptions;
        this.applyQuerryParams(options, querryParams);
        return this.http.delete<T>(route, options);
    }

    private applyQuerryParams(authOptions: any, querryParams: {[key: string]: any} = null){
        if(!!querryParams) {
            let params = new HttpParams({fromObject: querryParams});
            authOptions.params = params;
        }
    }
}