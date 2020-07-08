export class User {
    // private logger: LogService = new LogService();

    // private _userID: string;
    // private _userFirstName: string;
    // private _userLastName: string;
    // private _userNickName: string;
    // private _userPassword: string;
    // private _userEmail: string;
    // private _userGender: string;
    // private _userBirthDate: Date;
    // private _userPicture: string;
    // private _userLevel:number;
    // private _userImage: string;

    public constructor(
        public userID?: string,
        public userFirstName?: string,
        public userLastName?: string,
        public userNickName?: string,
        public userPassword?: string,
        public userEmail?: string,
        public userGender?: string,
        public userBirthDate?: Date,
        public userPicture?: string,
        public userLevel?:number,
        public userImage?:string
    ) { 
         // this.userFirstName = userFirstName;
        // this.userLastName = userLastName;
        // this.userID = userID;
        // this.userNickName = userNickName;
        // this.userBirthDate = userBirthDate;
        // this.userGender = userGender;
        // this.userEmail = userEmail;
        // this.userPassword = userPassword;
        // this.userPicture = userPicture;
        // this.userImage = userImage;
        // this.userLevel=userLevel;
    }

    // get userFirstName():string{
    //     this.logger.debug("get userFirstName: ", this._userFirstName);
    //     return this._userFirstName;
    // }

    // set userFirstName(val){
    //     this._userFirstName=val;
    //     this.logger.debug("set userFirstName: ", this._userFirstName);
    // }

    // get userLastName():string{
    //     this.logger.debug("get userLastName: ", this._userLastName);
    //     return this._userLastName;
    // }

    // set userLastName(val){
    //     this._userLastName=val;
    //     this.logger.debug("set userLastName: ", this._userLastName);
    // }

    // get userID():string{
    //     this.logger.debug("get userID: ", this._userID);
    //     return this._userID;
    // }

    // set userID(val){
    //     this._userID=val;
    //     this.logger.debug("set userID: ", this._userID);
    // }

    // get userNickName():string{
    //     this.logger.debug("get userNickName: ", this._userNickName);
    //     return this._userNickName;
    // }

    // set userNickName(val){
    //     this._userNickName=val;
    //     this.logger.debug("set userNickName: ", this._userNickName);
    // }

    // get userBirthDate():Date{
    //     this.logger.debug("get userBirthDate: ", this._userBirthDate);
    //     if(this._userBirthDate instanceof Date){
    //         return this._userBirthDate;
    //     }
    //     return new Date();
    // }

    // set userBirthDate(val){
    //     this._userBirthDate=val;
    //     //this._userBirthDate=new Date(val);
    //     this.logger.debug("set userBirthDate: ", this._userBirthDate);
    // }

    // get userGender():string{
    //     this.logger.debug("get userGender: ", this._userGender);
    //     return this._userGender;
    // }

    // set userGender(val){
    //     this._userGender=val;
    //     this.logger.debug("set userGender: ", this._userGender);
    // }
    
    // get userEmail():string{
    //     this.logger.debug("get userEmail: ", this._userEmail);
    //     return this._userEmail;
    // }

    // set userEmail(val){
    //     this._userEmail=val;
    //     this.logger.debug("set userEmail: ", this._userEmail);
    // }

    // get userPassword():string{
    //     this.logger.debug("get userPassword: ", this._userPassword);
    //     return this._userPassword;
    // }

    // set userPassword(val){
    //     this._userPassword=val;
    //     this.logger.debug("set userPassword: ", this._userPassword);
    // }

    // get userPicture():string{
    //     this.logger.debug("get userPicture: ", this._userPicture);
    //     return this._userPicture;
    // }

    // set userPicture(val){
    //     this._userPicture=val;
    //     this.logger.debug("set userPicture: ", this._userPicture);
    // }

    // get userImage():string{
    //     this.logger.debug("get userImage: ", this._userImage);
    //     return this._userImage;
    // }

    // set userImage(val){
    //     this._userImage=val;
    //     this.logger.debug("set userImage: ", this._userImage);
    // }

    // get userLevel():number{
    //     this.logger.debug("get userLevel: ", this._userLevel);
    //     return this._userLevel;
    // }

    // set userLevel(val){
    //     this._userLevel=val;
    //     this.logger.debug("set userLevel: ", this._userLevel);
    // }
}