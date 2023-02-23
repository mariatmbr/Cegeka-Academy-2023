using System;
using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repositories
{
	public class FundraiserRepository
    {
        private readonly PetShelterContext _context;

        public FundraiserRepository(PetShelterContext context)
        {
            _context = context;
        }

        public async Task AddFundraiser(Fundraiser fundraiser)
        {
            await _context.Fundraisers.AddAsync(fundraiser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Fundraiser>> GetFundraisersWithDonations()
        {
            return await _context.Fundraisers.Where(x => x.DonationTotal > 10).ToListAsync();
        }

        public async Task<int> GetRaisedAmount()
        {
            int sum = 0;
            foreach (Fundraiser f in _context.Fundraisers)
            {
                sum = sum + f.DonationTotal;

            }
            return sum;
        }
    }
}

