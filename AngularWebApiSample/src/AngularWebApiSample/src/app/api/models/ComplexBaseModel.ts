



export interface IComplexBaseModel {
    $type?: string;
    id?: number;
    
}

export class ComplexBaseModel implements IComplexBaseModel {
    public $type: string;
    public id: number;
    
    constructor(initObj?: IComplexBaseModel) {
        this.$type = 'AngularWebApiSample.Models.ComplexBaseModel, AngularWebApiSample';
        if(initObj) {
            this.id = initObj.id || 0;
            
        }
        else {
            this.id = 0;
            
        }
    }
}

