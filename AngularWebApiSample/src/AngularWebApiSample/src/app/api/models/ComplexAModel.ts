

import { IComplexBaseModel, ComplexBaseModel } from './ComplexBaseModel';

export interface IComplexAModel extends IComplexBaseModel {
    text?: string;
    
}

export class ComplexAModel extends ComplexBaseModel implements IComplexAModel {
    public text: string;
    
    constructor(initObj?: IComplexAModel) {
        super(initObj);
        this.$type = 'AngularWebApiSample.Models.ComplexAModel, AngularWebApiSample';
        if(initObj) {
            this.text = initObj.text || null;
            
        }
    }
}

