﻿// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.

export enum SecondSet {
    ValA = 'ValA',
    
    ValB = 'ValB',
    
    SomeOtherVal = 'SomeOtherVal'
}

export namespace SecondSet {
    export function getLabel(value: SecondSet): string {
        var toReturn = '';
        switch(value) {
            case SecondSet.ValA: toReturn = 'ValA'; break;
            
            case SecondSet.ValB: toReturn = 'ValB'; break;
            
            case SecondSet.SomeOtherVal: toReturn = 'ValC'; break;
        }
        return toReturn;
    } 
}




