import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ImageCropperModule } from 'ngx-image-cropper';
import {DatePipe} from '@angular/common';

import { SocialLoginModule, AuthServiceConfig } from "angularx-social-login";
import { GoogleLoginProvider, FacebookLoginProvider, LinkedInLoginProvider} from "angularx-social-login";

import { AppRoutingModule } from './modules/app-routing.module';
import { RoutingModule } from './modules/routing.module';

import { NgReduxModule, NgRedux } from 'ng2-redux';
import { Store } from './redux/store';
import { Reducer } from './redux/reducer';

import { AgmCoreModule } from '@agm/core';
import { CommonModule } from '@angular/common';
import { RotatedImageComponent } from './components/design-elements/rotated-image/rotated-image.component';


import { AdministrateCarsComponent } from './components/data-elements/admin/administrate-cars/administrate-cars.component';
import { AdministrateUsersComponent } from './components/data-elements/admin/administrate-users/administrate-users.component';
import { HomePageComponent } from './components/data-elements/not-logged-user/home-page/home-page.component';
import { ChooseCarComponent } from './components/data-elements/not-logged-user/choose-car/choose-car.component';
import { CarPriceComponent } from './components/data-elements/not-logged-user/car-price/car-price.component';
import { CarOrderComponent } from './components/data-elements/logged-user/car-order/car-order.component';
import { CarOrderDetailsComponent } from './components/data-elements/logged-user/car-order-details/car-order-details.component';
import { CarReturnComponent } from './components/data-elements/manager/car-return/car-return.component';
import { Page404Component } from './components/data-elements/page404/page404.component';
import { LayoutComponent } from './components/design-elements/layout/layout.component';
import { HeaderComponent } from './components/design-elements/header/header.component';
import { FooterComponent } from './components/design-elements/footer/footer.component';
import { SignUpComponent } from './components/data-elements/not-logged-user/sign-up/sign-up.component';
import { SignInComponent } from './components/data-elements/not-logged-user/sign-in/sign-in.component';
import { HomeWellcomeComponent } from './components/data-elements/not-logged-user/home-wellcome/home-wellcome.component';
import { CarCardHorizontalComponent } from './components/data-elements/cards/car-card-horizontal/car-card-horizontal.component';
import { CarCardVerticalComponent } from './components/data-elements/cards/car-card-vertical/car-card-vertical.component';
import { OurCarsComponent } from './components/data-elements/not-logged-user/our-cars/our-cars.component';
import { ChooseCarPageComponent } from './components/data-elements/not-logged-user/choose-car-page/choose-car-page.component';
import { FindCarComponent } from './components/data-elements/not-logged-user/find-car/find-car.component';
import { AdministrateOrdersComponent } from './components/data-elements/admin/administrate-orders/administrate-orders.component';
import { AdministrateCarTypesComponent } from './components/data-elements/admin/administrate-car-types/administrate-car-types.component';
import { AdministrateBranchesComponent } from './components/data-elements/admin/administrate-branches/administrate-branches.component';
import { CarDetailsComponent } from './components/data-elements/not-logged-user/car-details/car-details.component';
import { AdministratorPageComponent } from './components/data-elements/admin/administrator-page/administrator-page.component';
import { MapComponent } from './components/design-elements/map/map.component';
import { ContactFormComponent } from './components/data-elements/logged-user/contact-us/contact-form/contact-form.component';
import { ContactPageComponent } from './components/data-elements/logged-user/contact-us/contact-page/contact-page.component';
import { LastWatchedListComponent } from './components/design-elements/last-watched-list/last-watched-list.component';
import { LastWatchedCarComponent } from './components/design-elements/last-watched-car/last-watched-car.component';


const config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider('624796833023-clhjgupm0pu6vgga7k5i5bsfp6qp6egh.apps.googleusercontent.com')
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider('561602290896109')
  },
  {
    id: LinkedInLoginProvider.PROVIDER_ID,
    provider: new LinkedInLoginProvider('78iqy5cu2e1fgr')
  }
]);
 
export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [
    AdministrateCarsComponent,
    AdministrateUsersComponent,
    HomePageComponent,
    ChooseCarComponent,
    CarPriceComponent,
    CarOrderComponent,
    CarOrderDetailsComponent,
    CarReturnComponent,
    Page404Component,
    LayoutComponent,
    HeaderComponent,
    FooterComponent,
    SignUpComponent,
    SignInComponent,
    HomeWellcomeComponent,
    CarCardHorizontalComponent,
    CarCardVerticalComponent,
    OurCarsComponent,
    ChooseCarPageComponent,
    FindCarComponent,
    AdministrateOrdersComponent,
    AdministrateCarTypesComponent,
    AdministrateBranchesComponent,
    CarDetailsComponent,
    AdministratorPageComponent,
    RotatedImageComponent,
    MapComponent,
    ContactFormComponent,
    ContactPageComponent,
    LastWatchedListComponent,
    LastWatchedCarComponent,
  ],
  imports: [
    ImageCropperModule,
    BrowserModule,
    RouterModule,
    RoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgReduxModule,
    CommonModule,
    SocialLoginModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAZNqPlZyKbbQ2rmhGcy5sGC6DEx4dyDGw'
    })
  ],
  providers: [DatePipe, {
    provide: AuthServiceConfig,
    useFactory: provideConfig
  }],
  bootstrap: [LayoutComponent]
})
export class AppModule { 
  public constructor(redux:NgRedux<Store>){
    redux.configureStore(Reducer.reduce, new Store());
  }
}
