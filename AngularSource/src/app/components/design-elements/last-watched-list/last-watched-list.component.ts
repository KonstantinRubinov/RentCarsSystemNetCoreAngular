import { Component, OnInit, OnDestroy } from '@angular/core';
import { Unsubscribe } from 'redux';
import { NgRedux } from 'ng2-redux';
import { Store } from 'src/app/redux/store';
import { FullCarData } from 'src/app/models/FullCarData';

@Component({
  selector: 'app-last-watched-list',
  templateUrl: './last-watched-list.component.html',
  styleUrls: ['./last-watched-list.component.css']
})
export class LastWatchedListComponent implements OnInit, OnDestroy {

  constructor(private redux:NgRedux<Store>) { }

  private unsubscribe:Unsubscribe;
  public watchedCars:FullCarData[] = [];

  ngOnInit() {
    this.unsubscribe = this.redux.subscribe(()=>{
      this.watchedCars = this.redux.getState().watchedCars;
    });
  }

  public ngOnDestroy(): void {
    this.unsubscribe();
  }

}
