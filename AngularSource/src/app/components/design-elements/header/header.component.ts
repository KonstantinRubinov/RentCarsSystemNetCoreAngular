import { Component, OnInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { MyNavigator } from 'src/app/models/MyNavigator';
import { Unsubscribe } from 'redux';
import { Store } from 'src/app/redux/store';
import { NgRedux } from 'ng2-redux';
import { LoginUser } from 'src/app/models/LoginUser';
import { Action } from 'src/app/redux/action';
import { ActionType } from 'src/app/redux/action-type';
import { Router } from '@angular/router';
import { mainUrl } from 'src/environments/environment';

import { AuthService } from "angularx-social-login";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  private unsubscribe:Unsubscribe;
  public loginUser:LoginUser;
  public myLevel=0;
  public isLoggedIn:boolean;
  
  public mUrl=mainUrl;

  @ViewChild('signUpModal') private signUpModal: ElementRef;
  @ViewChild('signInModal') private signInModal: ElementRef;

  @ViewChild('signUp') private signUp: ElementRef;
  @ViewChild('signIn') private signIn: ElementRef;

  
  
  constructor(private router: Router, private authService: AuthService, private redux:NgRedux<Store>) { }

  public ngOnInit() {
    this.unsubscribe = this.redux.subscribe(()=>{
      this.loginUser = this.redux.getState().loginUser;
      this.isLoggedIn = this.redux.getState().isLoggedIn;
      if (this.isLoggedIn){
        this.signUpModal.nativeElement.click();
        this.signInModal.nativeElement.click();
        this.myLevel=this.loginUser.userLevel;

        if(this.redux.getState().socialLogged){
          this.mUrl='';
        } else {
          this.mUrl=mainUrl;
        }
      }
  
      if (this.redux.getState().needSignIn){
        this.signIn.nativeElement.click();
        const action: Action={type:ActionType.NeedSignIn, payload:false};
        this.redux.dispatch(action);
      }
    });
  }

  public logout(): void {
    this.authService.signOut();
    const action: Action={type:ActionType.UserLogOut};
    this.redux.dispatch(action);
    const action2: Action={type:ActionType.RemoveSocialLogged, payload:false};
    this.redux.dispatch(action2);
    this.myLevel=0;
    this.router.navigate(["/home"]);
  }
  
  navigators = [
    new MyNavigator("/home", 'Home'),
    new MyNavigator("/chooseCar", 'Choose Car'),
    new MyNavigator("", 'SignUp'),
    new MyNavigator("", 'SignIn'),
    new MyNavigator("", 'SignOut'),
    new MyNavigator("/administratorPage", 'Admin'),
    new MyNavigator("/contactPage", 'Contact Us'),
  ];


  public ngOnDestroy(): void {
    this.unsubscribe();
  }
}
