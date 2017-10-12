
import { API_BASE_URL } from '../../app-config.module';
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';


import { ISimpleModel, SimpleModel } from '../models/SimpleModel';


export interface IMidService {
    postSimpleModel(value: SimpleModel): Observable<any>;
    getstring(id: string): Observable<SimpleModel>;
    
}

@Injectable()
export class MidService implements IMidService {
    constructor (@Inject(HttpClient) protected http: HttpClient, @Optional() @Inject(API_BASE_URL) protected baseUrl?: string) {
    }

    public get midServiceUrl(): string {
        if(this.baseUrl) {
            return this.baseUrl.endsWith('/') ? this.baseUrl+'' : this.baseUrl+'/'+'';
        } else {
            return '';
        }
    }
    
    
        
        
    
    public postSimpleModel(value: SimpleModel): Observable<any> {
        const headers = new HttpHeaders()
            .set("Content-Type", "application/json")
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

        return this.http.post(
            this.midServiceUrl+'',
            value,
            {
                headers: headers,
                responseType: 'text'
            });
    }
    
    
        
    public getstring(id: string): Observable<SimpleModel> {
        const headers = new HttpHeaders()
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

        let params = new HttpParams()
            
            
            .set('id', id)
            ;

        
            

        return this.http.get<SimpleModel>(
            this.midServiceUrl+'',
            {
                headers: headers,
                params: params
            });
    }
        
    
    
    
}
