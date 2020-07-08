export class Rent {
    public constructor(
        public rentNumber?: number,
        public userID?: string,
        public carNumber?: string,
        public rentStartDate?: Date,
        public rentEndDate?: Date,
        public rentRealEndDate?: Date,

        public carPrice?:number,
		public orderDays?:number
        
    ) { }
}