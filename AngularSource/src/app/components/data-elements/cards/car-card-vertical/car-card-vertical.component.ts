import { Component, OnInit, Input } from '@angular/core';
import { FullCarData } from 'src/app/models/FullCarData';
import { mainUrl } from 'src/environments/environment';

@Component({
  selector: 'app-car-card-vertical',
  templateUrl: './car-card-vertical.component.html',
  styleUrls: ['./car-card-vertical.component.css']
})
export class CarCardVerticalComponent implements OnInit {
  @Input() car: FullCarData;
  public splittedBranch;
  public mUrl=mainUrl;
  constructor() { }

  ngOnInit() {
    this.splittedBranch = this.car.branchAddress.split(',', 3);
  }

}
