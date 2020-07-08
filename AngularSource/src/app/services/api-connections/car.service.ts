import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Car } from '../../models/Car';
import { carsUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  
  public constructor(private http: HttpClient) { }

  public getAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(carsUrl);
  }

  public getOneCar(carNumber: string): Observable<Car> {
    return this.http.get<Car>(carsUrl + carNumber);
  }

  public addCar(car: Car): Observable<Car> {
    return this.http.post<Car>(carsUrl, car);
  }


  public updateCar(car: Car): Observable<Car> {
    return this.http.put<Car>(carsUrl + car.carNumber, car);
  }


  public deleteCar(carNumber: string): Observable<string> {
    return this.http.post<string>(carsUrl, carNumber);
  }
}
