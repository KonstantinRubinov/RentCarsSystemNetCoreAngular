export class FullCarData {
    public constructor(
        public carNumber?:string,
        public carKm?: number,
        public carPicture: string ='',
        public carInShape?: boolean,
        public carAvaliable?: boolean,
        public carBranchID?: number,
        public carType?: string,
        public carFirm?: string,
        public carModel?: string,
        public carDayPrice?: number,
        public carLatePrice?: number,
        public carYear?: number,
        public carGear?: string,
        public branchName?:string,
        public branchAddress?: string,
        public branchLat?: number,
        public branchLng?: number,
    ) { }
}