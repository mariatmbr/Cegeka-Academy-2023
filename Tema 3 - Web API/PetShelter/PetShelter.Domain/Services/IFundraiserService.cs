using System;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.Domain.Services
{
	public interface IFundraiserService
	{
        Task DeleteFundraiserAsync(int fundraiserId);

        Task<IReadOnlyCollection<Fundraiser>> GetAllFundraisers();

        Task<Fundraiser> GetFundraiserAsync(int fundraiserId);

        Task<int> CreateFundraiser(Person owner, Fundraiser fundraiser);

        Task DonateToFundraiser(int fundraiserId, Donation donation);
    }
}

