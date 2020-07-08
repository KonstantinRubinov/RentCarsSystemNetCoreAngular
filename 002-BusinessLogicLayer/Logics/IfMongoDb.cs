using System.Collections.Generic;

namespace RentCarsServerCore
{
	public class IfMongoDb
	{
		private List<BranchModel> br = new List<BranchModel>();
		private List<CarTypeModel> ct = new List<CarTypeModel>();
		private List<CarModel> cars = new List<CarModel>();
		private IBranchRepository branchRepository = new MongoBranchManager();
		private ICarTypeRepository carTypeRepository = new MongoCarTypeManager();
		private ICarsRepository carsRepository = new MongoCarsManager();

		public void AddMongoData()
		{
			IRoleRepository roleRepository = new MongoRoleManager();
			roleRepository.AddRole(new RoleModel(0, "Guest"));
			roleRepository.AddRole(new RoleModel(1, "User"));
			roleRepository.AddRole(new RoleModel(2, "Manager"));
			roleRepository.AddRole(new RoleModel(3, "Admin"));

			bool t = true;
			bool f = false;

			BranchModel branchModel;
			CarTypeModel carTypeModel;
			CarModel carModel;

			branchModel = new BranchModel("Reshon-Letzion, Reshonim, Rozhensky 10", "Reshonim", 31.9867863, 34.7707802);
			carTypeModel = new CarTypeModel("Mazda 6 Sedan", "Mazda", "M 6 Sedan", (decimal)1300.00, (decimal)263.00, 2016, "manual");
			carModel = new CarModel(0, "4a730b5f-9299-488e-b454-6867625a7c6a.png", t, t, "20587465");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Jerusalem, Aisee, Shuk Mahane-Ehuda", "Aisee", 31.7841818, 35.2120812);
			carTypeModel = new CarTypeModel("Jaguar XF 2 Sedan", "Jaguar", "XF 2 Sedan", (decimal)2400.89, (decimal)400.00, 2012, "automatic");
			carModel = new CarModel(0, "3c33a352-9a65-4cc3-a237-c90946fda446.png", t, t, "25825847");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Holon, Beit HaRekev 6, Nativ Ha Asara 23", "Beit HaRekev 6", 31.9722855, 34.77903);
			carTypeModel = new CarTypeModel("Skoda Rapid Sedan", "Skoda", "Rapid Sedan", (decimal)1583.23, (decimal)250.00, 2018, "hybrid");
			carModel = new CarModel(0, "0d87e188-305e-4467-9d59-4287a9c51766.png", t, t, "2457814");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Tel Aviv, Aliyat Ha Noar, Nahelet Izhak 54", "Aliyat Ha Noar", 32.0756144, 34.8079408);
			carTypeModel = new CarTypeModel("Renault Celio Universal", "Renault", "Celio Universal", (decimal)1073.31, (decimal)252.00, 2014, "automatic");
			carModel = new CarModel(0, "0e773d44-d4c1-44dc-a7ef-1c28c0bc6bbf.png", t, t, "1425785");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Tel Aviv, Mizpe Azrieli, Menahem Begin 132", "Mizpe Azrieli", 32.0743942, 34.794358);
			carTypeModel = new CarTypeModel("Chevrolet Spark Universal", "Chevrolet", "Spark Universal", (decimal)714.37, (decimal)28.23, 2016, "manual");
			carModel = new CarModel(0, "3a56e46b-1645-4f25-b495-4d9ef857e5e9.png", t, t, "262531");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Ashkelon, AutoFix, HaPninim 19", "AutoFix", 31.664042, 34.6018696); carTypeModel = new CarTypeModel("Subaru Impreza Universal", "Subaru", "Impreza Universal", (decimal)1000.23, (decimal)123.00, 2014, "hybrid");
			carTypeModel = new CarTypeModel("Subaru Impreza Universal", "Subaru", "Impreza Universal", (decimal)1000.23, (decimal)123.00, 2014, "hybrid");
			carModel = new CarModel(0, "1ff043b3-10eb-49eb-84ed-5c08d17759de.png", t, t, "25156485");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Beer Sheva, Big Center, Hevron st 21", "Big Center", 31.2438616, 34.8119657);
			carTypeModel = new CarTypeModel("Jeep Cherokee Universal", "Jeep", "Cherokee Universal", (decimal)1804.28, (decimal)200.00, 2016, "manual");
			carModel = new CarModel(0, "0a55b13b-fdca-426f-b080-5b808189d469.png", t, t, "14785658");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Tel Aviv, Sharona Center, Nitham Sharona", "Sharona Center", 32.0724094, 34.7953738); carTypeModel = new CarTypeModel("Kia Sportage Universal", "Kia", "Sportage Universal", (decimal)895.36, (decimal)125.00, 2011, "automatic");
			carTypeModel = new CarTypeModel("Kia Sportage Universal", "Kia", "Sportage Universal", (decimal)895.36, (decimal)125.00, 2011, "automatic");
			carModel = new CarModel(0, "1bbf1ca1-611f-4425-b147-80c705e2e8e4.png", t, t, "845742");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Beer Sheva, Ofer Geand Kanion, David Toviahu 125", "Ofer Geand Kanion", 31.2503705, 34.7717336);
			carTypeModel = new CarTypeModel("Seat Ibiza Kombi", "Seat", "Ibiza Kombi", (decimal)1583.23, (decimal)254.66, 2016, "hybrid");
			carModel = new CarModel(0, "1dde854c-a43b-4963-9062-3f7c75982106.png", t, t, "85844487");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Reshon-Letzion, Reshonim, Rozhensky 10", "Reshonim", 31.9867863, 34.7707802);
			carTypeModel = new CarTypeModel("Peugeot NEW 308 Mini Van", "Peugeot", "NEW 308 Mini Van", (decimal)1000.23, (decimal)123.00, 2017, "automatic");
			carModel = new CarModel(0, "2f9c06ea-5c1d-411f-a781-579fd4fed7cf.png", t, t, "208456");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Jerusalem, Aisee, Shuk Mahane-Ehuda", "Aisee", 31.7841818, 35.2120812);
			carTypeModel = new CarTypeModel("Fiat 500 Mini Van", "Fiat", "500 Mini Van", (decimal)751.57, (decimal)122.00, 2013, "manual");
			carModel = new CarModel(0, "2eee73f6-54c7-4024-a3bf-d9aa4971b0b1.png", t, t, "35735748");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Holon, Beit HaRekev 6, Nativ Ha Asara 23", "Beit HaRekev 6", 31.9722855, 34.77903);
			carTypeModel = new CarTypeModel("Hyundai I30 Universal", "Hyundai", "I30 Universal", (decimal)891.20, (decimal)25.00, 2015, "hybrid");
			carModel = new CarModel(0, "2c5c9a7d-ce5c-43c0-9f53-8af035e89e30.png", t, t, "346758");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Tel Aviv, Aliyat Ha Noar, Nahelet Izhak 54", "Aliyat Ha Noar", 32.0756144, 34.8079408);
			carTypeModel = new CarTypeModel("Chevrolet Impala Sedan", "Chevrolet", "Impala Sedan", (decimal)1400.00, (decimal)230.00, 2016, "automatic");
			carModel = new CarModel(0, "3e05e08b-de89-474f-a628-7f8dd1de25df.png", t, t, "9498756");
			AddMongoData2(branchModel, carTypeModel, carModel);

			branchModel = new BranchModel("Tel Aviv, Mizpe Azrieli, Menahem Begin 132", "Mizpe Azrieli", 32.0743942, 34.794358);
			carTypeModel = new CarTypeModel("Mazda Cx3 Universal", "Mazda", "Cx3 Universal", (decimal)891.24, (decimal)50.41, 2018, "manual");
			carModel = new CarModel(0, "4a730b5f-9299-488e-b454-6867625a7c6a.png", t, t, "20587465");
			AddMongoData2(branchModel, carTypeModel, carModel);


			br = branchRepository.GetAllBranches();
			ct = carTypeRepository.GetAllCarTypes();
			cars = carsRepository.GetAllCars();

			for (int k = 0; k < cars.Count; k++)
			{
				for (int b = 0; b < br.Count; b++)
				{
					if (cars[k].carBranchIDMongo.Equals(br[b].branchIDMongo))
					{
						string x = cars[k].carBranchIDMongo;
					}

					if (cars[k].carTypeIDMongo.Equals(ct[b].carTypeIdMongo))
					{
						string y = cars[k].carTypeIDMongo;
					}
				}
			}
		}


		public void AddMongoData2(BranchModel branchModel, CarTypeModel carTypeModel, CarModel carModel)
		{
			branchModel = branchRepository.AddBranch(branchModel);
			carTypeModel = carTypeRepository.AddCarType(carTypeModel);
			carModel.carBranchIDMongo = branchModel.branchIDMongo;
			carModel.carTypeIDMongo = carTypeModel.carTypeIdMongo;
			carModel = carsRepository.AddCar(carModel);
		}
	}
}
