



export interface ISimpleModel {
    $type?: string;
        number?: number;
        text?: string;
        isOk?: boolean;
}

export class SimpleModel implements ISimpleModel {
    public $type: string;
        public number: number;
        public text: string;
        public isOk: boolean;

    constructor(initObj?: ISimpleModel) {
        this.$type = 'AngularWebApiSample.Models.SimpleModel, AngularWebApiSample';
        if (initObj) {
            this.number = initObj.number || 0;
            this.text = initObj.text || null;
            this.isOk = initObj.isOk || false;
        } else {
            this.number = 0;
            this.text = null;
            this.isOk = false;
        }
    }
}
