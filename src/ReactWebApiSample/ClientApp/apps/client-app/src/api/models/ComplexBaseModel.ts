﻿// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.




export interface IComplexBaseModel {
  $type?: string;
  id?: number;
}

export class ComplexBaseModel implements IComplexBaseModel {
  public $type: string;
  public id: number;

  constructor(initObj?: IComplexBaseModel) {
    this.$type = 'ReactWebApiSample.Models.ComplexBaseModel,'
            + 'ReactWebApiSample.Models';
    if (initObj) {
      this.id = initObj.id ?? 0;
    } else {
      this.id = 0;
    }
  }
}


