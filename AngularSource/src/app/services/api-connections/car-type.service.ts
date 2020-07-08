import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CarType } from '../../models/CarType';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { Action } from '../../redux/action';
import { ActionType } from '../../redux/action-type';
import { carTypesUrl } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { LogService } from '../log.service';

@Injectable({
  providedIn: 'root'
})
export class CarTypeService {
  
  public constructor(private http: HttpClient,
                     private redux:NgRedux<Store>,
                     private logger:LogService) { }
  
  public getAllTypesAndParse(): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<CarType[]>(carTypesUrl, { headers: he });
    observable.subscribe(carTypes=>{
      const action: Action={type:ActionType.GetAllTypesAndParse, payload:carTypes};
      this.redux.dispatch(action);
      this.logger.debug("getAllTypesAndParse: ", carTypes);
    }, error => {
      const action: Action={type:ActionType.GetAllTypesAndParseError, payload:error.message};
      this.redux.dispatch(action);
      this.logger.error("getAllTypesAndParseError: ", error.message);
    });
  }
  
  public getAllCarTypes(): Observable<CarType[]> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.get<CarType[]>(carTypesUrl, { headers: he });
  }

  public addCarType(carType: CarType): Observable<CarType> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.post<CarType>(carTypesUrl, carType, { headers: he });
  }

  public updateCarType(carType: CarType): Observable<CarType> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.put<CarType>(carTypesUrl + carType.carTypeId, carType, { headers: he });
  }

  public deleteCarType(id: number): Observable<any> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.delete<any>(carTypesUrl+id, { headers: he });
  }
  






















  // public GetAllCarTypes(): void {
  //   let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
  //   let observable = this.http.get<CarType[]>(carTypesUrl, { headers: he });
  //   observable.subscribe(carTypes=>{
  //     const action: Action={type:ActionType.GetAllTypes, payload:carTypes};
  //     this.redux.dispatch(action);
  //     this.logger.debug("GetAllCarTypes: ", carTypes);
  //   }, error => {
  //     const action: Action={type:ActionType.GetAllTypesError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("GetAllTypesError: ", error.message);
  //   });
  // }

  // public GetOneCarType(carType: string): void {
  //   let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
  //   let observable = this.http.get<CarType>(carTypesUrl + carType, { headers: he });
  //   observable.subscribe(carType=>{
  //     const action: Action={type:ActionType.GetOneType, payload:carType};
  //     this.redux.dispatch(action);
  //     this.logger.debug("GetOneCarType: ", carType);
  //   }, error => {
  //     const action: Action={type:ActionType.GetOneTypeError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("GetOneCarTypeError: ", error.message);
  //   });
  // }

  // public AddCarType(carType: CarType): void {
  //   let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
  //   let observable = this.http.post<CarType>(carTypesUrl, carType, { headers: he });
  //   observable.subscribe(carType=>{
  //     const action: Action={type:ActionType.AddType, payload:carType};
  //     this.redux.dispatch(action);
  //     this.logger.debug("AddCarType: ", carType);
  //   }, error => {
  //     const action: Action={type:ActionType.AddTypeError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("AddCarTypeError: ", error.message);
  //   });
  // }


  // public UpdateCarType(carType: CarType): void {
  //   let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
  //   let observable = this.http.put<CarType>(carTypesUrl + carType.carType, carType, { headers: he });
  //   observable.subscribe(carType=>{
  //     const action: Action={type:ActionType.UpdateType, payload:carType};
  //     this.redux.dispatch(action);
  //     this.logger.debug("UpdateCarType: ", carType);
  //   }, error => {
  //     const action: Action={type:ActionType.UpdateTypeError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("UpdateCarTypeError: ", error.message);
  //   });
  // }


  // public DeleteCarType(carType: string): void {
  //   let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
  //   let observable = this.http.post<string>(carTypesUrl, carType, { headers: he });
  //   observable.subscribe(carType=>{
  //     const action: Action={type:ActionType.DeleteType, payload:carType};
  //     this.redux.dispatch(action);
  //     this.logger.debug("DeleteCarType: ", carType);
  //   }, error => {
  //     const action: Action={type:ActionType.DeleteTypeError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("DeleteCarTypeError: ", error.message);
  //   });
  // }
}
