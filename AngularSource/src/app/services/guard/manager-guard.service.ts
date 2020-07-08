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
export class ManagerGuardService implements CanActivate {

  public constructor(private router: Router, private redux:NgRedux<Store>, private logger:LogService){}

    public canActivate(): boolean {
        if (this.redux.getState().loginUser!=null && this.redux.getState().loginUser.userLevel>1){
          this.logger.debug("manager ", "approved");
            return true;
        }
        const action: Action={type:ActionType.NeedSignIn, payload:true};
        this.redux.dispatch(action);
        this.logger.error("manager ", "not approved");
        this.router.navigate(["/home"]);
        return false;
    }


  

}
