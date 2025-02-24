/* eslint-disable @typescript-eslint/no-explicit-any */
// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.



import { ComplexBaseModel } from './ComplexBaseModel';

export interface ICombinedResultModel {
  id?: number;
  sampleList?: ComplexBaseModel[];
}

export class CombinedResultModel implements ICombinedResultModel {
  public $type: string;
  public id: number;
  public sampleList: ComplexBaseModel[];

  constructor(initObj?: ICombinedResultModel) {
    this.$type = 'AngularWebApiSample.Models.CombinedResultModel,'
            + 'AngularWebApiSample';
    if (initObj) {
      this.id = initObj.id ?? 0;
      this.sampleList = initObj.sampleList ?? [];
    } else {
      this.id = 0;
      this.sampleList = [];
    }
  }
}



