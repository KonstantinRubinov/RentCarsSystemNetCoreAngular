import { Component, OnInit } from '@angular/core';
import { CarForRentService } from 'src/app/services/api-connections/car-for-rent.service';
import { Rent } from 'src/app/models/Rent';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-administrate-orders',
  templateUrl: './administrate-orders.component.html',
  styleUrls: ['./administrate-orders.component.css']
})
export class AdministrateOrdersComponent implements OnInit {

  constructor(private carForRentService:CarForRentService,
              private logger:LogService) { }

  ngOnInit() :void {
    this.GetAllAddRents();
  }

  public carRents:Rent[];
  public carRent:Rent = new Rent();

  public rentNumber=0;
  public userID='';
  public carNumber='';
  public rentStartDate:any;
  public rentEndDate:any;
  public rentRealEndDate:any;

  public carRentsError='';
  public addRentError='';
  public updateRentError='';
  public deletedRentError='';
  
  private GetAllAddRents(){
    let observable = this.carForRentService.getAllCarsForRent();
    observable.subscribe(carRents=>{
      this.carRents = carRents;
      this.logger.debug("GetAllAddRents: ", carRents);
    }, carRentsError => {
      this.carRentsError = carRentsError.message;
      this.logger.error("GetAllAddRentsError: ", carRentsError.message);
    });
  }
  
  public AddRent(){
    if (this.carRent.rentNumber===null || this.carRent.rentNumber===0){
      this.carRent.carNumber = this.carNumber;
      this.carRent.rentEndDate = this.rentEndDate;
      this.carRent.rentNumber = this.rentNumber;
      this.carRent.rentRealEndDate = this.rentRealEndDate;
      this.carRent.rentStartDate = this.rentStartDate;
      this.carRent.userID = this.userID;
      
      let observable = this.carForRentService.addCarForRent(this.carRent);
      observable.subscribe(carRent=>{
        this.carRent.rentNumber = carRent.rentNumber;
        this.logger.debug("AddRent: ", carRent);
        this.GetAllAddRents();
      }, addRentError => {
        this.addRentError = addRentError.message;
        this.logger.error("AddRentError: ", addRentError.message);
      });
    }
  }

  public UpdateRent(){
    if (this.carRent.rentNumber!==null && this.carRent.rentNumber>0){
      this.carRent.carNumber = this.carNumber;
      this.carRent.rentEndDate = this.rentEndDate;
      this.carRent.rentNumber = this.rentNumber;
      this.carRent.rentRealEndDate = this.rentRealEndDate;
      this.carRent.rentStartDate = this.rentStartDate;
      this.carRent.userID = this.userID;
      
      let observable = this.carForRentService.updateCarForRent2(this.carRent);
      observable.subscribe(carRent=>{
        this.carRent.rentNumber = carRent.rentNumber;
        this.GetAllAddRents();
        this.logger.debug("UpdateRent: ", carRent);
      }, updateRentError => {
        this.updateRentError = updateRentError.message;
        this.logger.error("UpdateRentError: ", updateRentError.message);
      });
    }
  }

  public DeleteRent(){
    if (this.carRent.rentNumber!==null && this.carRent.rentNumber>0){
      let observable = this.carForRentService.deleteCarForRent(this.carRent.rentNumber);
      observable.subscribe(deleted=>{
        this.GetAllAddRents();
        this.logger.debug("DeleteRent: ", deleted);
      }, deletedRentError => {
        this.deletedRentError = deletedRentError.message;
        this.logger.error("DeleteRentError: ", deletedRentError.message);
      });
    }
  }
  
  public ShowSelectedRent(val){
    let value = val.target.value;
    let itemIndex = this.carRents.findIndex(item => item.rentNumber === +value);
    this.carRent=this.carRents[itemIndex];
    
    this.rentNumber=this.carRent.rentNumber;
    this.userID=this.carRent.userID;
    this.carNumber=this.carRent.carNumber;
    
    this.rentStartDate = this.carRent.rentStartDate;
    this.rentEndDate = this.carRent.rentEndDate;
    this.rentRealEndDate = this.carRent.rentRealEndDate;
  }
}