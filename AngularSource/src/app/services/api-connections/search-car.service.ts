import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { SearchCar } from '../../models/SearchCar';
import { Action } from '../../redux/action';
import { ActionType } from '../../redux/action-type';
import { FullCarData } from '../../models/FullCarData';
import { Observable } from 'rxjs';
import { SearchReturn } from '../../models/SearchReturn';
import { searchUrl, fullCarsUrl } from 'src/environments/environment';
import { LogService } from '../log.service';

@Injectable({
  providedIn: 'root'
})
export class SearchCarService {
  
  constructor(private http: HttpClient, private redux:NgRedux<Store>, private logger:LogService) { }
  
  public Search(searchCarBy: SearchCar, page:number, carsNum: number): void {
    const action: Action={type:ActionType.StartLoadingCarData, payload:true};
    this.redux.dispatch(action);

    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token'), 'page': page.toString(), 'carsNum': carsNum.toString() });
    let observable = this.http.post<SearchReturn>(searchUrl, searchCarBy, { headers: he });
    observable.subscribe(searchReturn=>{
      const action: Action={type:ActionType.GetAllFullCars, payload:searchReturn};
      this.redux.dispatch(action);
      this.logger.debug("Search: ", searchReturn);
    }, allCarsAllDataError => {
      const action: Action={type:ActionType.GetAllFullCarsError, payload:allCarsAllDataError.message};
      this.redux.dispatch(action);
      this.logger.error("SearchError: ", allCarsAllDataError.message);
    });
  }
  


  public SearchByCar(number:string, searchCarBy?: SearchCar): void {
    const action: Action={type:ActionType.StartLoadingCarData, payload:true};
    this.redux.dispatch(action);

    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.post<FullCarData>(searchUrl+number, searchCarBy, { headers: he });
    observable.subscribe(fullCarData=>{
      const action: Action={type:ActionType.GetAllFullCarData, payload:fullCarData};
      this.redux.dispatch(action);
      this.logger.debug("SearchByCar: ", fullCarData);
    }, allCarsAllDataError => {
      const action: Action={type:ActionType.GetAllCarsAllDataError, payload:allCarsAllDataError.message};
      this.redux.dispatch(action);
      this.logger.error("SearchByCarError: ", allCarsAllDataError.message);
    });
  }



  public GetAllCarsAllData(page:number, carsNum: number): void {
    const action: Action={type:ActionType.StartLoadingCarData, payload:true};
    this.redux.dispatch(action);

    let he = new HttpHeaders({'Content-Type': 'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token'), 'page': page.toString(), 'carsNum': carsNum.toString() });
    let observable = this.http.get<SearchReturn>(fullCarsUrl, { headers: he });
    observable.subscribe(searchReturn=>{
      const action: Action={type:ActionType.GetSearchReturn, payload:searchReturn};
      this.redux.dispatch(action);
      this.logger.debug("GetSearchReturn: ", searchReturn);
    }, searchReturnError => {
      const action: Action={type:ActionType.GetSearchReturnError, payload:searchReturnError.message};
      this.redux.dispatch(action);
      this.logger.error("GetSearchReturnError: ", searchReturnError.message);
    });
  }


  public GetCarAllData(number:string): Observable <FullCarData>{
    const action: Action={type:ActionType.StartLoadingCarData, payload:true};
    this.redux.dispatch(action);
    
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.get<FullCarData>(fullCarsUrl+number, { headers: he });
  }


// ------------------------------------------------------
    
  public addCarToWatched(jsonObject){
    const action: Action={type:ActionType.addWOneWatchedCar, payload:jsonObject};
    this.redux.dispatch(action);
    this.logger.debug("addCarToWatched: ", jsonObject);
    this.storeL(this.redux.getState().watchedCars);
  }


  private storeL(contentData) {
    this.logger.debug("inside localstorsge store: ", contentData);
    localStorage.setItem('cars', JSON.stringify(contentData));
    this.logger.debug("contentData : ", contentData);
  }
}
