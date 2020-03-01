import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class HttpParamsProcessor {
  process(key: string, obj: any): HttpParams {
    let params = new HttpParams();
    params = this.processInternal(params, key, obj);
    return params;
  }

  processInternal(params: HttpParams, key: string, obj: any): HttpParams {
    if (obj == null) {
      return params;
    }

    if (Array.isArray(obj)) {
      return this.processArray(params, key, obj);
    } else if (typeof obj === 'object') {
      return this.processObject(params, key, obj);
    } else {
      return this.addToHttpParams(params, key, obj);
    }
  }

  private addToHttpParams(params: HttpParams, key: string, elem: any): HttpParams {
    if (typeof elem === 'undefined' || elem == null) {
      return params;
    }

    return params.append(key, elem);
  }

  private processObject(params: HttpParams, key: string, obj: object): HttpParams {
    let retPar = params;
    for (const property in obj) {
      if (!obj.hasOwnProperty(property)) {
        continue;
      }

      if (property === '$type') {
        continue;
      }
      const name = `${key}.${property}`;
      retPar = this.processInternal(retPar, name, (<any>obj)[property]);
    }

    return retPar;
  }

  private processArray(params: HttpParams, key: string, arr: Array<any>): HttpParams {
    let retPar = params;
    let index = 0;
    for (const item of arr) {
      const name = `${key}[${index}]`;
      index++;
      retPar = this.processInternal(retPar, name, item);
    }

    return retPar;
  }
}
