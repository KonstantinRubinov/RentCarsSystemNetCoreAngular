import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/api-connections/user.service';
import { User } from 'src/app/models/User';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-administrate-users',
  templateUrl: './administrate-users.component.html',
  styleUrls: ['./administrate-users.component.css']
})
export class AdministrateUsersComponent implements OnInit {

  constructor(private userService:UserService, private logger: LogService) { }

  ngOnInit() {
    this.GetAllUsers();
  }

  public user:User = new User();
  public users:User[];
  public usersError='';
  
  public userFirstName='';
  public userLastName='';
  public userID='';
  public userNickName='';
  public userBirthDate?: any;
  public userGender='';
  public userEmail='';
  public userPassword='';
  public userPicture='';
  public userLevel = 0;
  public userImage='';

  public addUserError='';
  public updateUserError='';
  public deletedUserError='';

  private GetAllUsers(){
    let observable = this.userService.getAllUsers();
    observable.subscribe(users=>{
      this.users = users;
      this.logger.debug("GetAllUsers: ", users);
    }, usersError => {
      this.usersError = usersError.message;
      this.logger.error("GetAllUsersError: ", usersError.message);
    });
  }

  public AddUser(){
    if (this.user.userID===null || this.user.userID===''){
      this.user.userFirstName = this.userFirstName;
      this.user.userLastName = this.userLastName;
      this.user.userID = this.userID;
      this.user.userNickName = this.userNickName;
      this.user.userBirthDate = this.userBirthDate;
      this.user.userGender = this.userGender;
      this.user.userEmail = this.userEmail;
      this.user.userPassword = this.userPassword;
      this.user.userPicture = this.userPicture;
      this.user.userLevel = this.userLevel;
      this.user.userImage = this.userImage;
  
      let observable = this.userService.addUser(this.user);
      observable.subscribe(user=>{
        this.user.userID = user.userID;
        this.logger.debug("AddUser: ", user);
        this.GetAllUsers();
      }, addUserError => {
        this.addUserError = addUserError.message;
        this.logger.error("AddUserError: ", addUserError.message);
      });
    }
  }

  public UpdateUser(){
    if (this.user.userID!==null && this.user.userID!==''){
      this.user.userFirstName = this.userFirstName;
      this.user.userLastName = this.userLastName;
      this.user.userID = this.userID;
      this.user.userNickName = this.userNickName;
      this.user.userBirthDate = this.userBirthDate;
      this.user.userGender = this.userGender;
      this.user.userEmail = this.userEmail;
      this.user.userPassword = this.userPassword;
      this.user.userPicture = this.userPicture;
      this.user.userLevel = this.userLevel;
      this.user.userImage = this.userImage;
  
      let observable = this.userService.updateUser(this.user);
      observable.subscribe(user=>{
        this.user.userID = user.userID;
        this.logger.debug("UpdateUser: ", user);
        this.GetAllUsers();
      }, updateUserError => {
        this.updateUserError = updateUserError.message;
        this.logger.error("UpdateUserError: ", updateUserError.message);
      });
    }
  }

  public DeleteUser(){
    if (this.user.userID!==null && this.user.userID!==''){
      let observable = this.userService.deleteUser(this.user.userID);
      observable.subscribe(deleted=>{
        this.logger.debug("DeleteUser: ", deleted);
        this.GetAllUsers();
      }, deletedUserError => {
        this.deletedUserError = deletedUserError.message;
        this.logger.error("DeleteUserError: ", deletedUserError.message);
      });
    }
  }

  public ShowSelectedUser(val){ 
    let value = val.target.value;
    let itemIndex = this.users.findIndex(item => item.userID === value);
    this.user=this.users[itemIndex];

    this.userFirstName=this.user.userFirstName;
    this.userLastName=this.user.userLastName;
    this.userID=this.user.userID;
    this.userNickName=this.user.userNickName;
    this.userBirthDate=this.user.userBirthDate;
    this.userGender=this.user.userGender;
    this.userEmail=this.user.userEmail;
    this.userPassword=this.user.userPassword;
    this.userPicture=this.user.userPicture;
    this.userLevel=this.user.userLevel;
    this.userImage=this.user.userImage;
  }
}