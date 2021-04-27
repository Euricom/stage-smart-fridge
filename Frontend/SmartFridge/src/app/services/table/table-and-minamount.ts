import { Beverage } from "./beverage";

export interface ItableAndMinAmount 
{
    tableData: Beverage[];
    minAmount: number;
}

export class tableAndMinAmount 
{
    constructor
    (
        public tableData: Beverage[],
        public minAmount: number
     ) 
     {}
}
