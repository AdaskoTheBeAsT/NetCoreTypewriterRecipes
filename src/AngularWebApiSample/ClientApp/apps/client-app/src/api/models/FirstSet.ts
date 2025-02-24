/* eslint-disable @typescript-eslint/no-explicit-any */
// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.

export enum FirstSet {
  ValA = 0,

  ValB = 1,

  SomeOtherVal = 2
}

// eslint-disable-next-line @typescript-eslint/no-namespace
export namespace FirstSet {
  export function getLabel(value: FirstSet): string {
    let toReturn = '';
    switch(value) {
      case FirstSet.ValA:
        toReturn = 'ValA';
        break;
      case FirstSet.ValB:
        toReturn = 'ValB';
        break;
      case FirstSet.SomeOtherVal:
        toReturn = 'ValC';
        break;
    }
    return toReturn;
  }

  export function getKeys(): Array<number> {
    const list = new Array<number>();
    for (const enumMember in FirstSet) {
      const parsed = parseInt(enumMember, 10);
      if (parsed < 0) {
        continue;
      }

      list.push(parsed);
    }

    return list;
  }
}






