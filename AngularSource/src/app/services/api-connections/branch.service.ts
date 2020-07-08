import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Branch } from '../../models/Branch';
import { branchesUrl, branchIdsUrl } from 'src/environments/environment';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { Action } from '../../redux/action';
import { ActionType } from '../../redux/action-type';
import { Observable } from 'rxjs';
import { LogService } from '../log.service';

@Injectable({
  providedIn: 'root'
})
export class BranchService {

  public constructor(private http: HttpClient,
                     private redux:NgRedux<Store>,
                     private logger:LogService) {}
  

                     
  public getAllBrancheIds(): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<Branch[]>(branchIdsUrl, { headers: he });
    observable.subscribe(branches=>{
      const action: Action={type:ActionType.GetAllBranchIds, payload:branches};
      this.redux.dispatch(action);
      this.logger.debug("GetAllBranchIds: ", branches);
    }, error => {
      const action: Action={type:ActionType.GetAllBranchIdsError, payload:error.message};
      this.redux.dispatch(action);
      this.logger.error("GetAllBranchIdsError: ", error.message);
    });
  }
  
  public getOneBranchById(branchId: number): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<Branch>(branchesUrl + branchId, { headers: he });
    observable.subscribe(branch=>{
      const action: Action={type:ActionType.GetOneBranch, payload:branch};
      this.redux.dispatch(action);
      this.logger.debug("GetOneBranch: ", branch);
    }, error => {
      const action: Action={type:ActionType.GetOneBranchError, payload:error.message};
      this.redux.dispatch(action);
      this.logger.error("GetOneBranchError: ", error.message);
    });
  }
  
  public addBranch(branch: Branch): Observable<Branch> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.post<Branch>(branchesUrl, branch, { headers: he });
  }
  
  public updateBranch(branch: Branch) :Observable<Branch> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.put<Branch>(branchesUrl+branch.branchID, branch, { headers: he });
  }

  public deleteBranch(id: number): Observable<any> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.delete<any>(branchesUrl+id, { headers: he });
  }

  public getAllBranches():  Observable<Branch[]> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.get<Branch[]>(branchesUrl, { headers: he });
  }












  // public getOneBranchByName(branchName: string): void {
  //   let observable = this.http.get<Branch>(branchesUrl + '/name/' + branchName);
  //   observable.subscribe(branch=>{
  //     const action: Action={type:ActionType.GetOneBranch, payload:branch};
  //     this.redux.dispatch(action);
  //   }, error => {
  //     const action: Action={type:ActionType.GetOneBranchError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("GetOneBranchError: ", error.message);
  //   });
  // }

  // public addBranch(branch: Branch): void {
  //   let observable = this.http.post<Branch>(branchesUrl, branch);
  //   observable.subscribe(branch=>{
  //     const action: Action={type:ActionType.AddBranch, payload:branch};
  //     this.redux.dispatch(action);
  //   }, error => {
  //     const action: Action={type:ActionType.AddBranchError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("AddBranchError: ", error.message);
  //   });
  // }


  // public updateBranch(branch: Branch): void {
  //   let observable = this.http.put<Branch>(branchesUrl + branch.branchName, branch);
  //   observable.subscribe(branch=>{
  //     const action: Action={type:ActionType.UpdateBranch, payload:branch};
  //     this.redux.dispatch(action);
  //   }, error => {
  //     const action: Action={type:ActionType.UpdateBranchError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("UpdateBranchError: ", error.message);
  //   });
  // }


  // public deleteBranch(branchName: string): void {
  //   let observable = this.http.delete<string>(branchesUrl, branchName);
  //   observable.subscribe(branch=>{
  //     const action: Action={type:ActionType.DeleteBranch, payload:branch};
  //     this.redux.dispatch(action);
  //   }, error => {
  //     const action: Action={type:ActionType.DeleteBranchError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("DeleteBranchError: ", error.message);
  //   });
  // }

  // public deleteBranch(id: number): void {
  //   let observable = this.http.delete<number>(branchesUrl, id);
  //   observable.subscribe(branch=>{
  //     const action: Action={type:ActionType.DeleteBranch, payload:branch};
  //     this.redux.dispatch(action);
  //   }, error => {
  //     const action: Action={type:ActionType.DeleteBranchError, payload:error.message};
  //     this.redux.dispatch(action);
  //     this.logger.error("DeleteBranchError: ", error.message);
  //   });
  // }
}
