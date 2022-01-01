﻿// This file has been AUTOGENERATED by TypeWriter (https://frhagn.github.io/Typewriter/).
// Do not modify it.


import { IComplexBaseModel, ComplexBaseModel } from './ComplexBaseModel';

export interface IComplexAModel extends IComplexBaseModel {
    text?: string | null;
}

export class ComplexAModel extends ComplexBaseModel implements IComplexAModel {
    public text?: string | null;

    constructor(initObj?: IComplexAModel) {
        super(initObj);
        this.$type = 'ReactWebApiSample.Models.ComplexAModel,'
            + 'ReactWebApiSample.Models';
        if (initObj) {
            this.text = initObj.text || null;
        } else {
            this.text = null;
        }
    }
}


