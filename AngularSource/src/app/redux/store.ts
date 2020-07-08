import { Branch } from '../models/Branch';
import { Car } from '../models/Car';
import { Rent } from '../models/Rent';
import { CarType } from '../models/CarType';
import { User } from '../models/User';
import { FullCarData } from '../models/FullCarData';
import { LoginUser } from '../models/LoginUser';
import { Contact } from '../models/Contact';
import { SearchReturn } from '../models/SearchReturn';

export class Store{
    public branches:Branch[] = [];
    public branchIds:Branch[] = [];
    public branch:Branch;
    public favoriteBrancheError:string;
    public favoriteBranchesError:string;
    public favoriteBrancheIdsError:string;
    public addFavoriteBranchesError:string;
    public updateFavoriteBranchesError:string;
    public deleteFavoriteBranchesError:string;
    public clearAllBranchesError:string;
    
    public cars:Car[] = [];
    public car:Car;
    public favoriteCarsError:string;
    public addFavoriteCarsError:string;
    public updateFavoriteCarsError:string;
    public deleteFavoriteCarsError:string;
    
    public rents:FullCarData[] = [];
    public rent:Rent;
    public allFullRentsError:string;
    public oneRentError:string;
    public updateCarForRentError:string;
    public allFullCarsError:string;
    public allFullCarDataError:string;
    public carForRentError:string;
    
    public carTypes:CarType[] = [];
    // public carTypeIds:CarType[] = [];
    public types:string[]=[];
    public firms:string[]=[];
    public gears:string[]=[];
    public carType:CarType;
    public allTypesAndParseError:string;
    public allTypesError:string;
    public oneTypeError:string;
    public addTypeError:string;
    public clearAllTypesError:string;
    public deleteTypeError:string;
    public updateTypeError:string;
    
    public fullCars:FullCarData[] = [];
    public fullCarsLenth:number;
    public fullCarsPage:number;
    public fullCar:FullCarData;

    public messages:Contact[] = [];
    public message:Contact;
    public messagesError:string;
    public messageError:string;
    
    public user:User;
    public isLoggedIn=false;
    public needSignIn=false;
    public socialLogged=false;

    public loginUser:LoginUser;
    public loginError:string;
    public signUpError:string;
    public updateUserError:string;
    public userLogOutError:string;
    public removeSocialLoggedError:string;
    public addSocialLoggedError:string;
    public needSignInError:string;
    
    public watchedCars:FullCarData[] = [];
    public searchCar;
    public allCarsAllDataError:string;
    public loadingCarData = false;


    public getSearchReturnError:string;
    public searchReturn:SearchReturn = new SearchReturn();
}