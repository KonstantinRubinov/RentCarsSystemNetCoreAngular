import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { Unsubscribe } from 'redux';
import { Router } from '@angular/router';
import { Branch } from 'src/app/models/Branch';
import { Store } from 'src/app/redux/store';
import { BranchService } from 'src/app/services/api-connections/branch.service';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.component.html',
  styleUrls: ['./contact-page.component.css']
})
export class ContactPageComponent implements OnInit, OnDestroy {
  private unsubscribe:Unsubscribe;
  public branch:Branch;
  constructor(private branchService: BranchService, private redux:NgRedux<Store>) { }

  public ngOnInit() {
    this.branchService.getOneBranchById(1002);
    this.unsubscribe = this.redux.subscribe(()=>{
      this.branch = this.redux.getState().branch;
    });
  }

  public ngOnDestroy(): void {
    this.unsubscribe();
  }

}
