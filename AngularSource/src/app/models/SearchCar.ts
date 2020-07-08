export class SearchCar {
    public constructor(
        public freeSearch: string = "",
        public fromDate?:Date,
        public toDate?: Date,
        public company: string = "",
        public carType: string = "",
        public gear: string="",
        public year: number = 0
    ) { }
}