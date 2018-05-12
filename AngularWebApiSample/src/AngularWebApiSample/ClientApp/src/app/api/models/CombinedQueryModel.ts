

import { SimpleModel } from './SimpleModel';

export interface ICombinedQueryModel {
    $type?: string;
    id?: number;
    simpleModels?: SimpleModel[];
    
}

export class CombinedQueryModel implements ICombinedQueryModel {
    public $type: string;
    public id: number;
    public simpleModels: SimpleModel[];
    
    constructor(initObj?: ICombinedQueryModel) {
        this.$type = 'AngularWebApiSample.Models.CombinedQueryModel, AngularWebApiSample';
        if(initObj) {
            this.id = initObj.id || 0;
            this.simpleModels = initObj.simpleModels || [];
            
        }
        else {
            this.id = 0;
            this.simpleModels = [];
            
        }
    }
}

