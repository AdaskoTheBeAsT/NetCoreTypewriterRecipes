
import { API_BASE_URL } from '../../app-config.module';
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';


import { ICombinedResultModel, CombinedResultModel } from '../models/CombinedResultModel';
import { ICombinedQueryModel, CombinedQueryModel } from '../models/CombinedQueryModel';


export interface IComplexControler {
    postCombinedResultModel(value: CombinedResultModel): Observable<CombinedResultModel>;
    getCombinedQueryModel(query: CombinedQueryModel): Observable<CombinedResultModel>;
    
}

@Injectable()
export class ComplexControler implements IComplexControler {
    constructor (@Inject(HttpClient) protected http: HttpClient, @Optional() @Inject(API_BASE_URL) protected baseUrl?: string) {
    }

    public get complexControlerUrl(): string {
        if(this.baseUrl) {
            return this.baseUrl.endsWith('/') ? this.baseUrl+'' : this.baseUrl+'/'+'';
        } else {
            return '';
        }
    }
    
    
        
    
    
    
    public postCombinedResultModel(value: CombinedResultModel): Observable<CombinedResultModel> {
        const headers = new HttpHeaders()
            .set("Content-Type", "application/json")
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

        return this.http.post<CombinedResultModel>(
            this.complexControlerUrl+'',
            value,
            {
                headers: headers
            });
    }
    
        
    public getCombinedQueryModel(query: CombinedQueryModel): Observable<CombinedResultModel> {
        const headers = new HttpHeaders()
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

       let params = new HttpParams();
       let funcObj = {
            addToHttpParams(key: string, elem: any): void {
                if (typeof elem === 'undefined' || elem == null) {
                    return;
                }

                params = params.set(key, elem);
            },
            processObject(key: string, obj: object, firstPass:boolean, itemFunc: (key: string, item: any) => void): void {
                for (let property in obj) {
                    if (!obj.hasOwnProperty(property)){
                        continue;
                    }

                    if (property==='$type') {
                        continue;
                    }
                    let name = firstPass ? property : key + "." + property;
                    this.process(name, obj[property], false, itemFunc);
                }
            },
            processArray(key:string, arr: Array<any>, itemFunc: (key:string, item:any)=>void): void {
                for (let id in arr) {
                    if (!arr.hasOwnProperty(id)){
                        continue;
                    }
                    let itemName = key + '[' + id + ']';
                    let item = arr[id];
                    this.process(itemName, item, false, itemFunc);
                }
            },
            process(key: string, obj: any, firstPass: boolean, itemFunc: (key: string, item: any) => void): void {
                if (obj == null) { 
                    return;
                } 

                if (Array.isArray(obj)) {
                    this.processArray(key, obj, itemFunc);
                }
                else if (typeof obj === 'object') {
                    this.processObject(key, obj, firstPass, itemFunc);
                }
                else { 
                    itemFunc(key, obj);
                }
            }
        };

        let parr = [];

        
        parr.push(query);
        funcObj.process('query', parr.pop(), true, funcObj.addToHttpParams);

        return this.http.get<CombinedResultModel>(
            this.complexControlerUrl+'',
            {
                headers: headers,
                params: params
            });
    }
    
    
    
    
}
