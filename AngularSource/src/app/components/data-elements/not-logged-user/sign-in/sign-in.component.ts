import { Component, OnInit, OnDestroy } from '@angular/core';
import { LoginService } from 'src/app/services/api-connections/login.service';
import { LoginUser } from 'src/app/models/LoginUser';
import { Store } from 'src/app/redux/store';
import { NgRedux } from 'ng2-redux';
import { Action } from 'src/app/redux/action';
import { ActionType } from 'src/app/redux/action-type';
import { Unsubscribe } from 'redux';

import { AuthService } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider, LinkedInLoginProvider } from "angularx-social-login";
import { SocialUser } from "angularx-social-login";
import { LogService } from 'src/app/services/log.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit, OnDestroy {
  public login:LoginUser = new LoginUser();
  public loginError="";
  private unsubscribe:Unsubscribe;

  public user: SocialUser;
  public loggedIn: boolean;
  
  constructor(private loginService: LoginService,
              private authService: AuthService,
              private redux:NgRedux<Store>,
              private logger:LogService) { }
  
  ngOnInit() {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      this.login.userLevel=1;
      this.login.userNickName=user.name;
      this.login.userPassword=user.id;
      this.login.userPicture=user.photoUrl;
      const action: Action={type:ActionType.UserLogin, payload:this.login};
      this.redux.dispatch(action);
      const action2: Action={type:ActionType.AddSocialLogged, payload:true};
      this.redux.dispatch(action2);
      this.loggedIn = (user != null);
    });

    this.unsubscribe = this.redux.subscribe(()=>{
      if (this.redux.getState().loginUser)
      {
        this.login = new LoginUser();
      }
      if (this.redux.getState().loginError!=null && this.redux.getState().loginError!==''){
        this.loginError = this.redux.getState().loginError;
        const action: Action={type:ActionType.LoginError, payload:""};
        this.redux.dispatch(action);
      }
    });
  }

  // public signIn(): void {
  //   this.loginService.login(this.login);
  // }

  //successmsg: any;  
  errmsg: any;

  public signIn(): void {
    if (environment.core==false){
      this.loginService.login(this.login) 
      .subscribe(res => {
        if (res.status === 200) {
          sessionStorage.setItem('access_token', res.body.access_token);
          let loginUser:LoginUser = new LoginUser(
            "",
            "",
            res.body.userLevel,
            res.body.userPicture,
          );
          const action: Action={type:ActionType.UserLogin, payload:loginUser};
          this.redux.dispatch(action);                                                                                    localStorage.setItem('access_token', res.body.access_token);  
        } else {  
          this.errmsg = res.status + ' - ' + res.statusText;
          const action: Action={type:ActionType.LoginError, payload:this.errmsg};
          this.redux.dispatch(action);
        }  
          }, err => {                                 
          if (err.status === 401  ) {  
            this.errmsg = 'Invalid username or password.'; 
            const action: Action={type:ActionType.LoginError, payload:this.errmsg};
            this.redux.dispatch(action); 
          } else if (err.status === 400  ) {  
            this.errmsg = 'Invalid username or password.';  
            const action: Action={type:ActionType.LoginError, payload:this.errmsg};
            this.redux.dispatch(action);
          } else {  
            this.errmsg ="Invalid username or password";  
            const action: Action={type:ActionType.LoginError, payload:this.errmsg};
            this.redux.dispatch(action);
          }  
        }
      );  
    } else {
      const observable = this.loginService.loginCore(this.login)
      observable.subscribe(res => {
        this.logger.debug("LoggedUser: ", res);
        sessionStorage.setItem('access_token', res.usertoken);
        let loginUser:LoginUser = new LoginUser(
          "",
          "",
          res.userLevel,
          res.userPicture
        );
        const action: Action={type:ActionType.UserLogin, payload:loginUser};
        this.redux.dispatch(action);    
                      }, error => {
        this.logger.error('Invalid username or password.', error.message); 
        const action: Action={type:ActionType.LoginError, payload:error.message};
        this.redux.dispatch(action); 
      });
    }
  }  

  public ngOnDestroy(): void {
    this.unsubscribe();
  }

  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  signInWithFB(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signInWithLinkedIn(): void {
    this.authService.signIn(LinkedInLoginProvider.PROVIDER_ID);
  }

  signOut(): void {
    this.authService.signOut();
  }

}
