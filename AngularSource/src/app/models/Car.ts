export class Car {
    public constructor(
        public carNumber?:string,
        public carTypeID?: number,
        public carKm?: number,
        public carPicture?: string,
        public carInShape?: boolean,
        public carAvaliable?: boolean,
        public carBranchID?: number
    ) { }
}