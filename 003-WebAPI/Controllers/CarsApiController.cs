using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace RentCarsServerCore
{
	[Route("api")]
	[ApiController]
    public class CarsApiController : ControllerBase
    {
		private ICarsRepository carsRepository;
		private readonly IHostingEnvironment environment;

		public CarsApiController(ICarsRepository _carsRepository, IHostingEnvironment _environment)
		{
			carsRepository = _carsRepository;
			environment = _environment;
		}

		[HttpGet("cars")]
		public IActionResult GetAllCars()
		{
			try
			{
				List<CarModel> allCars = carsRepository.GetAllCars();
				return Ok(allCars);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[HttpGet("cars/{number}", Name = "GetOneCar")]
		public IActionResult GetOneCar(string number)
		{
			try
			{
				CarModel oneCar = carsRepository.GetOneCar(number);
				return Ok(oneCar);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[HttpPost("cars")]
		public IActionResult AddCar(CarModel carModel)
		{
			try
			{
				if (carModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}
				
				CarModel addedCar = carsRepository.AddCar(carModel);
				return StatusCode(StatusCodes.Status201Created, addedCar);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpPut("cars/{number}")]
		public IActionResult UpdateCar(string number, CarModel carModel)
		{
			try
			{
				if (carModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}
				
				carModel.carNumber = number;
				CarModel updatedCar = carsRepository.UpdateCar(carModel);
				return Ok(updatedCar);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[HttpDelete("cars/{number}")]
		public IActionResult DeleteCar(string number)
		{
			try
			{
				string carImage = carsRepository.DeleteCar(number);
				carImage = carImage.Trim();
				CarPictureModel carsPictureModel = carsRepository.GetNumberOfCarWithImage(carImage);
				if (carsPictureModel.numberOfCars == 0)
				{
					string filePath = environment.WebRootPath + "/images/cars/" + carImage;
					var file = new FileInfo(filePath);
					if (file.Exists)
					{
						file.Delete();
					}
				}
				return NoContent();
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpPost("cars/file/{number}")]
		public IActionResult UploadCarImage(string number, string carPic = "")
		{
			try
			{
				CarModel updloadedCar;
				string fileName;

				if (carPic.Equals(""))
				{
					fileName = Guid.NewGuid() + ".png";
				}
				else
				{
					fileName = carPic;
				}
				byte[] bytes = Convert.FromBase64String(carPic);
				string filePath = environment.WebRootPath + "/images/cars/" + fileName;
				using (FileStream binaryFileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
				{
					binaryFileStream.Write(bytes, 0, bytes.Length);
					updloadedCar = carsRepository.UploadCarImage(number, fileName);
					updloadedCar.carPicture = "images/cars/" + fileName;
				}
				if (updloadedCar != null)
				{
					return Ok(updloadedCar);
				}
				return StatusCode(StatusCodes.Status417ExpectationFailed);

			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("carimages")]
		public IActionResult GetAllCarImagesAndNumberOfCars()
		{
			try
			{
				List<CarPictureModel> allCarImages = carsRepository.GetAllCarImagesAndNumberOfCars();
				return Ok(allCarImages);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}
	}
}