export interface ILoginValues 
{
    token: string;
    expiration: Date;
    id: string;
    userName: string;
}

export class LoginValues
{
    constructor
    (
        public token: string,
        public expiration: Date,
        public id: string,
        public userName: string
     ) 
     {}
}
