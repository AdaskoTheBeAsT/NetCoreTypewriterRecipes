



export interface ISimpleModel {
    $type?: string;
    number?: number;
    text?: string;
    
}

export class SimpleModel implements ISimpleModel {
    public $type: string;
    public number: number;
    public text: string;
    
    constructor(initObj?: ISimpleModel) {
        this.$type = 'AngularWebApiSample.Models.SimpleModel, AngularWebApiSample';
        if(initObj) {
            this.number = initObj.number || 0;
            this.text = initObj.text || null;
            
        }
    }
}

