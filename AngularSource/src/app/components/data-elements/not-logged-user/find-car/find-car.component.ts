import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store } from 'src/app/redux/store';
import { NgRedux } from 'ng2-redux';
import { FullCarData } from 'src/app/models/FullCarData';
import { SearchCarService } from 'src/app/services/api-connections/search-car.service';
import { Action } from 'src/app/redux/action';
import { ActionType } from 'src/app/redux/action-type';
import { Unsubscribe } from 'redux';

@Component({
  selector: 'app-find-car',
  templateUrl: './find-car.component.html',
  styleUrls: ['./find-car.component.css']
})
export class FindCarComponent implements OnInit, OnDestroy {
  
  public fullCars: FullCarData[];
  public carPage: number;
  public carPages: number;
  public fullCarsLenght:number=5;
  public loadingCarData = true;
  public allCarsAllDataError:string='';
  private unsubscribe:Unsubscribe;
  
  constructor(private searchCarBy: SearchCarService, private redux:NgRedux<Store>) {}

   public ngOnInit(): void {
    if (this.redux.getState().fullCars.length==0 || this.redux.getState().fullCars==null){
       this.ShowMore();
    }


    this.unsubscribe = this.redux.subscribe(()=>{
      this.fullCars = this.redux.getState().fullCars;
      this.carPage = this.redux.getState().fullCarsPage;
      let lenth = this.redux.getState().fullCarsLenth
      this.carPages = Math.ceil( lenth/this.fullCarsLenght );

      if (this.redux.getState().loadingCarData==false){
        this.loadingCarData=this.redux.getState().loadingCarData;
      }
      if (this.redux.getState().allCarsAllDataError!=null && this.redux.getState().allCarsAllDataError!==''){
        this.allCarsAllDataError=this.redux.getState().allCarsAllDataError;
        const action: Action={type:ActionType.GetAllCarsAllDataError, payload:''};
        this.redux.dispatch(action);
      }
    });
    
  }

  

  public ngOnDestroy(): void {
    this.unsubscribe();
  }

  public ShowMore(){
    this.searchCarBy.GetAllCarsAllData(0, this.fullCarsLenght);
  }

  public PageTo(clickPage:number){
    if (this.redux.getState().searchCar){
      this.searchCarBy.Search(this.redux.getState().searchCar, clickPage, this.fullCarsLenght);
    } else {
      this.searchCarBy.GetAllCarsAllData(clickPage, this.fullCarsLenght);
    }
  }
  
}
