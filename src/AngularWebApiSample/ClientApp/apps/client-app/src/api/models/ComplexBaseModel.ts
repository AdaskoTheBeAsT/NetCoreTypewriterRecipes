/* eslint-disable @typescript-eslint/no-explicit-any */
// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.





export interface IComplexBaseModel {
  id?: number;
}

export class ComplexBaseModel implements IComplexBaseModel {
  public $type: string;
  public id: number;

  constructor(initObj?: IComplexBaseModel) {
    this.$type = 'AngularWebApiSample.Models.ComplexBaseModel,'
            + 'AngularWebApiSample';
    if (initObj) {
      this.id = initObj.id ?? 0;
    } else {
      this.id = 0;
    }
  }
}



