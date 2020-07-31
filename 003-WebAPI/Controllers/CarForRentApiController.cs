using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RentCarsServerCore
{
	[Route("api")]
	[ApiController]
    public class CarForRentApiController : ControllerBase
    {
		private ICarForRentRepository carForRentRepository;
		private IPriceRepository priceRepository;

		public CarForRentApiController(ICarForRentRepository _carForRentRepository, IPriceRepository _priceRepository)
		{
			carForRentRepository = _carForRentRepository;
			priceRepository = _priceRepository;
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpGet("CarsForRent")]
		public IActionResult GetAllCarsForRent()
		{
			try
			{
				List<CarForRentModel> allCarsForRent = carForRentRepository.GetAllCarsForRent();
				return Ok(allCarsForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[HttpGet("CarsForRent/{car}", Name = "GetRentByCar")]
		public IActionResult GetRentByCar(string car)
		{
			try
			{
				List<CarForRentModel> allCarsForRent = carForRentRepository.GetCarsForRentByCarNumber(car);
				return Ok(allCarsForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[Authorize]
		[HttpGet("CarsForRent/{userid}", Name = "GetCarsForRentByUserId")]
		public IActionResult GetCarsForRentByUserId(string userid)
		{
			try
			{
				List<FullCarDataModel> allCarsForRent = carForRentRepository.GetCarsForRentByUserId(userid);
				return Ok(allCarsForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[HttpPost("CarsForRent")]
		public IActionResult AddCarForRent(CarForRentModel carForRentModel)
		{
			try
			{
				if (carForRentModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}
				
				string id = User.Identity.Name;
				carForRentModel.userID = id;
				CarForRentModel addedCarForRent = carForRentRepository.AddCarForRent(carForRentModel);
				return StatusCode(StatusCodes.Status201Created, addedCarForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Manager)]
		[HttpPut("CarsForRent/{number}")]
		public IActionResult UpdateForRent(int number, CarForRentModel carForRentModel)
		{
			try
			{
				if (carForRentModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}
				
				carForRentModel.rentNumber = number;
				CarForRentModel updatedCarForRent = carForRentRepository.UpdateCarForRent(carForRentModel);
				return Ok(updatedCarForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpDelete("CarsForRent/'{number}'")]
		public IActionResult DeleteForRent(string number)
		{
			try
			{
				int i = carForRentRepository.DeleteCarForRent(number);
				return NoContent();
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpDelete("CarsForRent/{number}")]
		public IActionResult DeleteForRent(int number)
		{
			try
			{
				int i = carForRentRepository.DeleteCarForRent(number);
				return NoContent();
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[HttpPost("getSumPrice")]
		public IActionResult GetSumPrice(OrderPriceModel carForPrice)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				OrderPriceModel carSumPrice = priceRepository.priceForOrderIfAvaliable(carForPrice);
				return StatusCode(StatusCodes.Status201Created, carSumPrice);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);

				if (ex is DateNotAvaliableException)
				{
					return StatusCode(StatusCodes.Status403Forbidden, errors);
				}

				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}