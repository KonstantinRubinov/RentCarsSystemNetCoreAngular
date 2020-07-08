import { Injectable } from '@angular/core';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { Router, CanActivate } from '@angular/router';
import { ActionType } from '../../redux/action-type';
import { Action } from '../../redux/action';
import { LogService } from '.././log.service';


@Injectable({
  providedIn: 'root'
})
export class AdminGuardService implements CanActivate{

  public constructor(private redux:NgRedux<Store>, private router: Router, private logger:LogService){}

  public canActivate(): boolean {
    if (this.redux.getState().loginUser!=null && this.redux.getState().loginUser.userLevel>2){
        this.logger.debug("admin ", "approved");
        return true;
    }
    const action: Action={type:ActionType.NeedSignIn, payload:true};
    this.redux.dispatch(action);
    this.router.navigate(["/home"]);
    this.logger.error("admin ", "not approved");
    return false;
}
}
