using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RentCarsServerCore
{
	[Route("api")]
	[ApiController]
    public class CarTypeApiController : ControllerBase
    {
		private ICarTypeRepository carTypeRepository;

		public CarTypeApiController(ICarTypeRepository _carTypeRepository)
		{
			carTypeRepository = _carTypeRepository;
		}

		[HttpGet("CarTypes")]
		public IActionResult GetAllCarTypes()
		{
			try
			{
				List<CarTypeModel> allCarTypes = carTypeRepository.GetAllCarTypes();
				return Ok(allCarTypes);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("CarTypes/{id}", Name = "GetOneCarType")]
		public IActionResult GetOneCarType(int id)
		{
			try
			{
				CarTypeModel oneCarType = carTypeRepository.GetOneCarType(id);
				return Ok(oneCarType);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpPost("CarTypes")]
		public IActionResult AddCarType(CarTypeModel carTypeModel)
		{
			try
			{
				if (carTypeModel == null)
				{
					return BadRequest("Data is null.");
				}

				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				CarTypeModel addedCarType = carTypeRepository.AddCarType(carTypeModel);
				return StatusCode(StatusCodes.Status201Created, addedCarType);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpPut("CarTypes/{type}")]
		public IActionResult UpdateCarType(int id, CarTypeModel carTypeModel)
		{
			try
			{
				if (carTypeModel == null)
				{
					return BadRequest("Data is null.");
				}

				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				carTypeModel.carTypeId = id;
				CarTypeModel updatedCarType = carTypeRepository.UpdateCarType(carTypeModel);
				return Ok(updatedCarType);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpDelete("CarTypes/{type}")]
		public IActionResult DeleteCarType(string type)
		{
			try
			{
				int i = carTypeRepository.DeleteCarType(type);
				if (i > 0)
				{
					return NoContent();
				}
				return StatusCode(StatusCodes.Status500InternalServerError);

			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[Authorize]
		[Authorize(Roles = RoleModel.Admin)]
		[HttpDelete("CarTypes/{id}")]
		public IActionResult DeleteCarType(int id)
		{
			try
			{
				int i = carTypeRepository.DeleteCarType(id);
				if (i > 0)
				{
					return NoContent();
				}
				return StatusCode(StatusCodes.Status500InternalServerError);

			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}
	}
}