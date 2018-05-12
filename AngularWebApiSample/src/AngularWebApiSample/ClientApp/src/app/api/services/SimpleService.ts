
import { API_BASE_URL } from '../../app-config.module';
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';






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

        
        parr.push(id);
        funcObj.process('id', parr.pop(), true, funcObj.addToHttpParams);

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
