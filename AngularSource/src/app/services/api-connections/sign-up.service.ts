import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../../models/User';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { Router } from '@angular/router';
import { Action } from '../../redux/action';
import { ActionType } from '../../redux/action-type';
import { signUpUrl } from 'src/environments/environment';
import { LogService } from '../log.service';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class SignUpService {
  
  public constructor(private http: HttpClient,
                     private redux: NgRedux<Store>,
                     private router: Router,
                     private logger: LogService) { }
  
  // public signUp(userModel:User): void {
  //   let observable = this.http.post<User>(signUpUrl, userModel);
  //   observable.subscribe(user=>{
  //     const action: Action={type:ActionType.AddUser, payload:user.userLevel};
  //     this.redux.dispatch(action);
  //     this.logger.debug("signUp: ", user.userLevel);
  //     this.router.navigate(["home"]);
  //   }, error => {
  //     const action: Action={type:ActionType.SignUpError, payload:error.error};
  //     this.redux.dispatch(action);
  //     this.logger.error("signUpError: ", error.error);
  //   });
  // }

  public signUp(userModel:User): Observable<any> {
    return this.http.post<User>(signUpUrl, userModel);
  }
  
  public updateUser(user:User): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.put<User>(signUpUrl, user, { headers: he });
    observable.subscribe(user=>{
      const action: Action={type:ActionType.UpdateUser, payload:user};
      this.redux.dispatch(action);
      this.logger.debug("updateUser: ", user);
    }, error => {
      const action: Action={type:ActionType.UpdateUserError, payload:error.error};
      this.redux.dispatch(action);
      this.logger.error("updateUserError: ", error.error);
    });
  }
  
  public UploadFile(id, file): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.post<User>(signUpUrl + "file/" + id, file, { headers: he });
    observable.subscribe(user=>{
        const action: Action={type:ActionType.UpdateUser, payload:user};
        this.redux.dispatch(action);
        this.logger.debug("UploadFile: ", user);
    }, error => {
      const action: Action={type:ActionType.UpdateUserError, payload:error.error};
      this.redux.dispatch(action);
      this.logger.error("UploadFileError: ", error.error);
    });
  }
}
