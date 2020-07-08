using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RentCarsServerCore
{
	[Route("api")]
	[ApiController]
    public class SearchApiController : ControllerBase
    {
		private ISearchRepository searchRepository;
		private IFullCarDataRepository fullDataRepository;
		private IPriceRepository priceRepository;

		public SearchApiController(ISearchRepository _searchRepository, IFullCarDataRepository _fullDataRepository, IPriceRepository _priceRepository)
		{
			searchRepository = _searchRepository;
			fullDataRepository = _fullDataRepository;
			priceRepository = _priceRepository;
		}

		[HttpPost("search")]
		public IActionResult GetAllCarsBySearch(SearchModel searchModel)
		{
			var page = Request.Headers["page"];
			var carsNum = Request.Headers["carsNum"];

			try
			{
				SearchReturnModel searchReturnModel = searchRepository.GetAllCarsBySearch(searchModel, int.Parse(page), int.Parse(carsNum));
				return Ok(searchReturnModel);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpPost("search/{carnumber}")]
		public IActionResult IsAvaliableByNumber(string carnumber, SearchModel searchModel)
		{
			try
			{
				bool isAvaliable = priceRepository.CheckIfCarAvaliable(carnumber, (DateTime)searchModel.fromDate, (DateTime)searchModel.toDate);
				return Ok(isAvaliable);

			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[HttpGet("fullCarData")]
		public IActionResult GetAllCarsAllData()
		{
			var page = Request.Headers["page"];
			var carsNum = Request.Headers["carsNum"];

			try
			{
				SearchReturnModel searchReturnModel = searchRepository.GetAllCarsBySearch(new SearchModel(), int.Parse(page), int.Parse(carsNum));
				return Ok(searchReturnModel);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("fullCarData/{number}")]
		public IActionResult GetCarAllData(string number)
		{
			try
			{
				FullCarDataModel oneFullCarDataModel = fullDataRepository.GetCarAllData(number);
				return Ok(oneFullCarDataModel);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}
	}
}