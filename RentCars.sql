MySql

CREATE DATABASE RentCar;

CREATE TABLE RentCar.ALLCARTYPES(
	thisCarType nvarchar(50) NOT NULL,
	carFirm nvarchar(30) NOT NULL,
	carModel nvarchar(30) NOT NULL,
	carDayPrice DECIMAL(13, 2) NOT NULL,
	carLatePrice DECIMAL(13, 2) NOT NULL,
	carYear int NOT NULL,
	carGear nvarchar(30) NOT NULL,
	carTypeID int AUTO_INCREMENT,
	PRIMARY KEY ( carTypeID )
);

CREATE TABLE RentCar.BRANCHES(
	branchAddress nvarchar(50) NOT NULL,
	branchLat float NOT NULL,
	branchLng float NOT NULL,
	branchName nvarchar(50) NOT NULL,
	branchID int AUTO_INCREMENT,
	PRIMARY KEY ( branchID )
);

CREATE TABLE RentCar.CARS(
	carKm float NOT NULL,
	carPicture nvarchar(50) NOT NULL,
	carInShape bit NOT NULL,
	carAvaliable bit NOT NULL,
	carNumber nvarchar(50),
	carBranchID int NOT NULL,
	carTypeID int NOT NULL,
	PRIMARY KEY ( carNumber ),
	FOREIGN KEY (carBranchID) REFERENCES BRANCHES (branchID),
	FOREIGN KEY (carTypeID) REFERENCES ALLCARTYPES (carTypeID)
);

CREATE TABLE RentCar.ROLES(
	userLevel int,
	userRole nvarchar(15) NOT NULL,
	PRIMARY KEY ( userLevel )
);

CREATE TABLE RentCar.USERS(
	userID nvarchar(9) NOT NULL,
	userFirstName nvarchar(20) NOT NULL,
	userLastName nvarchar(20) NOT NULL,
	userNickName nvarchar(20) NOT NULL,
	userBirthDate datetime NULL,
	userGender nvarchar(10) NOT NULL,
	userEmail nvarchar(30) NOT NULL,
	userPassword nvarchar(50) NOT NULL,
	userPicture nvarchar(50) NULL,
	userLevel int NOT NULL,
	PRIMARY KEY ( userID ),
	FOREIGN KEY (userLevel) REFERENCES ROLES (userLevel)
);

CREATE TABLE RentCar.CARFORRENTS(
	rentStartDate datetime NOT NULL,
	rentEndDate datetime NOT NULL,
	rentRealEndDate datetime NULL,
	userID nvarchar(9) NOT NULL,
	carNumber nvarchar(50) NOT NULL,
	rentNumber int AUTO_INCREMENT,
	PRIMARY KEY ( rentNumber ),
	FOREIGN KEY (userID) REFERENCES USERS (userID),
	FOREIGN KEY (carNumber) REFERENCES CARS (carNumber)
);

CREATE TABLE RentCar.MESSAGES(
	messageID int AUTO_INCREMENT,
	userID nvarchar(9) NULL,
	userFirstName nvarchar(50) NOT NULL,
	userLastName nvarchar(50) NOT NULL,
	userEmail nvarchar(50) NOT NULL,
	userMessage nvarchar(1000) NOT NULL,
	PRIMARY KEY ( messageID ),
	FOREIGN KEY (userID) REFERENCES USERS (userID)
);



Delimiter $$
CREATE PROCEDURE RentCar.AddBranch (IN branchAddress varchar(50), IN branchLat float, IN branchLng float, IN branchName varchar(50))
BEGIN
	INSERT INTO Branches (branchAddress, branchLat, branchLng, branchName) VALUES (@branchAddress, @branchLat, @branchLng, @branchName);
	SELECT * FROM Branches WHERE Branches.branchID = SCOPE_IDENTITY();
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.AddCar (IN carKm float, IN carPicture varchar(50), IN carInShape bit, IN carAvaliable bit, IN carNumber varchar(50), IN carBranchID int, IN carTypeID int)
BEGIN
	INSERT INTO Cars (carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID) VALUES (carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID);
	SELECT * from Cars WHERE Cars.carNumber = carNumber;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.AddCarForRent (IN rentStartDate datetime, IN rentEndDate datetime, IN rentRealEndDate datetime, IN userID varchar(9), IN carNumber varchar(50))
BEGIN
	INSERT INTO CarForRents (rentStartDate, rentEndDate, rentRealEndDate, userID, carNumber) VALUES (rentStartDate, rentEndDate, rentRealEndDate, userID, carNumber);
	SELECT * FROM CarForRents WHERE CarForRents.rentNumber = SCOPE_IDENTITY();
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.AddCarType (IN thisCarType varchar(50), IN carFirm varchar(30), IN carModel varchar(30), IN carDayPrice DECIMAL(13, 2), IN carLatePrice DECIMAL(13, 2), IN carYear int, IN carGear varchar(30))
BEGIN
	INSERT INTO ALLCARTYPES (thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear) VALUES (thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear);
	SELECT * from ALLCARTYPES WHERE ALLCARTYPES.carTypeID = SCOPE_IDENTITY();
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.AddMessage (IN userID varchar(9), IN userFirstName varchar(50), IN userLastName varchar(50), IN userEmail varchar(50), IN userMessage varchar(1000))
BEGIN
	INSERT INTO MESSAGES (userID, userFirstName, userLastName, userEmail, userMessage) VALUES (userID, userFirstName, userLastName, userEmail, userMessage);
	SELECT * FROM MESSAGES where MESSAGES.messageID=SCOPE_IDENTITY();
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.AddRole (IN userLevel int, IN userRole varchar(15))
BEGIN
	INSERT INTO Roles (userLevel, userRole) VALUES (userLevel, userRole);
	SELECT * from Roles WHERE Roles.userLevel = userLevel;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.AddUser (IN userID varchar(9), IN userFirstName varchar(20), IN userLastName varchar(20), IN userNickName varchar(20), IN userBirthDate datetime, IN userGender varchar(10), IN userEmail varchar(30), IN userPassword varchar(50), IN userPicture varchar(50), IN userLevel int)
BEGIN
	INSERT INTO Users (userID, userFirstName, userLastName, userNickName, userBirthDate, userGender, userEmail, userPassword, userPicture, userLevel) VALUES (userID, userFirstName, userLastName, userNickName, userBirthDate, userGender, userEmail, userPassword, userPicture, userLevel);
	SELECT * from Users WHERE Users.userID = userID;
END $$
Delimiter ;




Delimiter $$
CREATE PROCEDURE RentCar.DeleteBranch (IN branchID int)
BEGIN
	DELETE FROM Branches WHERE Branches.branchID = branchID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteCar (IN carNumber varchar(50))
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;

	START TRANSACTION;
		SELECT carPicture from Cars WHERE Cars.carNumber = carNumber;
		DELETE FROM Cars WHERE Cars.carNumber = carNumber;
	COMMIT;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteCarForRentByCar (IN carNumber varchar(50))
BEGIN
	DELETE FROM CarForRents WHERE CarForRents.carNumber = carNumber;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteCarForRentByRent (IN rentNumber int)
BEGIN
	DELETE FROM CarForRents WHERE CarForRents.rentNumber = rentNumber;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteCarTypeById (IN carTypeID int)
BEGIN
	DELETE FROM ALLCARTYPES WHERE ALLCARTYPES.carTypeID = carTypeID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteCarTypeByType (IN thisCarType varchar(50))
BEGIN
	DELETE FROM ALLCARTYPES WHERE ALLCARTYPES.thisCarType = thisCarType;



Delimiter $$
CREATE PROCEDURE RentCar.DeleteMessage (IN messageID int)
BEGIN
	DELETE FROM MESSAGES WHERE MESSAGES.messageID=messageID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteMessageByUser (IN userID varchar(9))
BEGIN
	DELETE FROM MESSAGES WHERE MESSAGES.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteRole (IN userLevel int)
BEGIN
	DELETE FROM Roles WHERE Roles.userLevel = userLevel;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteRoleByRole (IN userRole varchar(15))
BEGIN
	DELETE FROM Roles WHERE Roles.userRole = userRole;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.DeleteUser (IN userID varchar(9))
BEGIN
	DELETE FROM Users WHERE Users.userID=userID;
END $$
Delimiter ;




Delimiter $$
CREATE PROCEDURE RentCar.GetAllBranches()
BEGIN
	SELECT * from Branches;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllBranchesNamesIds()
BEGIN
	SELECT branchID, branchName from Branches;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllCarImagesAndNumberOfCars()
BEGIN
	SELECT carPicture, COUNT(carNumber) FROM Cars GROUP BY carPicture;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllCars()
BEGIN
	SELECT * from Cars;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.GetAllCarsBySearch (IN page1 int, IN carsNum int, IN searchText varchar(30), IN carFirm varchar(30), IN thisCarType varchar(50), IN carGear varchar(30), IN carYear int)
BEGIN
	DECLARE TotalRows int;
	SET TotalRows = (
	SELECT COUNT(*)
	from Cars 
	left join ALLCARTYPES carTypes on Cars.carTypeID = carTypes.carTypeID
	left join Branches carBranches on Cars.carBranchID = carBranches.branchID
	where ((searchText IS NOT NULL AND searchText <> '' AND (Cars.carNumber LIKE CONCAT('%', searchText, '%') OR carTypes.carFirm LIKE CONCAT('%', searchText, '%') OR carTypes.carModel LIKE CONCAT('%', searchText, '%'))) OR (searchText IS NULL OR searchText = ''))
	AND ((carFirm IS NOT NULL AND carFirm <> '' AND carTypes.carFirm = carFirm) OR (carFirm IS NULL OR carFirm = ''))
	AND ((thisCarType IS NOT NULL AND thisCarType <> '' AND carTypes.thisCarType = thisCarType) OR (thisCarType IS NULL OR thisCarType = ''))
	AND ((carGear IS NOT NULL AND carGear <> '' AND carTypes.carGear = carGear) OR (carGear IS NULL OR carGear = ''))
	AND ((NOT carYear= 0 AND carTypes.carYear = carYear) OR (carYear = 0))
	);
	
	SELECT Cars.carNumber, Cars.carKm, Cars.carPicture, Cars.carInShape, Cars.carAvaliable, Cars.carBranchID, carTypes.thisCarType, carTypes.carFirm, carTypes.carModel, carTypes.carDayPrice, carTypes.carLatePrice, carTypes.carYear, carTypes.carGear, carBranches.branchName, carBranches.branchAddress, carBranches.branchLat, carBranches.branchLng, TotalRows
	from Cars 
	left join ALLCARTYPES carTypes on Cars.carTypeID = carTypes.carTypeID
	left join Branches carBranches on Cars.carBranchID = carBranches.branchID
	where ((searchText IS NOT NULL AND searchText <> '' AND (Cars.carNumber LIKE CONCAT('%', searchText, '%') OR carTypes.carFirm LIKE CONCAT('%', searchText, '%') OR carTypes.carModel LIKE CONCAT('%', searchText, '%'))) OR (searchText IS NULL OR searchText = ''))
	AND ((carFirm IS NOT NULL AND carFirm <> '' AND carTypes.carFirm = carFirm) OR (carFirm IS NULL OR carFirm = ''))
	AND ((thisCarType IS NOT NULL AND thisCarType <> '' AND carTypes.thisCarType = thisCarType) OR (thisCarType IS NULL OR thisCarType = ''))
	AND ((carGear IS NOT NULL AND carGear <> '' AND carTypes.carGear = carGear) OR (carGear IS NULL OR carGear = ''))
	AND ((NOT carYear= 0 AND carTypes.carYear = carYear) OR (carYear = 0))
	ORDER BY Cars.carNumber LIMIT carsNum OFFSET page1;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.GetAllCarsForRent()
BEGIN
	SELECT * from CarForRents;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllCarsForRentByCarNumber (IN carNumber varchar(50))
BEGIN
	SELECT * from CarForRents WHERE CarForRents.carNumber = carNumber;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllCarsForRentByUserId (IN userID varchar(9))
BEGIN
	SELECT CARS.carNumber, CARS.carKm, CARS.carPicture, CARS.carInShape, CARS.carAvaliable, CARS.carBranchID, ALLCARTYPES.thisCarType, ALLCARTYPES.carFirm, ALLCARTYPES.carModel, ALLCARTYPES.carDayPrice, ALLCARTYPES.carLatePrice, ALLCARTYPES.carYear, ALLCARTYPES.carGear, BRANCHES.branchName, BRANCHES.branchAddress, BRANCHES.branchLat, BRANCHES.branchLng
	From CARFORRENTS
	left JOIN CARS ON CARFORRENTS.carNumber=CARS.carNumber
	left JOIN ALLCARTYPES ON CARS.carTypeID=ALLCARTYPES.carTypeID
	left JOIN BRANCHES ON CARS.carBranchID=BRANCHES.branchID
	WHERE CARFORRENTS.userID = userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllCarTypes()
BEGIN
	SELECT * from ALLCARTYPES;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllCarTypesIds()
BEGIN
	SELECT carTypeID, thisCarType from ALLCARTYPES;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllMessages()
BEGIN
	SELECT * from MESSAGES;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllRoles()
BEGIN
	SELECT * from Roles;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetAllUsers()
BEGIN
	SELECT * from Users;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetCarAllDataBySearch (IN carNumber varchar(50))
BEGIN
	SELECT Cars.carNumber, Cars.carKm, Cars.carPicture, Cars.carInShape, Cars.carAvaliable, Cars.carBranchID, carTypes.thisCarType, carTypes.carFirm, carTypes.carModel, carTypes.carDayPrice, carTypes.carLatePrice, carTypes.carYear, carTypes.carGear, carBranches.branchName, carBranches.branchAddress, carBranches.branchLat, carBranches.branchLng
	from Cars 
	left join ALLCARTYPES carTypes on cars.carTypeID = carTypes.carTypeID
	left join Branches carBranches on cars.carBranchID = carBranches.branchID
	where Cars.carNumber=carNumber;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetCarDayPriceBySearch (IN carNumber varchar(50))
BEGIN
	SELECT carTypes.carDayPrice
	from Cars
	left join ALLCARTYPES carTypes on cars.carTypeID = carTypes.carTypeID
	where Cars.carNumber=carNumber;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetMessagesByUserId (IN userID varchar(9))
BEGIN
	SELECT * from MESSAGES WHERE MESSAGES.userID = userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetNumberOfCarWithImage (IN carPicture varchar(50))
BEGIN
	SELECT carPicture, COUNT(carNumber) FROM Cars WHERE Cars.carPicture = carPicture GROUP BY carPicture;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneBranchById (IN branchID int)
BEGIN
	SELECT * from Branches WHERE Branches.branchID = branchID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneCarByNumber (IN carNumber varchar(50))
BEGIN
	SELECT * from Cars WHERE Cars.carNumber = carNumber;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneCarForRentByRentNumber (IN rentNumber int)
BEGIN
	SELECT * from CarForRents WHERE CarForRents.rentNumber = rentNumber;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneCarType (IN carTypeID int)
BEGIN
	SELECT * from ALLCARTYPES WHERE ALLCARTYPES.carTypeID = carTypeID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneMessageById (IN messageID int)
BEGIN
	SELECT * from MESSAGES WHERE MESSAGES.messageID = messageID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneRole (IN userLevel int)
BEGIN
	SELECT * from Roles WHERE Roles.userLevel = userLevel;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneRoleByRole (IN userRole varchar(15))
BEGIN
	SELECT * from Roles WHERE Roles.userRole = userRole;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneUserById (IN userID varchar(9))
BEGIN
	SELECT * from Users WHERE Users.userID = userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneUserByLogin (IN userNickName varchar(20), IN userPassword varchar(50))
BEGIN
	SELECT * from Users WHERE Users.userNickName = userNickName and Users.userPassword=userPassword;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneUserByName (IN userNickName varchar(20))
BEGIN
	SELECT * from Users WHERE Users.userNickName = userNickName;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE RentCar.GetOneUserForMessageById (IN userID varchar(9))
BEGIN
	SELECT userFirstName, userLastName, userEmail from Users WHERE Users.userID = userID;
END $$
Delimiter ;




Delimiter $$
CREATE PROCEDURE RentCar.IsNameTaken (IN userNickName varchar(20))
BEGIN
	SELECT COUNT(1) FROM Users WHERE Users.userNickName = userNickName;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.queryUsersString()
BEGIN
	SELECT * from Users;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.ReturnUserByNameLevel (IN userNickName varchar(20), IN userLevel int)
BEGIN
	SELECT userNickName, userLevel from Users WHERE Users.userNickName=userNickName and Users.userLevel=userLevel;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.ReturnUserByNamePassword (IN userNickName varchar(20), IN userPassword varchar(50))
BEGIN
	SELECT userNickName, userLevel, userPicture from Users WHERE Users.userNickName=userNickName and Users.userPassword=userPassword;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.ReturnUserIdByUserPass (IN userPassword varchar(50))
BEGIN
	SELECT userID from Users WHERE Users.userPassword=userPassword;
END $$
Delimiter ;





Delimiter $$
CREATE PROCEDURE RentCar.UpdateBranch (IN branchAddress varchar(50), IN branchLat float, IN branchLng float, IN branchName varchar(50), IN branchID int)
BEGIN
	UPDATE Branches SET Branches.branchAddress = branchAddress, Branches.branchLat=branchLat, Branches.branchLng = branchLng, Branches.branchName = branchName WHERE Branches.branchID = branchID;
	SELECT * FROM Branches WHERE Branches.branchID = branchID;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.UpdateCar (IN carKm float, IN carPicture varchar(50), IN carInShape bit, IN carAvaliable bit, IN carNumber varchar(50), IN carBranchID int, IN carTypeID int)
BEGIN
	UPDATE Cars SET Cars.carKm = carKm, Cars.carPicture=carPicture, Cars.carInShape = carInShape, Cars.carAvaliable = carAvaliable, Cars.carNumber = carNumber, Cars.carBranchID = carBranchID, Cars.carTypeID = carTypeID WHERE Cars.carNumber = carNumber;
	SELECT * from Cars WHERE Cars.carNumber = carNumber;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.UpdateCarForRent (IN rentStartDate datetime, IN rentEndDate datetime, IN rentRealEndDate datetime, IN userID varchar(9), IN carNumber varchar(50), IN rentNumber int)
BEGIN
	UPDATE CarForRents SET CarForRents.rentStartDate=rentStartDate, CarForRents.rentEndDate = rentEndDate, CarForRents.rentRealEndDate = rentRealEndDate, CarForRents.userID = userID, CarForRents.carNumber = carNumber WHERE CarForRents.rentNumber = rentNumber;
	SELECT * FROM CarForRents WHERE CarForRents.rentNumber = rentNumber;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.UpdateCarType (IN thisCarType varchar(50), IN carFirm varchar(30), IN carModel varchar(30), IN carDayPrice DECIMAL(13, 2), IN carLatePrice DECIMAL(13, 2), IN carYear int, IN carGear varchar(30), IN carTypeID int)
BEGIN
	UPDATE ALLCARTYPES SET ALLCARTYPES.thisCarType = thisCarType, ALLCARTYPES.carFirm = carFirm, ALLCARTYPES.carModel=carModel, ALLCARTYPES.carDayPrice = carDayPrice, ALLCARTYPES.carLatePrice = carLatePrice, ALLCARTYPES.carYear = carYear, ALLCARTYPES.carGear = carGear WHERE ALLCARTYPES.carTypeID = carTypeID;
	SELECT * from ALLCARTYPES WHERE ALLCARTYPES.carTypeID = carTypeID;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.UpdateMessage (IN messageID int, IN userID varchar(9), IN userFirstName varchar(50), IN userLastName varchar(50), IN userEmail varchar(50), IN userMessage varchar(1000))
BEGIN
	UPDATE MESSAGES SET MESSAGES.userID = userID, MESSAGES.userFirstName = userFirstName, MESSAGES.userLastName = userLastName, MESSAGES.userEmail = userEmail, MESSAGES.userMessage = userMessage where MESSAGES.messageID=messageID;
	SELECT * from MESSAGES where MESSAGES.messageID = messageID;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.UpdateRole (IN userLevel int, IN userRole varchar(15))
BEGIN
	UPDATE Roles SET Roles.userLevel = userLevel, Roles.userRole = userRole WHERE Roles.userLevel = userLevel;
	SELECT * from Roles WHERE Roles.userLevel = userLevel;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.UpdateUser (IN userID varchar(9), IN userFirstName varchar(20), IN userLastName varchar(20), IN userNickName varchar(20), IN userBirthDate datetime, IN userGender varchar(10), IN userEmail varchar(30), IN userPassword varchar(50), IN userPicture varchar(50), IN userLevel int)
BEGIN
	UPDATE Users SET Users.userID = userID, Users.userFirstName = userFirstName, Users.userLastName = userLastName, Users.userNickName = userNickName, Users.userBirthDate = userBirthDate, Users.userGender = userGender, Users.userEmail = userEmail, Users.userPassword = userPassword, Users.userPicture = userPicture, Users.userLevel = userLevel where Users.userID=userID;
	SELECT * from Users WHERE Users.userID = userID;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE RentCar.UploadUserImage (IN userID varchar(9), IN userPicture varchar(50))
BEGIN
	UPDATE Users SET Users.userPicture = userPicture where Users.userID=userID;
	SELECT * FROM Users where Users.userID=userID;
END $$
Delimiter ;

//-------------------------------------------------------------------------------------------------------------------------//
Sql

CREATE DATABASE RentCar;

CREATE TABLE ALLCARTYPES(
	thisCarType nvarchar(50) NOT NULL,
	carFirm nvarchar(30) NOT NULL,
	carModel nvarchar(30) NOT NULL,
	carDayPrice money NOT NULL,
	carLatePrice money NOT NULL,
	carYear int NOT NULL,
	carGear nvarchar(30) NOT NULL,
	carTypeID int IDENTITY PRIMARY KEY
);

CREATE TABLE BRANCHES(
	branchAddress nvarchar(50) NOT NULL,
	branchLat float NOT NULL,
	branchLng float NOT NULL,
	branchName nvarchar(50) NOT NULL,
	branchID int IDENTITY PRIMARY KEY
);

CREATE TABLE CARFORRENTS(
	rentStartDate datetime NOT NULL,
	rentEndDate datetime NOT NULL,
	rentRealEndDate datetime NULL,
	userID nvarchar(9) FOREIGN KEY REFERENCES USERS(userID),
	carNumber nvarchar(50) FOREIGN KEY REFERENCES CARS(carNumber),
	rentNumber int IDENTITY PRIMARY KEY
);


CREATE TABLE CARS(
	carKm float NOT NULL,
	carPicture nvarchar(50) NOT NULL,
	carInShape bit NOT NULL,
	carAvaliable bit NOT NULL,
	carNumber nvarchar(50) PRIMARY KEY,
	carBranchID int NOT NULL FOREIGN KEY REFERENCES BRANCHES(branchID),
	carTypeID int NOT NULL FOREIGN KEY REFERENCES ALLCARTYPES(carTypeID),
);

CREATE TABLE MESSAGES(
	messageID int IDENTITY PRIMARY KEY,
	userID nvarchar(9) NULL FOREIGN KEY REFERENCES USERS(userID),
	userFirstName nvarchar(50) NOT NULL,
	userLastName nvarchar(50) NOT NULL,
	userEmail nvarchar(50) NOT NULL,
	userMessage nvarchar(max) NOT NULL
);

CREATE TABLE ROLES(
	userLevel int PRIMARY KEY,
	userRole nvarchar(15) NOT NULL
);


CREATE TABLE USERS(
	userID nvarchar(9) PRIMARY KEY,
	userFirstName nvarchar(20) NOT NULL,
	userLastName nvarchar(20) NOT NULL,
	userNickName nvarchar(20) NOT NULL,
	userBirthDate datetime NULL,
	userGender nvarchar(10) NOT NULL,
	userEmail nvarchar(30) NOT NULL,
	userPassword nvarchar(50) NOT NULL,
	userPicture nvarchar(50) NULL,
	userLevel int NOT NULL FOREIGN KEY REFERENCES ROLES(userLevel)
);

CREATE PROCEDURE [dbo].[AddBranch] (@branchAddress nvarchar(50), @branchLat float, @branchLng float, @branchName nvarchar(50))
AS
INSERT INTO Branches (branchAddress, branchLat, branchLng, branchName) VALUES (@branchAddress, @branchLat, @branchLng, @branchName);
SELECT * FROM Branches WHERE branchID = SCOPE_IDENTITY();


CREATE PROCEDURE [dbo].[AddCar] (@carKm float, @carPicture nvarchar(50), @carInShape bit, @carAvaliable bit, @carNumber nvarchar(50), @carBranchID int, @carTypeID int)
AS
INSERT INTO Cars (carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID) VALUES (@carKm, @carPicture, @carInShape, @carAvaliable, @carNumber, @carBranchID, @carTypeID);
SELECT * from Cars WHERE carNumber = @carNumber;


CREATE PROCEDURE [dbo].[AddCarForRent] (@rentStartDate datetime, @rentEndDate datetime, @rentRealEndDate datetime, @userID nvarchar(9), @carNumber nvarchar(50))
AS
INSERT INTO CarForRents (rentStartDate, rentEndDate, rentRealEndDate, userID, carNumber) VALUES (@rentStartDate, @rentEndDate, @rentRealEndDate, @userID, @carNumber);
SELECT * FROM CarForRents WHERE rentNumber = SCOPE_IDENTITY();


CREATE PROCEDURE [dbo].[AddCarType] (@thisCarType nvarchar(50), @carFirm nvarchar(30), @carModel nvarchar(30), @carDayPrice money, @carLatePrice money, @carYear int, @carGear nvarchar(30))
AS
INSERT INTO ALLCARTYPES (thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear) VALUES (@thisCarType, @carFirm, @carModel, @carDayPrice, @carLatePrice, @carYear, @carGear);
SELECT * from ALLCARTYPES WHERE carTypeID = SCOPE_IDENTITY();


CREATE PROCEDURE [dbo].[AddMessage] (@userID nvarchar(9), @userFirstName nvarchar(50), @userLastName nvarchar(50), @userEmail nvarchar(50), @userMessage nvarchar(MAX))
AS
INSERT INTO MESSAGES (userID, userFirstName, userLastName, userEmail, userMessage) VALUES (@userID, @userFirstName, @userLastName, @userEmail, @userMessage);
SELECT * FROM MESSAGES where messageID=SCOPE_IDENTITY();


CREATE PROCEDURE [dbo].[AddRole] (@userLevel int, @userRole nvarchar(15))
AS
INSERT INTO Roles (userLevel, userRole) VALUES (@userLevel, @userRole);
SELECT * from Roles WHERE userLevel = @userLevel;


CREATE PROCEDURE [dbo].[AddUser] (@userID nvarchar(9), @userFirstName nvarchar(20), @userLastName nvarchar(20), @userNickName nvarchar(20), @userBirthDate datetime, @userGender nvarchar(10), @userEmail nvarchar(30), @userPassword nvarchar(50), @userPicture nvarchar(50), @userLevel int)
AS
INSERT INTO Users (userID, userFirstName, userLastName, userNickName, userBirthDate, userGender, userEmail, userPassword, userPicture, userLevel) VALUES (@userID, @userFirstName, @userLastName, @userNickName, @userBirthDate, @userGender, @userEmail, @userPassword, @userPicture, @userLevel);
SELECT * from Users WHERE userID = @userID;


CREATE PROCEDURE [dbo].[DeleteBranch] @branchID int
AS
DELETE FROM Branches WHERE branchID = @branchID;


CREATE PROCEDURE [dbo].[DeleteCar] @carNumber nvarchar(50)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			SELECT carPicture from Cars WHERE carNumber = @carNumber;
			DELETE FROM Cars WHERE carNumber = @carNumber;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch
	
	
CREATE PROCEDURE [dbo].[DeleteCarForRentByCar] @carNumber nvarchar(50)
AS
DELETE FROM CarForRents WHERE carNumber = @carNumber;


CREATE PROCEDURE [dbo].[DeleteCarForRentByRent] @rentNumber int
AS
DELETE FROM CarForRents WHERE rentNumber = @rentNumber;


CREATE PROCEDURE [dbo].[DeleteCarTypeById] @carTypeID int
AS
DELETE FROM ALLCARTYPES WHERE carTypeID = @carTypeID;


CREATE PROCEDURE [dbo].[DeleteCarTypeByType] @thisCarType nvarchar(50)
AS
DELETE FROM ALLCARTYPES WHERE thisCarType = @thisCarType;


CREATE PROCEDURE [dbo].[DeleteMessage] @messageID int
AS
DELETE FROM MESSAGES WHERE messageID=@messageID;


CREATE PROCEDURE [dbo].[DeleteMessageByUser] @userID nvarchar(9)
AS
DELETE FROM MESSAGES WHERE userID=@userID;


CREATE PROCEDURE [dbo].[DeleteRole] @userLevel int
AS
DELETE FROM Roles WHERE userLevel = @userLevel;


CREATE PROCEDURE [dbo].[DeleteRoleByRole] @userRole nvarchar(15)
AS
DELETE FROM Roles WHERE userRole = @userRole;


CREATE PROCEDURE [dbo].[DeleteUser] @userID nvarchar(9)
AS
DELETE FROM Users WHERE userID=@userID;


CREATE PROCEDURE [dbo].[GetAllBranches]
AS
SELECT * from Branches;


CREATE PROCEDURE [dbo].[GetAllBranchesNamesIds]
AS
SELECT branchID, branchName from Branches;


CREATE PROCEDURE [dbo].[GetAllCarImagesAndNumberOfCars]
AS
SELECT carPicture, COUNT(carNumber) FROM Cars GROUP BY carPicture;;


CREATE PROCEDURE [dbo].[GetAllCars]
AS
SELECT * from Cars;


CREATE PROCEDURE [dbo].[GetAllCarsBySearch] (@page int, @carsNum int, @searchText nvarchar(30), @carFirm nvarchar(30), @thisCarType nvarchar(50), @carGear nvarchar(30), @carYear int)
AS
DECLARE @TotalRows int;
SELECT Cars.carNumber, Cars.carKm, Cars.carPicture, Cars.carInShape, Cars.carAvaliable, Cars.carBranchID, carTypes.thisCarType, carTypes.carFirm, carTypes.carModel, carTypes.carDayPrice, carTypes.carLatePrice, carTypes.carYear, carTypes.carGear, carBranches.branchName, carBranches.branchAddress, carBranches.branchLat, carBranches.branchLng, TotalRows = COUNT(*) OVER()
from Cars
left join ALLCARTYPES carTypes on Cars.carTypeID = carTypes.carTypeID
left join Branches carBranches on Cars.carBranchID = carBranches.branchID

where ((@searchText IS NOT NULL AND @searchText <> '' AND (Cars.carNumber LIKE '%'+ @searchText + '%' OR carTypes.carFirm LIKE '%' + @searchText + '%' OR carTypes.carModel LIKE '%' + @searchText + '%')) OR (@searchText IS NULL OR @searchText = ''))
AND ((@carFirm IS NOT NULL AND @carFirm <> '' AND carTypes.carFirm = @carFirm) OR (@carFirm IS NULL OR @carFirm = ''))
AND ((@thisCarType IS NOT NULL AND @thisCarType <> '' AND carTypes.thisCarType = @thisCarType) OR (@thisCarType IS NULL OR @thisCarType = ''))
AND ((@carGear IS NOT NULL AND @carGear <> '' AND carTypes.carGear = @carGear) OR (@carGear IS NULL OR @carGear = ''))
AND ((NOT @carYear= 0 AND carTypes.carYear = @carYear) OR (@carYear = 0))
ORDER BY Cars.carNumber OFFSET @page ROWS FETCH NEXT @carsNum ROWS ONLY;


CREATE PROCEDURE [dbo].[GetAllCarsForRent]
AS
SELECT * from CarForRents;


CREATE PROCEDURE [dbo].[GetAllCarsForRentByCarNumber] @carNumber nvarchar(50)
AS
SELECT * from CarForRents WHERE carNumber = @carNumberl


CREATE PROCEDURE [dbo].[GetAllCarsForRentByUserId] @userID nvarchar(9)
AS
SELECT CARS.carNumber, CARS.carKm, CARS.carPicture, CARS.carInShape, CARS.carAvaliable, CARS.carBranchID, ALLCARTYPES.thisCarType, ALLCARTYPES.carFirm, ALLCARTYPES.carModel, ALLCARTYPES.carDayPrice, ALLCARTYPES.carLatePrice, ALLCARTYPES.carYear, ALLCARTYPES.carGear, BRANCHES.branchName, BRANCHES.branchAddress, BRANCHES.branchLat, BRANCHES.branchLng
From CARFORRENTS
left JOIN CARS ON CARFORRENTS.carNumber=CARS.carNumber
left JOIN ALLCARTYPES ON CARS.carTypeID=ALLCARTYPES.carTypeID
left JOIN BRANCHES ON CARS.carBranchID=BRANCHES.branchID
WHERE CARFORRENTS.userID = @userID;


CREATE PROCEDURE [dbo].[GetAllCarTypes]
AS
SELECT * from ALLCARTYPES;


CREATE PROCEDURE [dbo].[GetAllCarTypesIds]
AS
SELECT carTypeID, thisCarType from ALLCARTYPES;


CREATE PROCEDURE [dbo].[GetAllMessages]
AS
SELECT * from MESSAGES;


CREATE PROCEDURE [dbo].[GetAllRoles]
AS
SELECT * from Roles;


CREATE PROCEDURE [dbo].[GetAllUsers]
AS
SELECT * from Users;


CREATE PROCEDURE [dbo].[GetCarAllDataBySearch] @carNumber nvarchar(50)
AS
SELECT Cars.carNumber, Cars.carKm, Cars.carPicture, Cars.carInShape, Cars.carAvaliable, Cars.carBranchID, carTypes.thisCarType, carTypes.carFirm, carTypes.carModel, carTypes.carDayPrice, carTypes.carLatePrice, carTypes.carYear, carTypes.carGear, carBranches.branchName, carBranches.branchAddress, carBranches.branchLat, carBranches.branchLng
from Cars 
left join ALLCARTYPES carTypes on cars.carTypeID = carTypes.carTypeID
left join Branches carBranches on cars.carBranchID = carBranches.branchID
where Cars.carNumber=@carNumber;


CREATE PROCEDURE [dbo].[GetCarDayPriceBySearch] @carNumber nvarchar(50)
AS
SELECT carTypes.carDayPrice
from Cars
left join ALLCARTYPES carTypes on cars.carTypeID = carTypes.carTypeID
where Cars.carNumber=@carNumber;


CREATE PROCEDURE [dbo].[GetMessagesByUserId] @userID nvarchar(9)
AS
SELECT * from MESSAGES WHERE userID = @userID;


CREATE PROCEDURE [dbo].[GetNumberOfCarWithImage] @carPicture nvarchar(50)
AS
SELECT carPicture, COUNT(carNumber) FROM Cars WHERE carPicture = @carPicture GROUP BY carPicture;


CREATE PROCEDURE [dbo].[GetOneBranchById] @branchID int
AS
SELECT * from Branches WHERE branchID = @branchID;


CREATE PROCEDURE [dbo].[GetOneCarByNumber] @carNumber nvarchar(50)
AS
SELECT * from Cars WHERE carNumber = @carNumber;


CREATE PROCEDURE [dbo].[GetOneCarForRentByRentNumber] @rentNumber int
AS
SELECT * from CarForRents WHERE rentNumber = @rentNumber;


CREATE PROCEDURE [dbo].[GetOneCarType] @carTypeID int
AS
SELECT * from ALLCARTYPES WHERE carTypeID = @carTypeID;


CREATE PROCEDURE [dbo].[GetOneMessageById] @messageID int
AS
SELECT * from MESSAGES WHERE messageID = @messageID;


CREATE PROCEDURE [dbo].[GetOneRole] @userLevel int
AS
SELECT * from Roles WHERE userLevel = @userLevel;


CREATE PROCEDURE [dbo].[GetOneRoleByRole] @userRole nvarchar(15)
AS
SELECT * from Roles WHERE userRole = @userRole;


CREATE PROCEDURE [dbo].[GetOneUserById] @userID nvarchar(9)
AS
SELECT * from Users WHERE userID = @userID;


CREATE PROCEDURE [dbo].[GetOneUserByLogin] (@userNickName nvarchar(20), @userPassword nvarchar(50))
AS
SELECT * from Users WHERE userNickName = @userNickName and userPassword=@userPassword;


CREATE PROCEDURE [dbo].[GetOneUserByName] @userNickName nvarchar(20)
AS
SELECT * from Users WHERE userNickName = @userNickName;


CREATE PROCEDURE [dbo].[GetOneUserForMessageById] @userID nvarchar(9)
AS
SELECT userFirstName, userLastName, userEmail from Users WHERE userID = @userID;


CREATE PROCEDURE [dbo].[IsNameTaken] @userNickName nvarchar(20)
AS
SELECT COUNT(1) FROM Users WHERE userNickName = @userNickName;


CREATE PROCEDURE [dbo].[queryUsersString]
AS
SELECT * from Users;


CREATE PROCEDURE [dbo].[ReturnUserByNameLevel] (@userNickName nvarchar(20), @userLevel int)
AS
SELECT userNickName, userLevel from Users WHERE userNickName=@userNickName and userLevel=@userLevel;


CREATE PROCEDURE [dbo].[ReturnUserByNamePassword] (@userNickName nvarchar(20), @userPassword nvarchar(50))
AS
SELECT userNickName, userLevel, userPicture from Users WHERE userNickName=@userNickName and userPassword=@userPassword;


CREATE PROCEDURE [dbo].[ReturnUserIdByUserPass] @userPassword nvarchar(50)
AS
SELECT userID from Users WHERE userPassword=@userPassword;


CREATE PROCEDURE [dbo].[UpdateBranch] (@branchAddress nvarchar(50), @branchLat float, @branchLng float, @branchName nvarchar(50), @branchID int)
AS
UPDATE Branches SET branchAddress = @branchAddress, branchLat=@branchLat, branchLng = @branchLng, branchName = @branchName WHERE branchID = @branchID
SELECT * FROM Branches WHERE branchID = @branchID;


CREATE PROCEDURE [dbo].[UpdateCar] (@carKm float, @carPicture nvarchar(50), @carInShape bit, @carAvaliable bit, @carNumber nvarchar(50), @carBranchID int, @carTypeID int)
AS
UPDATE Cars SET carKm = @carKm, carPicture=@carPicture, carInShape = @carInShape, carAvaliable = @carAvaliable, carNumber = @carNumber, carBranchID = @carBranchID, carTypeID = @carTypeID WHERE carNumber = @carNumber;
SELECT * from Cars WHERE carNumber = @carNumber;


CREATE PROCEDURE [dbo].[UpdateCarForRent] (@rentStartDate datetime, @rentEndDate datetime, @rentRealEndDate datetime, @userID nvarchar(9), @carNumber nvarchar(50), @rentNumber int)
AS
UPDATE CarForRents SET rentStartDate=@rentStartDate, rentEndDate = @rentEndDate, rentRealEndDate = @rentRealEndDate, userID = @userID, carNumber = @carNumber WHERE rentNumber = @rentNumber;
SELECT * FROM CarForRents WHERE rentNumber = @rentNumber;


CREATE PROCEDURE [dbo].[UpdateCarType] (@thisCarType nvarchar(50), @carFirm nvarchar(30), @carModel nvarchar(30), @carDayPrice money, @carLatePrice money, @carYear int, @carGear nvarchar(30), @carTypeID int)
AS
UPDATE ALLCARTYPES SET thisCarType = @thisCarType, carFirm = @carFirm, carModel=@carModel, carDayPrice = @carDayPrice, carLatePrice = @carLatePrice, carYear = @carYear, carGear = @carGear WHERE carTypeID = @carTypeID;
SELECT * from ALLCARTYPES WHERE carTypeID = @carTypeID;;


CREATE PROCEDURE [dbo].[UpdateMessage] (@messageID int, @userID nvarchar(9), @userFirstName nvarchar(50), @userLastName nvarchar(50), @userEmail nvarchar(50), @userMessage nvarchar(MAX))
AS
UPDATE MESSAGES SET userID = @userID, userFirstName = @userFirstName, userLastName = @userLastName, userEmail = @userEmail, userMessage = @userMessage where messageID=@messageID;
SELECT * from MESSAGES where messageID = @messageID;


CREATE PROCEDURE [dbo].[UpdateRole] (@userLevel int, @userRole nvarchar(15))
AS
UPDATE Roles SET userLevel = @userLevel, userRole = @userRole WHERE userLevel = @userLevel;
SELECT * from Roles WHERE userLevel = @userLevel;


CREATE PROCEDURE [dbo].[UpdateUser] (@userID nvarchar(9), @userFirstName nvarchar(20), @userLastName nvarchar(20), @userNickName nvarchar(20), @userBirthDate datetime, @userGender nvarchar(10), @userEmail nvarchar(30), @userPassword nvarchar(50), @userPicture nvarchar(50), @userLevel int)
AS
UPDATE Users SET userID = @userID, userFirstName = @userFirstName, userLastName = @userLastName, userNickName = @userNickName, userBirthDate = @userBirthDate, userGender = @userGender, userEmail = @userEmail, userPassword = @userPassword, userPicture = @userPicture, userLevel = @userLevel where userID=@userID;
SELECT * from Users WHERE userID = @userID;


CREATE PROCEDURE [dbo].[UploadUserImage] (@userID nvarchar(9), @userPicture nvarchar(50))
AS
UPDATE Users SET userPicture = @userPicture where userID=@userID;
SELECT * FROM Users where userID=@userID;




INSERT INTO RentCar.ROLES (userLevel, userRole) VALUES (0, 'Guest');
INSERT INTO RentCar.ROLES (userLevel, userRole) VALUES (1, 'User');
INSERT INTO RentCar.ROLES (userLevel, userRole) VALUES (2, 'Manager');
INSERT INTO RentCar.ROLES (userLevel, userRole) VALUES (4, 'Admin');


INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Reshon-Letzion, Reshonim, Rozhensky 10', 'Reshonim', '31.9867863', '34.7707802');
INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Jerusalem, Aisee, Shuk Mahane-Ehuda', 'Aisee', '31.7841818', '35.2120812');
INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Holon, Beit HaRekev 6, Nativ Ha Asara 23', 'Beit HaRekev 6', '31.9722855', '34.77903');
INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Tel Aviv, Aliyat Ha Noar, Nahelet Izhak 54', 'Aliyat Ha Noar', '32.0756144', '34.8079408');
INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Tel Aviv, Mizpe Azrieli, Menahem Begin 132', 'Mizpe Azrieli', '32.0743942', '34.794358');
INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Ashkelon, AutoFix, HaPninim 19', 'AutoFix', '31.664042', '34.6018696');
INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Beer Sheva, Big Center, Hevron st 21', 'Big Center', '31.2438616', '34.8119657');
INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Tel Aviv, Sharona Center, Nitham Sharona', 'Sharona Center', '32.0724094', '34.7953738');
INSERT INTO RentCar.Branches(branchAddress, branchName, branchLat, branchLng) VALUES ('Beer Sheva, Ofer Geand Kanion, David Toviahu 125', 'Ofer Geand Kanion', '31.2503705', '34.7717336');


INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Mazda 6 Sedan', 'Mazda', 'M 6 Sedan', '1300.00', '263.00', '2016', 'manual');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Jaguar XF 2 Sedan', 'Jaguar', 'XF 2 Sedan', '2400.89', '400.00', '2012', 'automatic');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Skoda Rapid Sedan', 'Skoda', 'Rapid Sedan', '1583.23', '250.00', '2018', 'hybrid');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Renault Celio Universal', 'Renault', 'Celio Universal', '1073.31', '252.00', '2014', 'automatic');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Chevrolet Spark Universal', 'Chevrolet', 'Spark Universal', '714.37', '28.23', '2016', 'manual');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Subaru Impreza Universal', 'Subaru', 'Impreza Universal', '1000.23', '123.00', '2014', 'hybrid');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Jeep Cherokee Universal', 'Jeep', 'Cherokee Universal', '1804.28', '200.00', '2016', 'manual');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Kia Sportage Universal', 'Kia', 'Sportage Universal', '895.36', '125.00', '2011', 'automatic');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Seat Ibiza Kombi', 'Seat', 'Ibiza Kombi', '1583.23', '254.66', '2016', 'hybrid');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Peugeot NEW 308 Mini Van', 'Peugeot', 'NEW 308 Mini Van', '1000.23', '123.00', '2017', 'automatic');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Fiat 500 Mini Van', 'Fiat', '500 Mini Van', '751.57', '122.00', '2013', 'manual');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Hyundai I30 Universal', 'Hyundai', 'I30 Universal', '891.20', '25.00', '2015', 'hybrid');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Chevrolet Impala Sedan', 'Chevrolet', 'Impala Sedan', '1400.00', '230.00', '2016', 'automatic');

INSERT INTO RentCar.ALLCARTYPES(thisCarType, carFirm, carModel, carDayPrice, carLatePrice, carYear, carGear)
VALUES ('Mazda Cx3 Universal', 'Mazda', 'Cx3 Universal', '891.24', '50.41', '2018', 'manual');




INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '4a730b5f-9299-488e-b454-6867625a7c6a.png', TRUE, TRUE, '20587465', 6, 6);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '3c33a352-9a65-4cc3-a237-c90946fda446.png', TRUE, TRUE, '25825847', 8, 4);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '0d87e188-305e-4467-9d59-4287a9c51766.png', TRUE, TRUE, '2457814', 7, 5);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '0e773d44-d4c1-44dc-a7ef-1c28c0bc6bbf.png', TRUE, TRUE, '1425785', 5, 12);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '3a56e46b-1645-4f25-b495-4d9ef857e5e9.png', TRUE, TRUE, '262531', 2, 11);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '1ff043b3-10eb-49eb-84ed-5c08d17759de.png', TRUE, TRUE, '25156485', 2, 7);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '0a55b13b-fdca-426f-b080-5b808189d469.png', TRUE, TRUE, '14785658', 9, 13);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '1bbf1ca1-611f-4425-b147-80c705e2e8e4.png', TRUE, TRUE, '845742', 5, 13);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '1dde854c-a43b-4963-9062-3f7c75982106.png', TRUE, TRUE, '85844487', 8, 13);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '2f9c06ea-5c1d-411f-a781-579fd4fed7cf.png', TRUE, TRUE, '208456', 2, 15);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '2eee73f6-54c7-4024-a3bf-d9aa4971b0b1.png', TRUE, TRUE, '35735748', 5, 15);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '2c5c9a7d-ce5c-43c0-9f53-8af035e89e30.png', TRUE, TRUE, '346758', 9, 8);
INSERT INTO RentCar.CARS(carKm, carPicture, carInShape, carAvaliable, carNumber, carBranchID, carTypeID)
VALUES ('0', '3e05e08b-de89-474f-a628-7f8dd1de25df.png', TRUE, TRUE, '9498756', 10, 10);



Insrt data to MongoDb.

mongo
use RentCars


db.createCollection('Roles')
db.createCollection('Branches')
db.createCollection('CarTypes')
db.createCollection('Cars')

db.Roles.insertMany([{'userLevel':0,'userRole':'Guest'},{'userLevel':1,'userRole':'User'},{'userLevel':2,'userRole':'Manager'},{'userLevel':3,'userRole':'Admin'}])


db.Branches.insertMany([{'branchAddress':'Reshon-Letzion, Reshonim, Rozhensky 10','branchName':'Reshonim','branchLat':31.9867863,'branchLng':34.7707802},{'branchAddress':'Jerusalem, Aisee, Shuk Mahane-Ehuda','branchName':'Aisee','branchLat':31.7841818,'branchLng':35.2120812},{'branchAddress':'Holon, Beit HaRekev 6, Nativ Ha Asara 23','branchName':'Beit HaRekev 6','branchLat':31.9722855,'branchLng':34.77903},{'branchAddress':'Tel Aviv, Aliyat Ha Noar, Nahelet Izhak 54','branchName':'Aliyat Ha Noar','branchLat':32.0756144,'branchLng':34.8079408},{'branchAddress':'Tel Aviv, Mizpe Azrieli, Menahem Begin 132','branchName':'Mizpe Azrieli','branchLat':32.0743942,'branchLng':34.794358},{'branchAddress':'Ashkelon, AutoFix, HaPninim 19','branchName':'AutoFix','branchLat':31.664042,'branchLng':34.6018696},{'branchAddress':'Beer Sheva, Big Center, Hevron st 21','branchName':'Big Center','branchLat':31.2438616,'branchLng':34.8119657},{'branchAddress':'Tel Aviv, Sharona Center, Nitham Sharona','branchName':'Sharona Center','branchLat':32.0724094,'branchLng':34.7953738},{'branchAddress':'Beer Sheva, Ofer Geand Kanion, David Toviahu 125','branchName':'Ofer Geand Kanion','branchLat':31.2503705,'branchLng':34.7717336}])


db.CarTypes.insertMany([{'thisCarType':'Mazda 6 Sedan','carFirm':'Mazda','carModel':'M 6 Sedan','carDayPrice':1300.0,'carLatePrice':263.0,'carYear':2016,'carGear':'manual'},{'thisCarType':'Jaguar XF 2 Sedan','carFirm':'Jaguar','carModel':'XF 2 Sedan','carDayPrice':2400.89,'carLatePrice':400.0,'carYear':2012,'carGear':'automatic'},{'thisCarType':'Skoda Rapid Sedan','carFirm':'Skoda','carModel':'Rapid Sedan','carDayPrice':1583.23,'carLatePrice':250.0,'carYear':2018,'carGear':'hybrid'},{'thisCarType':'Renault Celio Universal','carFirm':'Renault','carModel':'Celio Universal','carDayPrice':1073.31,'carLatePrice':252.0,'carYear':2014,'carGear':'automatic'},{'thisCarType':'Chevrolet Spark Universal','carFirm':'Chevrolet','carModel':'Spark Universal','carDayPrice':714.37,'carLatePrice':28.23,'carYear':2016,'carGear':'manual'},{'thisCarType':'Subaru Impreza Universal','carFirm':'Subaru','carModel':'Impreza Universal','carDayPrice':1000.23,'carLatePrice':123.0,'carYear':2014,'carGear':'hybrid'},{'thisCarType':'Jeep Cherokee Universal','carFirm':'Jeep','carModel':'Cherokee Universal','carDayPrice':1804.28,'carLatePrice':200.0,'carYear':2016,'carGear':'manual'},{'thisCarType':'Kia Sportage Universal','carFirm':'Kia','carModel':'Sportage Universal','carDayPrice':895.36,'carLatePrice':125.0,'carYear':2011,'carGear':'automatic'},{'thisCarType':'Seat Ibiza Kombi','carFirm':'Seat','carModel':'Ibiza Kombi','carDayPrice':1583.23,'carLatePrice':254.66,'carYear':2016,'carGear':'hybrid'},{'thisCarType':'Peugeot NEW 308 Mini Van','carFirm':'Peugeot','carModel':'NEW 308 Mini Van','carDayPrice':1000.23,'carLatePrice':123.0,'carYear':2017,'carGear':'automatic'},{'thisCarType':'Fiat 500 Mini Van','carFirm':'Fiat','carModel':'500 Mini Van','carDayPrice':751.57,'carLatePrice':122.0,'carYear':2013,'carGear':'manual'},{'thisCarType':'Hyundai I30 Universal','carFirm':'Hyundai','carModel':'I30 Universal','carDayPrice':891.2,'carLatePrice':25.0,'carYear':2015,'carGear':'hybrid'},{'thisCarType':'Chevrolet Impala Sedan','carFirm':'Chevrolet','carModel':'Impala Sedan','carDayPrice':1400.0,'carLatePrice':230.0,'carYear':2016,'carGear':'automatic'},{'thisCarType':'Mazda Cx3 Universal','carFirm':'Mazda','carModel':'Cx3 Universal','carDayPrice':891.24,'carLatePrice':50.41,'carYear':2018,'carGear':'manual'}])


db.Cars.insertMany([{'carNumber':'20587465','carTypeID':6,'carKm':0.0,'carPicture':'4a730b5f-9299-488e-b454-6867625a7c6a.png','carInShape':true,'carAvaliable':true,'carBranchID':6},{'carNumber':'25825847','carTypeID':4,'carKm':0.0,'carPicture':'3c33a352-9a65-4cc3-a237-c90946fda446.png','carInShape':true,'carAvaliable':true,'carBranchID':8},{'carNumber':'2457814','carTypeID':5,'carKm':0.0,'carPicture':'0d87e188-305e-4467-9d59-4287a9c51766.png','carInShape':true,'carAvaliable':true,'carBranchID':7},{'carNumber':'1425785','carTypeID':12,'carKm':0.0,'carPicture':'0e773d44-d4c1-44dc-a7ef-1c28c0bc6bbf.png','carInShape':true,'carAvaliable':true,'carBranchID':5},{'carNumber':'262531','carTypeID':11,'carKm':0.0,'carPicture':'3a56e46b-1645-4f25-b495-4d9ef857e5e9.png','carInShape':true,'carAvaliable':true,'carBranchID':2},{'carNumber':'25156485','carTypeID':7,'carKm':0.0,'carPicture':'1ff043b3-10eb-49eb-84ed-5c08d17759de.png','carInShape':true,'carAvaliable':true,'carBranchID':2},{'carNumber':'14785658','carTypeID':13,'carKm':0.0,'carPicture':'0a55b13b-fdca-426f-b080-5b808189d469.png','carInShape':true,'carAvaliable':true,'carBranchID':9},{'carNumber':'845742','carTypeID':13,'carKm':0.0,'carPicture':'1bbf1ca1-611f-4425-b147-80c705e2e8e4.png','carInShape':true,'carAvaliable':true,'carBranchID':5},{'carNumber':'85844487','carTypeID':13,'carKm':0.0,'carPicture':'1dde854c-a43b-4963-9062-3f7c75982106.png','carInShape':true,'carAvaliable':true,'carBranchID':8},{'carNumber':'208456','carTypeID':15,'carKm':0.0,'carPicture':'2f9c06ea-5c1d-411f-a781-579fd4fed7cf.png','carInShape':true,'carAvaliable':true,'carBranchID':2},{'carNumber':'35735748','carTypeID':15,'carKm':0.0,'carPicture':'2eee73f6-54c7-4024-a3bf-d9aa4971b0b1.png','carInShape':true,'carAvaliable':true,'carBranchID':5},{'carNumber':'346758','carTypeID':8,'carKm':0.0,'carPicture':'2c5c9a7d-ce5c-43c0-9f53-8af035e89e30.png','carInShape':true,'carAvaliable':true,'carBranchID':9},{'carNumber':'9498756','carTypeID':10,'carKm':0.0,'carPicture':'3e05e08b-de89-474f-a628-7f8dd1de25df.png','carInShape':true,'carAvaliable':true,'carBranchID':10}])