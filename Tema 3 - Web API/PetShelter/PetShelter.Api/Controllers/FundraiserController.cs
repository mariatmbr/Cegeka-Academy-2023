﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.Resources;
using PetShelter.Api.Resources.Extensions;
using PetShelter.Domain;
using PetShelter.Domain.Services;

namespace PetShelter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundraiserController : ControllerBase
    {
        private readonly IFundraiserService _fundraiserService;

        public FundraiserController(IFundraiserService fundraiserService)
        {
            _fundraiserService = fundraiserService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<Resources.Fundraiser>>> GetFundraisers()
        {
            var data = await this._fundraiserService.GetAllFundraisers();
            return this.Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("", Name = nameof(AddFundraiser))]
        public async Task<IActionResult> AddFundraiser([FromBody] AddedFundraiser fundraiser)
        {
            var id = await _fundraiserService.CreateFundraiser(fundraiser.Owner.AsDomainModel(), fundraiser.AsDomainModel());
            return CreatedAtRoute(nameof(AddFundraiser), id);
        }

        [HttpPost("{id}/donate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> DonateToFundraiser(int id, [FromBody] Donation donation)
        {
            await _fundraiserService.DonateToFundraiser(id, donation);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Resources.Fundraiser>> GetFundraiser(int id)
        {
            var fundraiser = await this._fundraiserService.GetFundraiserAsync(id);
            if (fundraiser is null)
            {
                return this.NotFound();
            }
            return this.Ok(new Resources.Fundraiser
            {
                Id = fundraiser.Id,
                Name = fundraiser.Name,
                DueDate = fundraiser.DueDate,
                CreationDate = fundraiser.CreationDate,
                CurrentDonationAmount = fundraiser.CurrentDonationAmount,
                GoalValue = fundraiser.GoalValue,
                Owner = fundraiser.Owner.AsResource(),
                Status = fundraiser.Status,
                Donors = fundraiser.Donations.Select(p => p.Donor.AsResource()).ToList(),
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFundraiser(int id)
        {
            var fundraiser = await this._fundraiserService.GetFundraiserAsync(id);
            if (fundraiser is null)
            {
                return this.NotFound();
            }
            await this._fundraiserService.DeleteFundraiserAsync(id);
            return this.NoContent();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Options()
        {
            this.Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            return this.Ok();
        }


    }
}
