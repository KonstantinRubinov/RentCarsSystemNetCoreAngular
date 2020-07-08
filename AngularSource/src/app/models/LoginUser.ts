export class LoginUser {
    // private logger: LogService = new LogService();

    // private _userNickName: string;
    // private _userPassword: string;
    // private _userLevel: number;
    // private _userPicture: string;

    public constructor(
        public userNickName: string="",
        public userPassword: string="",
        public userLevel: number = 0,
        public userPicture: string=""
    ) { 
        // this.userNickName = userNickName;
        // this.userPassword = userPassword;
        // this.userLevel = userLevel;
        // this.userPicture = userPicture;
    }

    // get userNickName():string{
    //     this.logger.debug("get userNickName: ", this._userNickName);
    //     return this._userNickName;
    // }

    // set userNickName(val){
    //     this._userNickName=val;
    //     this.logger.debug("set userNickName: ", this._userNickName);
    // }

    // get userPassword():string{
    //     this.logger.debug("get userPassword: ", this._userPassword);
    //     return this._userPassword;
    // }

    // set userPassword(val){
    //     this._userPassword=val;
    //     this.logger.debug("set userPassword: ", this._userPassword);
    // }

    // get userLevel():number{
    //     this.logger.debug("get userLevel: ", this._userLevel);
    //     return this._userLevel;
    // }

    // set userLevel(val){
    //     this._userLevel=val;
    //     this.logger.debug("set userLevel: ", this._userLevel);
    // }

    // get userPicture():string{
    //     this.logger.debug("get userPicture: ", this._userPicture);
    //     return this._userPicture;
    // }

    // set userPicture(val){
    //     this._userPicture=val;
    //     this.logger.debug("set userPicture: ", this._userPicture);
    // }
}