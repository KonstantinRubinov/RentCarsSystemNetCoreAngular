import { Component, OnInit } from '@angular/core';
import { Branch } from 'src/app/models/Branch';
import { BranchService } from 'src/app/services/api-connections/branch.service';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-administrate-branches',
  templateUrl: './administrate-branches.component.html',
  styleUrls: ['./administrate-branches.component.css']
})
export class AdministrateBranchesComponent implements OnInit{
  
  ngOnInit(): void {
    this.GetAllBranches();
  }

  constructor(private branchService:BranchService, private logger:LogService) { }

  public branch:Branch = new Branch();
  public branches:Branch[];
  public branchesError='';

  public branchID=0;
  public branchCity='';
  public branchName='';
  public branchStreet='';
  public branchLatitude:number;
  public branchLongitude:number;
  public addBranchError='';
  public updateBranchError='';
  public deletedBranchError='';
  

  private GetAllBranches(){
    let observable = this.branchService.getAllBranches();
    observable.subscribe(branches=>{
      this.branches = branches;
      this.logger.debug("GetAllBranches: ", branches);
    }, branchesError => {
      this.branchesError = branchesError.message;
      this.logger.error("GetAllBranchesError: ", branchesError.message);
    });
  }
  
  
  public AddBranch(){
    if (this.branch.branchID===null || this.branch.branchID===-1){
      this.branch.branchAddress = this.branchCity + ', ' + this.branchName + ', ' + this.branchStreet;
      this.branch.branchName = this.branchName;
      this.branch.branchLat = this.branchLatitude;
      this.branch.branchLng = this.branchLongitude;
  
      let observable = this.branchService.addBranch(this.branch);
      observable.subscribe(branchType=>{
        this.branch.branchID = branchType.branchID;
        this.logger.debug("AddBranch: ", branchType);
        this.GetAllBranches();
      }, branchError => {
        this.addBranchError = branchError.message;
        this.logger.error("AddBranchError: ", branchError.message);
      });
    }
  }

  public UpdateBranch(){
    if (this.branch.branchID!==null && this.branch.branchID>-1){
      this.branch.branchAddress = this.branchCity + ', ' + this.branchName + ', ' + this.branchStreet;
      this.branch.branchName = this.branchName;
      this.branch.branchLat = this.branchLatitude;
      this.branch.branchLng = this.branchLongitude;
  
      let observable = this.branchService.updateBranch(this.branch);
      observable.subscribe(branchType=>{
        this.branch.branchID = branchType.branchID;
        this.logger.debug("UpdateBranch: ", branchType);
        this.GetAllBranches();
      }, branchError => {
        this.updateBranchError = branchError.message;
        this.logger.error("UpdateBranchError: ", branchError.message);
      });
    }
  }

  public DeleteBranch(){
    if (this.branch.branchID!==null && this.branch.branchID>-1){
      let observable = this.branchService.deleteBranch(this.branch.branchID);
      observable.subscribe(deleted=>{
        this.logger.debug("DeleteBranch: ", deleted);
        this.GetAllBranches();
      }, deletedError => {
        this.deletedBranchError = deletedError.message;
        this.logger.error("DeleteBranchError: ", deletedError.message);
      });
    }
  }

  
  
  private strToSplitBy=", ";
  public ShowSelectedBranch(val){ 
    let value = val.target.value;
    let itemIndex = this.branches.findIndex(item => item.branchID === +value);
    this.branch=this.branches[itemIndex];

    let splitted = this.branch.branchAddress.split(this.strToSplitBy, 3);

    this.branchCity=splitted[0];
    this.branchName=splitted[1];
    this.branchStreet=splitted[2];
    this.branchLatitude = this.branch.branchLat;
    this.branchLongitude = this.branch.branchLng;
  }

  

}
