import { Component, OnInit } from '@angular/core';
import { Store } from 'src/app/redux/store';
import { NgRedux } from 'ng2-redux';
import { SearchCarService } from 'src/app/services/api-connections/search-car.service';
import { ActionType } from 'src/app/redux/action-type';
import { Action } from 'src/app/redux/action';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  constructor(private searchCarService: SearchCarService, private redux:NgRedux<Store>) { }

  ngOnInit(): void {
    this.retrieveL();
  }


  private retrieveL() {
    console.log("inside localstorage");
    let storedToken: any = localStorage.getItem('cars');
    console.log("storedToken:", storedToken);
    if (!storedToken) throw 'no token found';
    storedToken = JSON.parse(storedToken);
    const action: Action={type:ActionType.addAllWatchedCars, payload:storedToken};
    this.redux.dispatch(action);
    //this.searchCarService.watchedCars=storedToken;
  }

}
