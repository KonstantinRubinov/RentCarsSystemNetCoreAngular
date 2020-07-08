import { Component, OnInit, Input } from '@angular/core';
import { FullCarData } from 'src/app/models/FullCarData';
import { mainUrl } from 'src/environments/environment';

@Component({
  selector: 'app-car-card-horizontal',
  templateUrl: './car-card-horizontal.component.html',
  styleUrls: ['./car-card-horizontal.component.css']
})
export class CarCardHorizontalComponent implements OnInit {
  @Input() car: FullCarData;
  public splittedBranch;
  public mUrl=mainUrl;
  constructor() { }

  ngOnInit() {
    this.splittedBranch = this.car.branchAddress.split(',', 3);
  }

}
