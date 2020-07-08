export class Branch {
    public constructor(
        public branchID?:number,
        public branchAddress?: string,
        public branchLat?: number,
        public branchLng?: number,
        public branchName?: string
    ) { }
}