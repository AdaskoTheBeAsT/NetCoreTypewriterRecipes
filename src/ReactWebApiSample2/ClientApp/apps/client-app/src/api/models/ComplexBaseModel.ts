﻿// This file has been AUTOGENERATED by TypeWriter (https://frhagn.github.io/Typewriter/).
// Do not modify it.




export interface IComplexBaseModel {
    $case?: string | number;
    id?: number;
}

export class ComplexBaseModel implements IComplexBaseModel {
    public $case?: string | number;
    public id: number;

    constructor(initObj?: IComplexBaseModel) {
        this.$case = undefined;
        if (initObj) {
            this.id = initObj.id || 0;
        } else {
            this.id = 0;
        }
    }
}

