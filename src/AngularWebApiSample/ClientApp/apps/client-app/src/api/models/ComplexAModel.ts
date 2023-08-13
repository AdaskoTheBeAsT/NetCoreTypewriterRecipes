﻿// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.


import { IComplexBaseModel, ComplexBaseModel } from './ComplexBaseModel';

export interface IComplexAModel extends IComplexBaseModel {
  text?: string | null;
}

export class ComplexAModel extends ComplexBaseModel implements IComplexAModel {
  public text?: string | null;

  constructor(initObj?: IComplexAModel) {
    super(initObj);
    this.$type = 'AngularWebApiSample.Models.ComplexAModel,'
            + 'AngularWebApiSample.Models';
    if (initObj) {
      this.text = initObj.text ?? null;
    } else {
      this.text = null;
    }
  }
}


