import { Component, OnInit } from '@angular/core';
import { CarService } from 'src/app/services/api-connections/car.service';
import { Car } from 'src/app/models/Car';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-administrate-cars',
  templateUrl: './administrate-cars.component.html',
  styleUrls: ['./administrate-cars.component.css']
})
export class AdministrateCarsComponent implements OnInit {

  constructor(private carService:CarService, private logger:LogService) { }

  ngOnInit() :void {
    this.GetAllCars();
  }

  public cars:Car[];
  public car:Car = new Car();

  public carNumber='';
  public carTypeID=0;
  public carKm=0;
  public carPicture='';
  public carInShape=true;
  public carAvaliable=false;
  public carBranchID=0;

  public carsError='';
  public addCarError='';
  public updateCarError='';
  public deletedCarError='';
  
  private GetAllCars(){
    let observable = this.carService.getAllCars();
    observable.subscribe(cars=>{
      this.cars = cars;
      this.logger.debug("GetAllCars: ", cars);
    }, carsError => {
      this.carsError = carsError.message;
      this.logger.error("GetAllCarsError: ", carsError.message);
    });
  }
  
  public AddCar(){
    if (this.car.carNumber===null || this.car.carNumber===''){
      this.car.carNumber = this.carNumber;
      this.car.carKm = this.carKm;
      this.car.carAvaliable = this.carAvaliable;
      this.car.carInShape = this.carInShape;
      this.car.carPicture = this.carPicture;
      this.car.carBranchID = this.carBranchID;
      this.car.carPicture = this.carPicture;
      let observable = this.carService.addCar(this.car);
      observable.subscribe(car=>{
        this.car.carNumber = car.carNumber;
        this.logger.debug("AddCar: ", car);
        this.GetAllCars();
      }, carError => {
        this.addCarError = carError.message;
        this.logger.error("AddCarError: ", carError.message);
      });
    }
  }

  public UpdateCar(){
    if (this.car.carNumber!==null && this.car.carNumber!==''){
      this.car.carNumber = this.carNumber;
      this.car.carKm = this.carKm;
      this.car.carAvaliable = this.carAvaliable;
      this.car.carInShape = this.carInShape;
      this.car.carPicture = this.carPicture;
      this.car.carBranchID = this.carBranchID;
      this.car.carPicture = this.carPicture;
      let observable = this.carService.updateCar(this.car);
      observable.subscribe(car=>{
        this.car.carNumber = car.carNumber;
        this.logger.debug("UpdateCar: ", car);
        this.GetAllCars();
      }, updatedCarError => {
        this.updateCarError = updatedCarError.message;
        this.logger.error("UpdateCarError: ", updatedCarError.message);
      });
    }
  }

  public DeleteCar(){
    if (this.car.carNumber!==null && this.car.carNumber!==''){
      let observable = this.carService.deleteCar(this.car.carNumber);
      observable.subscribe(deleted=>{
        this.logger.debug("DeleteCar: ", deleted);
        this.GetAllCars();
      }, deletedCarError => {
        this.deletedCarError = deletedCarError.message;
        this.logger.error("DeleteCarError: ", deletedCarError.message);
      });
    }
  }

  public ShowSelectedCar(val){
    let value = val.target.value;
    let itemIndex = this.cars.findIndex(item => item.carNumber === value);
    this.car=this.cars[itemIndex];
    

    this.carNumber=this.car.carNumber;
    this.carTypeID=this.car.carTypeID;
    this.carKm=this.car.carKm;
    this.carPicture=this.car.carPicture;
    this.carInShape=this.car.carInShape;
    this.carAvaliable=this.car.carAvaliable;
    this.carBranchID=this.car.carBranchID;
  }
  

}
