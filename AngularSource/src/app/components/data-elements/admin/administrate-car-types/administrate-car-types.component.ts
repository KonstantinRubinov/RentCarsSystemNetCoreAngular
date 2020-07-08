import { Component, OnInit } from '@angular/core';
import { CarType } from 'src/app/models/CarType';
import { CarTypeService } from 'src/app/services/api-connections/car-type.service';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-administrate-car-types',
  templateUrl: './administrate-car-types.component.html',
  styleUrls: ['./administrate-car-types.component.css']
})
export class AdministrateCarTypesComponent implements OnInit{
  constructor(private carTypeService:CarTypeService, private logger:LogService) { }

  ngOnInit(): void {
    this.GetAllAddTypes();
  }
  
  public carTypes:CarType[];
  public carTypeModel:CarType = new CarType();

  public gears = ['automatic','manual','hybrid']

  public carTypeId=0;
  public carType='';
  public carFirm='';
  public carModel='';
  public carDayPrice:number;
  public carLatePrice:number;
  public carYear:number;
  public selectedGear='';

  public carTypesError='';
  public addTypeError='';
  public updateTypeError=''
  public deletedTypeError='';

  

  private GetAllAddTypes(){
    let observable = this.carTypeService.getAllCarTypes();
    observable.subscribe(carTypes=>{
      this.carTypes = carTypes;
      this.logger.debug("GetAllAddTypes: ", carTypes);
    }, carTypesError => {
      this.carTypesError = carTypesError.message;
      this.logger.error("GetAllAddTypesError: ", carTypesError.message);
    });
  }
  
  public AddType(){
    if (this.carTypeModel.carTypeId===null || this.carTypeModel.carTypeId===-1){
      this.carTypeModel.carType = this.carType;
      this.carTypeModel.carFirm = this.carFirm;
      this.carTypeModel.carModel = this.carModel;
      this.carTypeModel.carDayPrice = this.carDayPrice;
      this.carTypeModel.carLatePrice = this.carLatePrice;
      this.carTypeModel.carYear = this.carYear;
      this.carTypeModel.carGear = this.selectedGear;
      
      let observable = this.carTypeService.addCarType(this.carTypeModel);
      observable.subscribe(carType=>{
        this.carTypeModel.carTypeId = carType.carTypeId;
        this.logger.debug("AddType: ", carType);
        this.GetAllAddTypes();
      }, carTypeError => {
        this.addTypeError = carTypeError.message;
        this.logger.error("AddTypeError: ", carTypeError.message);
      });
    }
  }

  public UpdateType(){
    if (this.carTypeModel.carTypeId!==null || this.carTypeModel.carTypeId>-1){
      this.carTypeModel.carType = this.carType;
      this.carTypeModel.carFirm = this.carFirm;
      this.carTypeModel.carModel = this.carModel;
      this.carTypeModel.carDayPrice = this.carDayPrice;
      this.carTypeModel.carLatePrice = this.carLatePrice;
      this.carTypeModel.carYear = this.carYear;
      this.carTypeModel.carGear = this.selectedGear;
      
      let observable = this.carTypeService.updateCarType(this.carTypeModel);
      observable.subscribe(carType=>{
        this.carTypeModel.carTypeId = carType.carTypeId;
        this.logger.debug("UpdateType: ", carType);
        this.GetAllAddTypes();
      }, carTypeError => {
        this.addTypeError = carTypeError.message;
        this.logger.error("UpdateTypeError: ", carTypeError.message);
      });
    }
  }

  public DeleteType(){
    if (this.carTypeModel.carTypeId!==null && this.carTypeModel.carTypeId>-1){
      let observable = this.carTypeService.deleteCarType(this.carTypeModel.carTypeId);
      observable.subscribe(deleted=>{
        this.logger.debug("DeleteType: ", deleted);
        this.GetAllAddTypes();
      }, deletedError => {
        this.deletedTypeError = deletedError.message;
        this.logger.error("DeleteTypeError: ", deletedError.message);
      });
    }
  }

  public ShowSelectedCarType(val){
    let value = val.target.value;
    let itemIndex = this.carTypes.findIndex(item => item.carTypeId === +value);
    this.carTypeModel=this.carTypes[itemIndex];
    

    this.carTypeId=this.carTypeModel.carTypeId;
    this.carType=this.carTypeModel.carType;
    this.carFirm=this.carTypeModel.carFirm;
    this.carModel=this.carTypeModel.carModel;
    this.carDayPrice=this.carTypeModel.carDayPrice;
    this.carLatePrice=this.carTypeModel.carLatePrice;
    this.carYear=this.carTypeModel.carYear;
    this.selectedGear=this.carTypeModel.carGear;
  }

}