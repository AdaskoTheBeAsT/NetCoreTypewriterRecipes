

import { ComplexBaseModel } from './ComplexBaseModel';

export interface ICombinedModel {
    $type?: string;
    id?: number;
    sampleList?: ComplexBaseModel[];
    
}

export class CombinedModel implements ICombinedModel {
    public $type: string;
    public id: number;
    public sampleList: ComplexBaseModel[];
    
    constructor(initObj?: ICombinedModel) {
        this.$type = 'AngularWebApiSample.Models.CombinedModel, AngularWebApiSample';
        if(initObj) {
            this.id = initObj.id || 0;
            this.sampleList = initObj.sampleList || [];
            
        }
    }
}

