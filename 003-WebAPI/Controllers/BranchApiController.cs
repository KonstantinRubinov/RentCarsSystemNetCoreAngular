using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RentCarsServerCore
{
	[Route("api")]
	[ApiController]
    public class BranchApiController : ControllerBase
    {
		private IBranchRepository branchRepository;

		public BranchApiController(IBranchRepository _branchRepository)
		{
			branchRepository = _branchRepository;
		}

		[HttpGet("branches/NameId")]
		public IActionResult GetAllBranchesNamesIds()
		{
			try
			{
				List<BranchModel> allBranchesNamesIds = branchRepository.GetAllBranchesNamesIds();
				return Ok(allBranchesNamesIds);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("branches")]
		public IActionResult GetAllBranches()
		{
			try
			{
				List<BranchModel> allBranches = branchRepository.GetAllBranches();
				return Ok(allBranches);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpGet("branches/{id}", Name = "GetOneBranch")]
		public IActionResult GetOneBranch(int id)
		{
			try
			{
				BranchModel oneBranch = branchRepository.GetOneBranch(id);
				return Ok(oneBranch);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[HttpPost("branches")]
		public IActionResult AddBranch(BranchModel branchModel)
		{
			try
			{
				if (branchModel == null)
				{
					return BadRequest("Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				BranchModel addedBranch = branchRepository.AddBranch(branchModel);
				return StatusCode(StatusCodes.Status201Created, addedBranch);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}


		[HttpPut("branches/{id}")]
		public IActionResult UpdateBranch(int id, BranchModel branchModel)
		{
			try
			{
				if (branchModel == null)
				{
					return BadRequest("Data is null.");
				}

				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return BadRequest(errors);
				}

				branchModel.branchID = id;
				BranchModel updatedBranch = branchRepository.UpdateBranch(branchModel);
				return Ok(updatedBranch);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return StatusCode(StatusCodes.Status500InternalServerError, errors);
			}
		}

		[HttpDelete("branches/{id}")]
		public IActionResult DeleteBranch(int id)
		{
			try
			{
				int i = branchRepository.DeleteBranch(id);
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