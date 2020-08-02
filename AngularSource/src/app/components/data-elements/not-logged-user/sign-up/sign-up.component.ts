import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { SignUpService } from 'src/app/services/api-connections/sign-up.service';
import { Router } from '@angular/router';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { Store } from 'src/app/redux/store';
import { NgRedux } from 'ng2-redux';
import { Action } from 'src/app/redux/action';
import { ActionType } from 'src/app/redux/action-type';
import { environment } from 'src/environments/environment';
import { LoginService } from 'src/app/services/api-connections/login.service';
import { LogService } from 'src/app/services/log.service';
import { LoginUser } from 'src/app/models/LoginUser';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
   public userFirstName: string;
   public userLastName: string;
   public userID: string;
   public userNickName: string;
   public userBirthDate: Date;
   public userGender: string;
   public userEmail: string;
   public userPassword: string;
   public userPicture:string;
   public userImage: string;
   
   public userPassword2:string;
   public rightFile=true;
   public extension:string;

   public genderChoises = [
		"Male",
		"Female",
		"Other"
   ];

   
   public imageChangedEvent: any = '';
   public fileChangeEvent(event) {
      this.imageChangedEvent = event;
      let files = event.target.files;

      let fileName = files[0].name;
      // let fileType = files[0].type;
      // let fileSize = files[0].size;
      
      this.userPicture = fileName;
      let extensions = this.userPicture.split(".");
      this.extension = extensions[extensions.length-1].toLowerCase();
      let regex = new RegExp("(jpg|png|jpeg|gif)$"); 
      let regexTest = regex.test(this.extension);

      if (regexTest){
         this.rightFile=true;
         // var reader = new FileReader();
         // reader.onload = this.handleReaderLoaded.bind(this);
         // reader.readAsBinaryString(files[0]);
      } else {
         this.rightFile=false;
      }
      
   }



   private handleReaderLoaded(readerEvt) {
      var binaryString = readerEvt.target.result;
      this.userImage = btoa(binaryString);  // Converting binary string data.
   }

   public IdLabelColor = {
      'color':'black'
   };

   constructor(private signUpService: SignUpService,
               private router: Router,
               private logger:LogService,
               private loginService: LoginService,
               private redux:NgRedux<Store>) { }
  
   // public signUp(): void {
   //    let idIsRight = this.ValidateID(this.user.userId)
   //    if (this.password===this.password2){
   //       this.user.userPassword=this.password;
   //    }
   //    if (idIsRight===1 && this.password===this.password2){
   //       this.userService.signUp(this.user);
   //    }
   // }

   errmsg: any;

   public signUp(): void {
      let idIsRight = this.ValidateID(this.userID)
      if (this.userPassword===this.userPassword2){
         this.userPassword=this.userPassword;
      }
      if (idIsRight===1 && this.userPassword===this.userPassword2){
         let user:User=new User(
            this.userID,
            this.userFirstName,
            this.userLastName,
            this.userNickName,
            this.userPassword,
            this.userEmail,
            this.userGender,
            this.userBirthDate,
            this.userPicture,
            1,
            this.userImage
         );
         this.signUpService.signUp(user).subscribe(user=>{
               if(user.hasOwnProperty('result')){
                  user=user.result;
               }
               this.logger.debug("signUp: ", user);
               let loginUser:LoginUser = new LoginUser();
               loginUser.userNickName= user.userNickName;
               loginUser.userPassword= user.userPassword;
               this.signIn(loginUser);
            }, error => {
               const action: Action={type:ActionType.SignUpError, payload:error.error};
               this.redux.dispatch(action);
               this.logger.error("signUpError: ", error.message);
            }
         );
      }
   }

   public signIn(loginUser:LoginUser): void {
      if (environment.core==false){
         this.loginService.login(loginUser) 
            .subscribe(res => {    
               if (res.status === 200) { 
                  sessionStorage.setItem('access_token', res.body.access_token);
                  let loginUser:LoginUser = new LoginUser();
                  loginUser.userNickName=res.body.userNickName;
                  loginUser.userPicture=res.body.userPicture;
                  const action: Action={type:ActionType.UserLogin, payload:loginUser};
                  this.redux.dispatch(action);                                                                                    localStorage.setItem('access_token', res.body.access_token);  
               } else {  
                  this.errmsg = res.status + ' - ' + res.statusText;
                  const action: Action={type:ActionType.LoginError, payload:this.errmsg};
                  this.redux.dispatch(action);
               }  
            },  
                     err => {                                 
               if (err.status === 401  ) {  
                  this.errmsg = 'Invalid username or password.'; 
                  const action: Action={type:ActionType.LoginError, payload:this.errmsg};
                  this.redux.dispatch(action); 
               } else if (err.status === 400  ) {  
                  this.errmsg = 'Invalid username or password.';  
                  const action: Action={type:ActionType.LoginError, payload:this.errmsg};
                  this.redux.dispatch(action);
               } else {  
                  this.errmsg ="Invalid username or password";  
                  const action: Action={type:ActionType.LoginError, payload:this.errmsg};
                  this.redux.dispatch(action);
               }  
            });  
      } else {
         const observable = this.loginService.loginCore(loginUser)
         observable.subscribe(res => {
            this.logger.debug("LoggedUser: ", res);
            sessionStorage.setItem('access_token', res.usertoken);
            let loginUser:LoginUser = new LoginUser();
            loginUser.userNickName=res.userNickName;
            loginUser.userPicture=res.userPicture;
            const action: Action={type:ActionType.UserLogin, payload:loginUser};
            this.redux.dispatch(action);    
                           }, error => {
            this.logger.error('Invalid username or password.', error.message); 
            const action: Action={type:ActionType.LoginError, payload:error.message};
            this.redux.dispatch(action); 
                           }
         );
      }
   }

   ngOnInit() {}
   
   public ValidateRetypePassword(){
      if (this.userPassword!=null && this.userPassword2!=null &&this.userPassword!==this.userPassword2){
         return false;
      } else return true;
   }
  
   public ValidateID(IDnum:string):number
   {
      var R_ELEGAL_INPUT = -1;
      var R_NOT_VALID = -2;
      var R_VALID = 1; 
      
      if (IDnum==null || IDnum==='' ||IDnum.length==0){
         return R_ELEGAL_INPUT;
      }
      
      // Validate correct input
      if ((IDnum.length > 9) || (IDnum.length < 5)){
         return R_ELEGAL_INPUT; 
      }
      if (!this.isANumber(IDnum)){
         return R_ELEGAL_INPUT;
      }
      
      // The number is too short - add leading 0000
      if (IDnum.length < 9)
      {
         while(IDnum.length < 9)
         {
            IDnum = '0' + IDnum;         
         }
      }
   
      // CHECK THE ID NUMBER
      var mone = 0, incNum;
      for (var i=0; i < 9; i++)
      {
         incNum = Number(IDnum.charAt(i));
         incNum *= (i%2)+1;
         if (incNum > 9)
            incNum -= 9;
         mone += incNum;
      }
      if (mone%10 == 0)
         return R_VALID;
      else
         return R_NOT_VALID;
   }

   private isANumber(str){
      return !/\D/.test(str);
   }

   
   public croppedImage: any = '';

  
   public imageCropped(event: ImageCroppedEvent) {
      this.croppedImage = event.base64;
      let file = event.file;
      var reader = new FileReader();
      reader.onload = this.handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
   }

   public imageLoaded() {
      // show cropper
   }

   public loadImageFailed() {
      // show message
   }
}