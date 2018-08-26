
import { API_BASE_URL } from '../../app-config.module';
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';


import { ISimpleModel, SimpleModel } from '../models/SimpleModel';


export interface IMidService {
    postSimpleModel(value: SimpleModel): Observable<SimpleModel>;
    getstring(id: string): Observable<SimpleModel>;
}

@Injectable()
export class MidService implements IMidService {
    constructor (
        @Inject(HttpClient) protected http: HttpClient,
        @Optional() @Inject(API_BASE_URL) protected baseUrl?: string) {
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
                headers: headers
            });
    }

    public getstring(
        id: string): Observable<SimpleModel> {
        const headers = new HttpHeaders()
            .set('Accept', 'application/json')
            .set('If-Modified-Since', '0');

       let params = new HttpParams();
       const funcObj = {
            addToHttpParams(key: string, elem: any): void {
                if (typeof elem === 'undefined' || elem == null) {
                    return;
                }

                params = params.set(key, elem);
            },
            processObject(key: string, obj: object, firstPass: boolean, itemFunc: (key: string, item: any) => void): void {
                for (const property in obj) {
                    if (!obj.hasOwnProperty(property)) {
                        continue;
                    }

                    if (property === '$type') {
                        continue;
                    }

                    const name = firstPass ? property : key + '.' + property;
                    this.process(name, obj[property], false, itemFunc);
                }
            },
            processArray(key: string, arr: Array<any>, itemFunc: (key: string, item: any) => void): void {
                for (const arrid in arr) {
                    if (!arr.hasOwnProperty(arrid)) {
                        continue;
                    }

                    const itemName = key + '[' + arrid + ']';
                    const item = arr[arrid];
                    this.process(itemName, item, false, itemFunc);
                }
            },
            process(key: string, obj: any, firstPass: boolean, itemFunc: (key: string, item: any) => void): void {
                if (obj == null) {
                    return;
                }

                if (Array.isArray(obj)) {
                    this.processArray(key, obj, itemFunc);
                } else if (typeof obj === 'object') {
                    this.processObject(key, obj, firstPass, itemFunc);
                } else {
                    itemFunc(key, obj);
                }
            }
        };

        const parr = [];

        parr.push(id);
        funcObj.process('id', parr.pop(), true, funcObj.addToHttpParams);

        return this.http.get<SimpleModel>(
            this.midServiceUrl + '',
            {
                headers: headers,
                params: params
            });
    }




}
