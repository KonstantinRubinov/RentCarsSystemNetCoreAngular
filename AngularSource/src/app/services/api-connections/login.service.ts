import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { LoginUser } from '../../models/LoginUser';
import { mainUrl } from 'src/environments/environment';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  
  public constructor(private http: HttpClient, private redux:NgRedux<Store>) { }

    // public login(loginUser:LoginUser): void {
    //   let observable = this.http.post<User>(loginUrl, loginUser);
    //   observable.subscribe(loggedUser=>{
    //     const action: Action={type:ActionType.UserLogin, payload:loggedUser};
    //     this.redux.dispatch(action);
    //   }, error => {
    //     const action: Action={type:ActionType.LoginError, payload:error.message};
    //     this.redux.dispatch(action);
    //   });
    // }

    public login(loginUser:LoginUser): any {
      const body = new HttpParams()          
      .set('grant_type', 'password')          
      .set('username', loginUser.userNickName)    
      .set('password', loginUser.userPassword)    
  
      return this.http.post(mainUrl + 'token', body.toString(), {observe: 'response',    
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },    
      });
    }
  
    public loginCore(loginUser:LoginUser): any {
      return this.http.post<LoginUser>(mainUrl + 'token', loginUser);
    }
}
