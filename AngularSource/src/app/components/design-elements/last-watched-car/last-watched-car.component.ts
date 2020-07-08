import { Component, OnInit, Input } from '@angular/core';
import { FullCarData } from 'src/app/models/FullCarData';
import { mainUrl } from 'src/environments/environment';

@Component({
  selector: 'app-last-watched-car',
  templateUrl: './last-watched-car.component.html',
  styleUrls: ['./last-watched-car.component.css']
})
export class LastWatchedCarComponent implements OnInit {

  @Input() watchedCar:FullCarData;
  public mUrl=mainUrl;
  constructor() { }

  ngOnInit() {
  }

}
