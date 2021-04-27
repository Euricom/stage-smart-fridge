export interface Ibeverage 
{
    id: number;
    name: string;
    amount: number;
}

export class Beverage
{
    constructor
    (
        public id: number,
        public name: string,
        public amount: number
     ) 
     {}
}
