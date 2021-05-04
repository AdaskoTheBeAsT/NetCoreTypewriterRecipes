
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { API_BASE_URL } from 'src/app/app-config.module';


import { HttpParamsProcessorService } from '@adaskothebeast/http-params-processor';
import { SimpleModel } from 'src/api/models/SimpleModel';



interface Object {
  hasOwnProperty<TK extends string>(v: TK): this is Record<TK, object>;
}

export interface IApiMidService {
    postSimpleModel(value: SimpleModel): Observable<SimpleModel>;
    getstring(id: string): Observable<SimpleModel>;
}

@Injectable(
    { providedIn: 'root' }
)
export class ApiMidService implements IApiMidService {
    constructor (
      @Inject(HttpClient) protected http: HttpClient,
      @Optional() @Inject(API_BASE_URL) protected baseUrl?: string,
      @Inject(HttpParamsProcessorService) protected processor: HttpParamsProcessorService) {
    }

    public get midServiceUrl(): string {
        if (this.baseUrl) {
            return this.baseUrl.endsWith('/') ? this.baseUrl + '' : this.baseUrl + '/' + '';
        } else {
            return '';
        }
    }





    public postSimpleModel(value: SimpleModel): Observable<SimpleModel> {
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/json')
            .set('Accept', 'application/json')
            .set('If-Modified-Since', '0');

        return this.http.post<SimpleModel>(
            this.midServiceUrl + '',
            value,
            {
                headers
            });
    }

    public getstring(id: string): Observable<SimpleModel> {
        const headers = new HttpHeaders()
            .set('Accept', 'application/json')
            .set('If-Modified-Since', '0');

        let params = new HttpParams();
        const parr = [];

        parr.push(id);
        params = this.processor.processInternal(params, 'id', parr.pop());

        return this.http.get<SimpleModel>(
            this.midServiceUrl + '',
            {
                headers,
                params
            });
    }





}
