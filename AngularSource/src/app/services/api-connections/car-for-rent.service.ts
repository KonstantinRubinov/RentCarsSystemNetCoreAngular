import { Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgRedux } from 'ng2-redux';
import { Store } from '../../redux/store';
import { Action } from '../../redux/action';
import { ActionType } from '../../redux/action-type';
import { Rent } from '../../models/Rent';
import { FullCarData } from '../../models/FullCarData';
import { carsForRentUrl, getSumPriceUrl } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { LogService } from '../log.service';

@Injectable({
  providedIn: 'root'
})
export class CarForRentService {
  
  public constructor(private http: HttpClient,
                    private redux:NgRedux<Store>,
                    private logger:LogService) { }
  

  public getAllCarsForRent(): Observable<Rent[]> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.get<Rent[]>(carsForRentUrl, { headers: he });
  }
  
  public getAllCarsForRentByUser(userID:string): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<FullCarData[]>(carsForRentUrl+userID, { headers: he });
    observable.subscribe(carsByUser=>{
      const action: Action={type:ActionType.GetAllFullRents, payload:carsByUser};
      this.redux.dispatch(action);
      this.logger.debug("getAllCarsForRentByUser: ", carsByUser);
    }, error => {
      const action: Action={type:ActionType.GetAllFullRentsError, payload:error.message};
      this.redux.dispatch(action);
      this.logger.error("GetAllFullRentsError: ", error.message);
    });
  }
  
  public getRentPrice(carForRent: Rent): Observable<Rent> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.post<Rent>(getSumPriceUrl, carForRent,{ headers: he });
  }

  public addCarForRent(carForRent: Rent): Observable<Rent> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.post<Rent>(carsForRentUrl, carForRent,{ headers: he });
  }
  
  public updateCarForRent(carForRent: Rent): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.put<Rent>(carsForRentUrl + carForRent.carNumber, carForRent, { headers: he });
    observable.subscribe(rent=>{
      const action: Action={type:ActionType.UpdateCarForRent, payload:rent};
      this.redux.dispatch(action);
      this.logger.debug("updateCarForRent: ", rent);
    }, error => {
      const action: Action={type:ActionType.UpdateCarForRentError, payload:error.message};
      this.redux.dispatch(action);
      this.logger.error("UpdateCarForRentError: ", error.message);
    });
  }

  public updateCarForRent2(carForRent: Rent): Observable<Rent> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.put<Rent>(carsForRentUrl + carForRent.carNumber, carForRent, { headers: he });
  }

  public deleteCarForRent(rentNumber: number):Observable<any>  {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.post<string>(carsForRentUrl, rentNumber, { headers: he });
  }




















  // public getAllCarsForRent(): void {
  //   let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
  //   let observable = this.http.get<Rent[]>(carsForRentUrl, { headers: he });
  //   observable.subscribe(rents=>{
  //     const action: Action={type:ActionType.GetAllRents, payload:rents};
  //     this.redux.dispatch(action);
  //     this.logger.debug("getAllCarsForRent: ", rents);
  //   }, error => {
  //     const action: Action={type:ActionType.GetAllRentsError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("GetAllRentsError: ", error.message);
  //   });
  // }
  
  // public getOneCarForRent(carNumber: string):void {
  //   let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
  //   let observable = this.http.get<Rent>(carsForRentUrl + carNumber, { headers: he });
  //   observable.subscribe(rent=>{
  //     const action: Action={type:ActionType.GetOneRent, payload:rent};
  //     this.redux.dispatch(action);
  //     this.logger.debug("GetOneRent: ", rent);
  //   }, error => {
  //     const action: Action={type:ActionType.GetOneRentError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("GetOneRentError: ", error.message);
  //   });
  // }
  
  
}
