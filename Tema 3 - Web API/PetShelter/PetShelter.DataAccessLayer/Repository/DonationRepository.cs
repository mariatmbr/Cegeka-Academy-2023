using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class DonationRepository : BaseRepository<Donation>, IDonationRepository
{
    public DonationRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<ICollection<Donation>> GetAllDonorsForSpecificFundraiser(int fundraiserId)
    {
        return await _context.Donations.Where(d => d.FundraiserId == fundraiserId).ToListAsync();
    }
}