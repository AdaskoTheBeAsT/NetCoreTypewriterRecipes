

import { IComplexBaseModel, ComplexBaseModel } from './ComplexBaseModel';

export interface IComplexBModel extends IComplexBaseModel {
    isActive?: boolean;
    
}

export class ComplexBModel extends ComplexBaseModel implements IComplexBModel {
    public isActive: boolean;
    
    constructor(initObj?: IComplexBModel) {
        super(initObj);
        this.$type = 'AngularWebApiSample.Models.ComplexBModel, AngularWebApiSample';
        if(initObj) {
            this.isActive = initObj.isActive || false;
            
        }
    }
}

