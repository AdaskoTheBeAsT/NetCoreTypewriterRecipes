
import { API_BASE_URL } from '../../app-config.module';
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';




export interface ISimpleService {
    get(): Observable<string[]>;
    getnumber(id: number): Observable<string>;
    poststring(value: string): Observable<any>;
    deletenumber(id: number): Observable<any>;
    
}

@Injectable()
export class SimpleService implements ISimpleService {
    constructor (@Inject(HttpClient) protected http: HttpClient, @Optional() @Inject(API_BASE_URL) protected baseUrl?: string) {
    }

    public get simpleServiceUrl(): string {
        if(this.baseUrl) {
            return this.baseUrl.endsWith('/') ? this.baseUrl+'api/Simple' : this.baseUrl+'/'+'api/Simple';
        } else {
            return 'api/Simple';
        }
    }
    
    
        
    public get(): Observable<string[]> {
        const headers = new HttpHeaders()
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

        let params = new HttpParams()
            ;

        

        return this.http.get<string[]>(
            this.simpleServiceUrl+'',
            {
                headers: headers,
                params: params
            });
    }
        
    
    
    
        
    public getnumber(id: number): Observable<string> {
        const headers = new HttpHeaders()
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

        let params = new HttpParams()
            
            
            .set('id', id.toString())
            ;

        
            

        return this.http.get<string>(
            this.simpleServiceUrl+'',
            {
                headers: headers,
                params: params
            });
    }
        
    
    
    
        
        
    
    public poststring(value: string): Observable<any> {
        const headers = new HttpHeaders()
            .set("Content-Type", "application/json")
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

        return this.http.post(
            this.simpleServiceUrl+'',
            value,
            {
                headers: headers,
                responseType: 'text'
            });
    }
    
    
        
        
    
    
    public deletenumber(id: number): Observable<any> {
        return this.http.delete<any>(
            this.simpleServiceUrl+'/'+id);
    }
    
}
