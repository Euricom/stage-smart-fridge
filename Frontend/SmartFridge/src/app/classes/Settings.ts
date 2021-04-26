export class Settings 
{
    constructor
    (
        public emailToSendTo: string,
        public userId: string,
        public sendAmount: number,
        public wantToRecieveNotification: boolean
     ) 
     {}
}
