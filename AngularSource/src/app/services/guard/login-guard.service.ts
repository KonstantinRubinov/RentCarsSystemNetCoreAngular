import { Injectable } from '@angular/core';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { CanActivate, Router } from '@angular/router';
import { Action } from '../../redux/action';
import { ActionType } from '../../redux/action-type';
import { LogService } from '.././log.service';

@Injectable({
  providedIn: 'root'
})
export class LoginGuardService implements CanActivate {

  public constructor(private redux:NgRedux<Store>, private router: Router, private logger:LogService){}

  public canActivate(): boolean {
    if (this.redux.getState().loginUser!=null && this.redux.getState().loginUser.userLevel>0){
      this.logger.debug("user ", "approved");
        return true;
    }
    const action: Action={type:ActionType.NeedSignIn, payload:true};
    this.redux.dispatch(action);
    this.router.navigate(["/home"]);
    this.logger.error("user ", "not approved");
    return false;
  }
}
