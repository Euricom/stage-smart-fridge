export class LoginValues
{
    constructor
    (
        public token: string,
        public expiration: Date,
        public id: string,
        public userName: string,
        public minAmount: number,
        public emailToSendTo: string,
        public checkBoxValue: boolean
     ) 
     {}
}
