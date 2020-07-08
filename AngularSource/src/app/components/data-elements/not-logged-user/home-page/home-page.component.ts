import { Component, OnInit } from '@angular/core';
import { SearchCarService } from 'src/app/services/api-connections/search-car.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  constructor(private searchCarBy: SearchCarService) { }

  ngOnInit() {
    // this.searchCarBy.GetAllCarsAllData(0, 5);
  }

}
