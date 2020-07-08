import { FullCarData } from './FullCarData';

export class SearchReturn {
    public constructor(
        public fullCarsData?:FullCarData[],
        public fullCarsDataLenth?: number,
        public fullCarsDataPage?: number
    ) { }
}