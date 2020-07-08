import { Component, OnInit, OnDestroy } from '@angular/core';
import { FullCarData } from 'src/app/models/FullCarData';
import { Unsubscribe } from 'redux';
import { Store } from 'src/app/redux/store';
import { NgRedux } from 'ng2-redux';
import { SearchCarService } from 'src/app/services/api-connections/search-car.service';
import { MyNavigator } from 'src/app/models/MyNavigator';
import { Action } from 'src/app/redux/action';
import { ActionType } from 'src/app/redux/action-type';

@Component({
  selector: 'app-our-cars',
  templateUrl: './our-cars.component.html',
  styleUrls: ['./our-cars.component.css']
})
export class OurCarsComponent implements OnInit, OnDestroy {
  public fullCars: FullCarData[];
  public loadingCarData = true;
  public allCarsAllDataError:string;
  private unsubscribe:Unsubscribe;

  constructor(private searchCarBy: SearchCarService, public redux:NgRedux<Store>) { }

  public ngOnInit(): void {
    this.unsubscribe = this.redux.subscribe(()=>{
      this.fullCars = this.redux.getState().fullCars;
        if (this.redux.getState().loadingCarData==false){
          this.loadingCarData=this.redux.getState().loadingCarData;
        }
        if (this.redux.getState().allCarsAllDataError!=null && this.redux.getState().allCarsAllDataError!==''){
          this.allCarsAllDataError=this.redux.getState().allCarsAllDataError;
          const action: Action={type:ActionType.GetAllCarsAllDataError, payload:''};
          this.redux.dispatch(action);
        }
    });
    this.searchCarBy.GetAllCarsAllData(0, 5);
  }

 
  public ngOnDestroy(): void {
    this.unsubscribe();
  }

  public ShowMore(){
    this.searchCarBy.GetAllCarsAllData(0, 5);
  }

  navigators = [
    new MyNavigator("/chooseCar", 'Choose Car')
  ];

}
