
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
    
    
        
        
    
    public postCombinedResultModel(value: CombinedResultModel): Observable<any> {
        const headers = new HttpHeaders()
            .set("Content-Type", "application/json")
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

        return this.http.post(
            this.complexControlerUrl+'',
            value,
            {
                headers: headers,
                responseType: 'text'
            });
    }
    
    
        
    public getCombinedQueryModel(query: CombinedQueryModel): Observable<CombinedResultModel> {
        const headers = new HttpHeaders()
            .set("Accept", "application/json")
            .set("If-Modified-Since", "0");

        let params = new HttpParams()
            
            ;

        
            
            let parameter = query;
            for (let key in parameter) {
                if (!parameter.hasOwnProperty(key)){
                    continue;
                }

                if (key==='$type'){
                    continue;
                }

                let elem = parameter[key];
                if(Array.isArray(elem)){
                    let id=0;
                    for(let item in elem){
                        let itemName = key+'['+id+']';
                        for (let attr in elem[item]) {
                            if (attr==='$type'){
                                continue;
                            }
                            if (!elem[item].hasOwnProperty(attr)){
                                continue;
                            }

                            let attrName = itemName+'.'+attr;
                            params = params.set(attrName, elem[item][attr]);
                        }
                        id++;
                    }
                }
                else{
                    params = params.set(key, elem);
                }
                
            }

        return this.http.get<CombinedResultModel>(
            this.complexControlerUrl+'',
            {
                headers: headers,
                params: params
            });
    }
        
    
    
    
}
