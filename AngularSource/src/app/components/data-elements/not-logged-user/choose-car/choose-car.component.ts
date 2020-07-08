import { Component, OnInit } from '@angular/core';
import { SearchCar } from 'src/app/models/SearchCar';
import { SearchCarService } from 'src/app/services/api-connections/search-car.service';
import { CarTypeService } from 'src/app/services/api-connections/car-type.service';
import { Unsubscribe } from 'redux';
import { Store } from 'src/app/redux/store';
import { NgRedux } from 'ng2-redux';
import { FullCarData } from 'src/app/models/FullCarData';
import { MyNavigator } from 'src/app/models/MyNavigator';
import { Action } from 'src/app/redux/action';
import { ActionType } from 'src/app/redux/action-type';

@Component({
  selector: 'app-choose-car',
  templateUrl: './choose-car.component.html',
  styleUrls: ['./choose-car.component.css']
})
export class ChooseCarComponent implements OnInit {
  private unsubscribe:Unsubscribe;
  public localSearchCar=new SearchCar();

  public selectedType:string;
  public selectedCompany:string;
  public selectedGear:string;

  public fullCars: FullCarData[];
  public types:string[];
  public firms:string[];
  public gears:string[];
  
  public today:string = new Date().toISOString().slice(0, 10);

  constructor(private searchCarBy: SearchCarService,
              private carTypeService: CarTypeService,
              private redux:NgRedux<Store>) { }

  public Search(){
    this.localSearchCar.carType=this.selectedType;
    this.localSearchCar.company=this.selectedCompany;
    this.localSearchCar.gear=this.selectedGear;

    const action: Action={type:ActionType.AddSearchCar, payload:this.localSearchCar};
    this.redux.dispatch(action);

    this.searchCarBy.Search(this.redux.getState().searchCar, 0, 5);
  }

  ngOnInit() {
    this.unsubscribe = this.redux.subscribe(()=>{
      this.fullCars = this.redux.getState().fullCars;
      this.types = this.redux.getState().types;
      this.firms = this.redux.getState().firms;
      this.gears = this.redux.getState().gears;
    });
    this.carTypeService.getAllTypesAndParse();
    //this.searchCarBy.Search(new SearchCar() , 0, 5);
  }

  public ngOnDestroy(): void {
    this.unsubscribe();
  }

  navigators = [
    new MyNavigator("/chooseCar", 'Choose Car')
  ];

}
