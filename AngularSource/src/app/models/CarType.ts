export class CarType {
    public constructor(
        public carTypeId?: number,
        public carType?: string,
        public carFirm?: string,
        public carModel?: string,
        public carDayPrice?: number,
        public carLatePrice?: number,
        public carYear?: number,
        public carGear?: string
    ) { }
}