import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FullCarData } from 'src/app/models/FullCarData';
import { SearchCarService } from 'src/app/services/api-connections/search-car.service';
import { LoginUser } from 'src/app/models/LoginUser';
import { Unsubscribe } from 'redux';
import { Store } from 'src/app/redux/store';
import { NgRedux } from 'ng2-redux';
import { Rent } from 'src/app/models/Rent';
import { CarForRentService } from 'src/app/services/api-connections/car-for-rent.service';
import { Branch } from 'src/app/models/Branch';
import { ActionType } from 'src/app/redux/action-type';
import { Action } from 'src/app/redux/action';
import { mainUrl } from 'src/environments/environment';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-car-details',
  templateUrl: './car-details.component.html',
  styleUrls: ['./car-details.component.css']
})
export class CarDetailsComponent implements OnInit, OnDestroy {
    private unsubscribe:Unsubscribe;
    public car: FullCarData= new FullCarData();
    public loginUser:LoginUser;
    public myLevel=0;
    public orderDateEntered:boolean = false;

    public carPrice:number;
    public orderDays:number;

    public today:string = new Date().toISOString().slice(0, 10);
    
    public rent:Rent = new Rent();
    

   constructor(private activatedRoute: ActivatedRoute,
               private searchCarService: SearchCarService,
               private carForRentService : CarForRentService,
               private redux:NgRedux<Store>,
               private logger: LogService) { }
   
  private event:any = null;
   
  public onFileChanged(event):void{
     this.event = event.target.files[0];
  }
 
  public ngOnDestroy(): void {
   this.unsubscribe();
 }


 public DateChanged(){
   if (this.rent.rentStartDate!=null && this.rent.rentEndDate!=null){
    this.orderDateEntered=true;
    this.CheckDate();
   }
 }
 
 public priceDaysError:string='';

 public CheckDate(){
    this.rent.carNumber = this.car.carNumber;
    const observable = this.carForRentService.getRentPrice(this.rent);
    observable.subscribe(priceDays => {
      this.logger.debug("CheckDate: ", priceDays);
      this.priceDaysError ='';
      this.carPrice = priceDays.carPrice;
      this.orderDays = priceDays.orderDays;
      this.car.carAvaliable=true;
    },
    error => {
      this.logger.error("CheckDateError: ", error.message);
      this.priceDaysError = error.statusText;
      this.carPrice = 0;
      this.orderDays = 0;
      this.car.carAvaliable=false;
  });
 }

 public branch:Branch = new Branch();
 public mUrl = mainUrl;

   public ngOnInit(): void {
      let id = +this.activatedRoute.snapshot.params.id;
      
      const observable = this.searchCarService.GetCarAllData(id.toString());
      observable.subscribe(oneCar => {
        this.logger.debug("GetCarAllData: ", oneCar);
        this.car = oneCar;
        const action: Action={type:ActionType.StartLoadingCarData, payload:false};
        this.redux.dispatch(action);
        
        this.branch.branchAddress = oneCar.branchAddress;
        this.branch.branchID = oneCar.carBranchID;
        this.branch.branchName = oneCar.branchName;
        this.branch.branchLat = oneCar.branchLat;
        this.branch.branchLng = oneCar.branchLng;
        
        this.searchCarService.addCarToWatched(oneCar);
          
      }, error => {
        const action: Action={type:ActionType.StartLoadingCarData, payload:false};
        this.redux.dispatch(action);
        this.logger.error("GetCarAllDataError: ", error.message);
      });

      this.unsubscribe = this.redux.subscribe(()=>{
        if (this.redux.getState().loginUser!=null){
          this.loginUser = this.redux.getState().loginUser;
        }
        if (this.loginUser!=null && this.loginUser.userLevel!=null){
          this.myLevel=this.loginUser.userLevel;
        } else{
          this.myLevel=0;
        }

        if (this.redux.getState().searchCar){
          this.rent.rentStartDate=this.redux.getState().searchCar.fromDate;
          this.rent.rentEndDate=this.redux.getState().searchCar.toDate;
          this.DateChanged();
        }

      });
  }

  public orderError='';
  public orderRent:Rent;

  public OrderCar(){
    const observable = this.carForRentService.addCarForRent(this.rent);
    observable.subscribe(rentResponse=>{
      this.orderRent=rentResponse;
      this.logger.debug("OrderCar: ", rentResponse);
    }, error => {
      this.orderError=error;
      this.logger.error("OrderCarError: ", error.message);
    });
  }
  
}