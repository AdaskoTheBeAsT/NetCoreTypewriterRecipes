/* eslint-disable @typescript-eslint/no-explicit-any */
// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.





import { FirstSet } from './FirstSet';
import { SecondSet } from './SecondSet';

export interface ISimpleModel {
  num?: number;
  text?: string | null;
  isOk?: boolean;
  firstSet?: FirstSet;
  secondSet?: SecondSet;
}

export class SimpleModel implements ISimpleModel {
  public $type: string;
  public num: number;
  public text?: string | null;
  public isOk: boolean;
  public firstSet: FirstSet;
  public secondSet: SecondSet;

  constructor(initObj?: ISimpleModel) {
    this.$type = 'AngularWebApiSample.Models.SimpleModel,'
            + 'AngularWebApiSample';
    if (initObj) {
      this.num = initObj.num ?? 0;
      this.text = initObj.text ?? null;
      this.isOk = initObj.isOk ?? false;
      this.firstSet = initObj.firstSet ?? FirstSet.ValA;
      this.secondSet = initObj.secondSet ?? SecondSet.ValA;
    } else {
      this.num = 0;
      this.text = null;
      this.isOk = false;
      this.firstSet = FirstSet.ValA;
      this.secondSet = SecondSet.ValA;
    }
  }
}

