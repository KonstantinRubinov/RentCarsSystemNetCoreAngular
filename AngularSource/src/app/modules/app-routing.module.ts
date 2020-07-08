import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from "@angular/router";


import { LoginGuardService } from '../services/guard/login-guard.service';
import { ManagerGuardService } from '../services/guard/manager-guard.service';
import { AdminGuardService } from '../services/guard/admin-guard.service';
import { HomePageComponent } from '../components/data-elements/not-logged-user/home-page/home-page.component';
import { ChooseCarPageComponent } from '../components/data-elements/not-logged-user/choose-car-page/choose-car-page.component';
import { CarPriceComponent } from '../components/data-elements/not-logged-user/car-price/car-price.component';
import { ContactPageComponent } from '../components/data-elements/logged-user/contact-us/contact-page/contact-page.component';
import { CarOrderComponent } from '../components/data-elements/logged-user/car-order/car-order.component';
import { CarOrderDetailsComponent } from '../components/data-elements/logged-user/car-order-details/car-order-details.component';
import { CarReturnComponent } from '../components/data-elements/manager/car-return/car-return.component';
import { AdministrateCarsComponent } from '../components/data-elements/admin/administrate-cars/administrate-cars.component';
import { AdministrateBranchesComponent } from '../components/data-elements/admin/administrate-branches/administrate-branches.component';
import { AdministrateUsersComponent } from '../components/data-elements/admin/administrate-users/administrate-users.component';
import { AdministrateOrdersComponent } from '../components/data-elements/admin/administrate-orders/administrate-orders.component';
import { AdministrateCarTypesComponent } from '../components/data-elements/admin/administrate-car-types/administrate-car-types.component';
import { AdministratorPageComponent } from '../components/data-elements/admin/administrator-page/administrator-page.component';
import { CarDetailsComponent } from '../components/data-elements/not-logged-user/car-details/car-details.component';
import { Page404Component } from '../components/data-elements/page404/page404.component';



// ng g m modules/routing --spec false --flat

const routes: Routes = [
    { path: "home", component: HomePageComponent },
    { path: "chooseCar", component: ChooseCarPageComponent },
    { path: "price", component: CarPriceComponent },

    { path: "makeOrder", canActivate: [LoginGuardService], component: CarOrderComponent },
    { path: "orderDetails/:id", canActivate: [LoginGuardService], component: CarOrderDetailsComponent },

    { path: "contactPage", component: ContactPageComponent },

    { path: "carReturn", canActivate: [ManagerGuardService], component: CarReturnComponent },

    { path: "administrateCars", canActivate: [AdminGuardService], component: AdministrateCarsComponent },
    { path: "administrateBranches", canActivate: [AdminGuardService], component: AdministrateBranchesComponent },
    { path: "administrateUsers", canActivate: [AdminGuardService], component: AdministrateUsersComponent },
    { path: "administrateOrders", canActivate: [AdminGuardService], component: AdministrateOrdersComponent },
    { path: "administrateCarTypes", canActivate: [AdminGuardService], component: AdministrateCarTypesComponent },
    { path: "administratorPage", canActivate: [AdminGuardService], component: AdministratorPageComponent },
    
    { path: "car-details/:id", component: CarDetailsComponent },
    { path: "404",  component: Page404Component },
    


    
    { path: "", redirectTo: "home", pathMatch: "full" },
    
    { path: "**",  redirectTo: "404" },
];

@NgModule({
    declarations: [],
    imports: [CommonModule, RouterModule.forRoot(routes)]
})
export class AppRoutingModule { }
