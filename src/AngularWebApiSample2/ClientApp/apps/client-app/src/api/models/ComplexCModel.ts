﻿// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.


import { IComplexBaseModel, ComplexBaseModel } from './ComplexBaseModel';

export interface IComplexCModel extends IComplexBaseModel {
    isActive?: boolean;
}

export class ComplexCModel extends ComplexBaseModel implements IComplexCModel {
    public isActive: boolean;

    constructor(initObj?: IComplexCModel) {
        super(initObj);
        this.$case = 3;
        if (initObj) {
            this.isActive = initObj.isActive || false;
        } else {
            this.isActive = false;
        }
    }
}


