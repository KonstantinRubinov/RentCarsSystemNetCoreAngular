import { Store } from "./store";
import { Action } from "./action";
import { ActionType } from "./action-type";
import { LoginUser } from '../models/LoginUser';
import { FullCarData } from '../models/FullCarData';
import { Rent } from '../models/Rent';
import { Branch } from '../models/Branch';
import { Car } from '../models/Car';
import { Contact } from '../models/Contact';
import { CarType } from '../models/CarType';
import { SearchCar } from '../models/SearchCar';

export class Reducer{
    public static reduce(oldStore: Store, action:Action):Store{
        let newStore:Store = {...oldStore};

        switch(action.type){
            case ActionType.AddUser:
                newStore.loginUser=new LoginUser(action.payload.userNickName, action.payload.userPassword, action.payload.userLevel, action.payload.userPicture);
                newStore.loginUser=action.payload;
                newStore.isLoggedIn = true;
                break;
            case ActionType.UpdateUser:
                break;
            case ActionType.UserLogin:
                newStore.loginUser=new LoginUser(action.payload.userNickName, action.payload.userPassword, action.payload.userLevel, action.payload.userPicture);
                newStore.isLoggedIn = true;
                break;
            case ActionType.LoginError:
                newStore.loginError=action.payload;
                break;
            case ActionType.SignUpError:
                newStore.signUpError=action.payload;
                break;
            case ActionType.NeedSignIn:
                newStore.needSignIn=action.payload;
                break;
            case ActionType.AddSocialLogged:
                newStore.socialLogged=true;
                break;
            case ActionType.RemoveSocialLogged:
                newStore.socialLogged=false;
                break;
            case ActionType.UserLogOut:
                newStore.loginUser=null;
                newStore.isLoggedIn = false;
                break;
                
            case ActionType.AddUserError:
                newStore.loginError=action.payload;
                break;
            case ActionType.UpdateUserError:
                newStore.updateUserError=action.payload;
                break;
            case ActionType.UserLoginError:
                newStore.loginError=action.payload;
                break;
            case ActionType.LoginError:
                newStore.loginError=action.payload;
                break;
            case ActionType.SignUpErrorError:
                newStore.signUpError=action.payload;
                break;
            case ActionType.NeedSignInError:
                newStore.needSignInError=action.payload;
                break;
            case ActionType.AddSocialLoggedError:
                newStore.addSocialLoggedError=action.payload;
                break;
            case ActionType.RemoveSocialLoggedError:
                newStore.removeSocialLoggedError=action.payload;
                break;
            case ActionType.UserLogOutError:
                newStore.userLogOutError=action.payload;
                break;
                
            case ActionType.GetAllFullCars:
                newStore.fullCars = action.payload.fullCarsData.map(x => new FullCarData(x.carNumber, x.carKm, x.carPicture, x.carInShape, x.carAvaliable, x.carBranchID, x.carType, x.carFirm, x.carModel, x.carDayPrice, x.carLatePrice, x.carYear, x.carGear, x.branchName, x.branchAddress, x.branchLat, x.branchLng));
                newStore.fullCarsLenth = action.payload.fullCarsDataLenth;
                newStore.fullCarsPage = action.payload.fullCarsDataPage;
                newStore.loadingCarData = false;
                break;
            case ActionType.GetAllFullCarData:
                newStore.fullCar = new FullCarData(action.payload.carNumber, action.payload.carKm, action.payload.carPicture, action.payload.carInShape, action.payload.carAvaliable, action.payload.carBranchID, action.payload.carType, action.payload.carFirm, action.payload.carModel, action.payload.carDayPrice, action.payload.carLatePrice, action.payload.carYear, action.payload.carGear, action.payload.branchName, action.payload.branchAddress, action.payload.branchLat, action.payload.branchLng);
                newStore.loadingCarData = false;
                break;
            case ActionType.AddCarForRent:
                newStore.rent=new Rent(action.payload.rentNumber, action.payload.userID, action.payload.carNumber, action.payload.rentStartDate, action.payload.rentEndDate, action.payload.rentRealEndDate, action.payload.carPrice, action.payload.orderDays);
                break;
            case ActionType.GetAllFullRents:
                newStore.rents=action.payload.map(x => new FullCarData(x.carNumber, x.carKm, x.carPicture, x.carInShape, x.carAvaliable, x.carBranchID, x.carType, x.carFirm, x.carModel, x.carDayPrice, x.carLatePrice, x.carYear, x.carGear, x.branchName, x.branchAddress, x.branchLat, x.branchLng));
                break;
            case ActionType.UpdateCarForRent:
                let updateCarForRent = new FullCarData(action.payload.carNumber, action.payload.carKm, action.payload.carPicture, action.payload.carInShape, action.payload.carAvaliable, action.payload.carBranchID, action.payload.carType, action.payload.carFirm, action.payload.carModel, action.payload.carDayPrice, action.payload.carLatePrice, action.payload.carYear, action.payload.carGear, action.payload.branchName, action.payload.branchAddress, action.payload.branchLat, action.payload.branchLng);
                let itemIndex = newStore.rents.findIndex(item => item === updateCarForRent);
                newStore.rents[itemIndex] = updateCarForRent;
                break;
                
            case ActionType.GetAllFullCarsError:
                newStore.allFullCarsError = action.payload
                break;
            case ActionType.GetAllFullCarDataError:
                newStore.allFullCarDataError = action.payload;
                break;
            case ActionType.AddCarForRentError:
                newStore.carForRentError=action.payload;
                break;
            case ActionType.GetAllFullRentsError:
                newStore.allFullRentsError=action.payload;
                break;
            case ActionType.UpdateCarForRentError:
                newStore.updateCarForRentError=action.payload;
                break;
                
            case ActionType.addAllWatchedCars:
                newStore.watchedCars = action.payload.map(x => new FullCarData(x.carNumber, x.carKm, x.carPicture, x.carInShape, x.carAvaliable, x.carBranchID, x.carType, x.carFirm, x.carModel, x.carDayPrice, x.carLatePrice, x.carYear, x.carGear, x.branchName, x.branchAddress, x.branchLat, x.branchLng));
                while (newStore.watchedCars.length>5){
                    newStore.watchedCars.splice(0,1);
                }
                break;
            case ActionType.addWOneWatchedCar:
                let addWOneWatchedCar = new FullCarData(action.payload.carNumber, action.payload.carKm, action.payload.carPicture, action.payload.carInShape, action.payload.carAvaliable, action.payload.carBranchID, action.payload.carType, action.payload.carFirm, action.payload.carModel, action.payload.carDayPrice, action.payload.carLatePrice, action.payload.carYear, action.payload.carGear, action.payload.branchName, action.payload.branchAddress, action.payload.branchLat, action.payload.branchLng);
                newStore.watchedCars.forEach( (item, index) => {
                    if(item.carNumber === addWOneWatchedCar.carNumber)
                        newStore.watchedCars.splice(index,1);
                });
                if (newStore.watchedCars.length>=5){
                    newStore.watchedCars.splice(0,1);
                }
                newStore.watchedCars.push(addWOneWatchedCar);
                break;
                
            case ActionType.StartLoadingCarData:
                newStore.loadingCarData = action.payload;
                break;
                
            case ActionType.GetAllBranchIds:
                newStore.branchIds = action.payload.map(x => new Branch(x.branchID, x.branchAddress, x.branchLat, x.branchLng, x.branchName));
                break;
            case ActionType.GetAllBranches:
                newStore.branches = action.payload.map(x => new Branch(x.branchID, x.branchAddress, x.branchLat, x.branchLng, x.branchName));
                break;
            case ActionType.GetOneBranch:
                newStore.branch=new Branch(action.payload.branchID, action.payload.branchAddress, action.payload.branchLat, action.payload.branchLng, action.payload.branchName);
                break;
            case ActionType.AddBranch:
                let addBranch =new Branch(action.payload.branchID, action.payload.branchAddress, action.payload.branchLat, action.payload.branchLng, action.payload.branchName);
                newStore.branches.push(addBranch);
                break;
            case ActionType.ClearAllBranches:
                newStore.branches=null;
                break;
            case ActionType.DeleteBranch:
                newStore.branches.forEach( (item, index) => {
                    if(item.branchID === action.payload.branchID)
                        newStore.branches.splice(index,1);
                });
                break;
            case ActionType.UpdateBranch:
                let updateBranch =new Branch(action.payload.branchID, action.payload.branchAddress, action.payload.branchLat, action.payload.branchLng, action.payload.branchName);
                itemIndex = newStore.branches.findIndex(item => item.branchID === updateBranch.branchID);
                    newStore.branches[itemIndex] = updateBranch;
                break;


            case ActionType.GetAllBranchIdsError:
                newStore.favoriteBrancheIdsError = action.payload;
                break;
            case ActionType.GetAllBranchesError:
                newStore.favoriteBranchesError = action.payload;
                break;
            case ActionType.GetOneBranchError:
                newStore.favoriteBrancheError=action.payload;
                break;
            case ActionType.AddBranchError:
                newStore.addFavoriteBranchesError=action.payload;
                break;
            case ActionType.ClearAllBranchesError:
                newStore.clearAllBranchesError=action.payload;
                break;
            case ActionType.DeleteBranchError:
                newStore.deleteFavoriteBranchesError=action.payload;
                break;
            case ActionType.UpdateBranchError:
                newStore.updateFavoriteBranchesError=action.payload;
                break;
                
            case ActionType.GetAllCars:
                newStore.cars = action.payload.map(x => new Car(x.carNumber, x.carTypeID, x.carKm, x.carPicture, x.carInShape, x.carAvaliable, x.carBranchID));
                break;
            case ActionType.GetOneCar:
                newStore.car=new Car(action.payload.carNumber, action.payload.carTypeID, action.payload.carKm, action.payload.carPicture, action.payload.carInShape, action.payload.carAvaliable, action.payload.carBranchID);
                break;
            case ActionType.AddCar:
                let addCar =new Car(action.payload.carNumber, action.payload.carTypeID, action.payload.carKm, action.payload.carPicture, action.payload.carInShape, action.payload.carAvaliable, action.payload.carBranchID);
                newStore.cars.push(addCar);
                break;
            case ActionType.ClearAllCars:
                newStore.cars=null;
                break;
            case ActionType.DeleteCar:
                newStore.cars.forEach( (item, index) => {
                    if(item.carNumber === action.payload.carNumber)
                        newStore.cars.splice(index,1);
                });
                break;
            case ActionType.UpdateCar:
                let updateCar =new Car(action.payload.carNumber, action.payload.carTypeID, action.payload.carKm, action.payload.carPicture, action.payload.carInShape, action.payload.carAvaliable, action.payload.carBranchID);
                itemIndex = newStore.cars.findIndex(item => item.carNumber === updateCar.carNumber);
                    newStore.cars[itemIndex] = updateCar;
                break;
                
            case ActionType.GetAllRents:
                newStore.rents = action.payload.map(x => new FullCarData(x.carNumber, x.carKm, x.carPicture, x.carInShape, x.carAvaliable, x.carBranchID, x.carType, x.carFirm, x.carModel, x.carDayPrice, x.carLatePrice, x.carYear, x.carGear, x.branchName, x.branchAddress, x.branchLat, x.branchLng));
                break;
            case ActionType.GetOneRent:
                newStore.rent==new Rent(action.payload.rentNumber, action.payload.userID, action.payload.carNumber, action.payload.rentStartDate, action.payload.rentEndDate, action.payload.rentRealEndDate, action.payload.carPrice, action.payload.orderDays);
                break;
            
            case ActionType.ClearAllRents:
                newStore.rents=null;
                break;
            case ActionType.DeleteRent:
                newStore.rents.forEach( (item, index) => {
                    if(item === action.payload) newStore.rents.splice(index,1);
                });
                break;
                
            case ActionType.GetAllMessages:
                newStore.messages = action.payload.map(x => new Contact(x.messageID, x.userID, x.userFirstName, x.userLastName, x.userEmail, x.userMessage));
                break;
            case ActionType.GetMessage:
                newStore.message=new Contact(action.payload.messageID, action.payload.userID, action.payload.userFirstName, action.payload.userLastName, action.payload.userEmail, action.payload.userMessage)
                break;

            case ActionType.GetAllMessagesError:
                newStore.messagesError = action.payload;
                break;
            case ActionType.GetMessageError:
                newStore.messageError=action.payload;
                break;
                
            case ActionType.GetAllTypesAndParse:
                var types:string[]=[];
                var firms:string[]=[];
                var gears:string[]=[];
                let carTypes = action.payload.map(x => new CarType(x.carTypeId, x.carType, x.carFirm, x.carModel, x.carDayPrice, x.carLatePrice, x.carYear, x.carGear));
                for (let i=0;i<carTypes.length;i++){
                    types.push(carTypes[i].carType);
                    firms.push(carTypes[i].carFirm);
                    gears.push(carTypes[i].carGear);
                }
                newStore.types = Array.from(new Set(types));
                newStore.firms = Array.from(new Set(firms));
                newStore.gears = Array.from(new Set(gears));
				newStore.carTypes = carTypes;
				break;
            case ActionType.GetAllTypes:
                newStore.carTypes = action.payload.map(x => new CarType(x.carTypeId, x.carType, x.carFirm, x.carModel, x.carDayPrice, x.carLatePrice, x.carYear, x.carGear));
                break;
            case ActionType.GetOneType:
                newStore.carType=new CarType(action.payload.carTypeId, action.payload.carType, action.payload.carFirm, action.payload.carModel, action.payload.carDayPrice, action.payload.carLatePrice, action.payload.carYear, action.payload.carGear);
                break;
            case ActionType.AddType:
                let addType =new CarType(action.payload.carTypeId, action.payload.carType, action.payload.carFirm, action.payload.carModel, action.payload.carDayPrice, action.payload.carLatePrice, action.payload.carYear, action.payload.carGear);
                newStore.carTypes.push(addType);
                break;
            case ActionType.ClearAllTypes:
                newStore.carTypes=null;
                break;
            case ActionType.DeleteType:
                newStore.carTypes.forEach( (item, index) => {
                    if(item.carTypeId === action.payload.carTypeId)
                        newStore.carTypes.splice(index,1);
                });
                break;
            case ActionType.UpdateType:
                let updateType =new CarType(action.payload.carTypeId, action.payload.carType, action.payload.carFirm, action.payload.carModel, action.payload.carDayPrice, action.payload.carLatePrice, action.payload.carYear, action.payload.carGear);
                itemIndex = newStore.carTypes.findIndex(item => item.carTypeId === updateType.carTypeId);
                    newStore.carTypes[itemIndex] = updateType;
                break;
                
            case ActionType.GetAllTypesAndParseError:
                newStore.allTypesAndParseError = action.payload;
                break;
            case ActionType.GetAllTypesError:
                newStore.allTypesError = action.payload;
                break;
            case ActionType.GetOneTypeError:
                newStore.oneTypeError=action.payload;
                break;
            case ActionType.AddTypeError:
                newStore.addTypeError=action.payload;
                break;
            case ActionType.ClearAllTypesError:
                newStore.clearAllTypesError=action.payload;
                break;
            case ActionType.DeleteTypeError:
                newStore.deleteTypeError=action.payload;
                break;
            case ActionType.UpdateTypeError:
                newStore.updateTypeError=action.payload;
                break;
                
            case ActionType.AddSearchCar:
                newStore.searchCar =new SearchCar(action.payload.freeSearch, action.payload.fromDate, action.payload.toDate, action.payload.company, action.payload.carType, action.payload.gear, action.payload.year);
                break;
            case ActionType.GetAllCarsAllDataError:
                newStore.allCarsAllDataError=action.payload;
                newStore.loadingCarData=false;
                break;
                
            case ActionType.GetSearchReturn:
                // newStore.searchReturn.fullCarsData = action.payload.map(x => new FullCarData(x.carNumber, x.carKm, x.carPicture, x.carInShape, x.carAvaliable, x.carBranchID, x.carType, x.carFirm, x.carModel, x.carDayPrice, x.carLatePrice, x.carYear, x.carGear, x.branchName, x.branchAddress, x.branchLat, x.branchLng));
                // newStore.searchReturn.fullCarsDataLenth = action.payload.fullCarsDataLenth;
                // newStore.searchReturn.fullCarsDataPage = action.payload.fullCarsDataPage;
                newStore.fullCars = action.payload.fullCarsData.map(x => new FullCarData(x.carNumber, x.carKm, x.carPicture, x.carInShape, x.carAvaliable, x.carBranchID, x.carType, x.carFirm, x.carModel, x.carDayPrice, x.carLatePrice, x.carYear, x.carGear, x.branchName, x.branchAddress, x.branchLat, x.branchLng));
                newStore.fullCarsLenth = action.payload.fullCarsDataLenth;
                newStore.fullCarsPage = action.payload.fullCarsDataPage;
                newStore.loadingCarData = false;
                break;
            case ActionType.GetSearchReturnError:
                newStore.getSearchReturnError=action.payload;
                break;
                
            default:
                break;
        }
        return newStore;
    }
}