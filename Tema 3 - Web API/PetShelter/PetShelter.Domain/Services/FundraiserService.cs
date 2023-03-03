using System;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Extensions.DomainModel;
using PetShelter.Domain.Extensions.DataAccess;

namespace PetShelter.Domain.Services
{
    public class FundraiserService : IFundraiserService
    {
        private readonly IFundraiserRepository _fundraiserRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IPersonRepository _personRepository;

        public FundraiserService(IFundraiserRepository fundraiserRepository, IDonationRepository donationRepository, IPersonRepository personRepository)
        {
            _fundraiserRepository = fundraiserRepository;
            _donationRepository = donationRepository;
            _personRepository = personRepository;
        }

        public async Task<int> CreateFundraiser(Person owner, Fundraiser fundraiser)
        {
            var person = await _personRepository.GetOrAddPersonAsync(owner.FromDomainModel());
            var fundraiserAdded = new DataAccessLayer.Models.Fundraiser(fundraiser.Id, fundraiser.Name, fundraiser.GoalValue, fundraiser.DueDate)
            {
                Owner = person
            };
            await _fundraiserRepository.Add(fundraiserAdded);
            return fundraiserAdded.Id;
        }


        public async Task DeleteFundraiserAsync(int fundraiserId)
        {
            var fundraiser = await _fundraiserRepository.GetById(fundraiserId);

            await _fundraiserRepository.Delete(fundraiser);
        }

        public async Task<IReadOnlyCollection<Fundraiser>> GetAllFundraisers()
        {
            var fundraisers = await _fundraiserRepository.GetAll();

            return fundraisers.Select(x => x.toDomainModel()).ToArray();
        }

        public async Task<Fundraiser> GetFundraiserAsync(int fundraiserId)
        {
            var fundraiser = await _fundraiserRepository.GetById(fundraiserId);

            if (fundraiser == null)
            {
                return null;
            }

            fundraiser.Owner = await _personRepository.GetById(fundraiser.OwnerId);
            fundraiser.Donations = await _donationRepository.GetAllDonorsForSpecificFundraiser(fundraiserId);
            return fundraiser.toDomainModel();
        }

        public async Task DonateToFundraiser(int fundraiserId, Donation donation)
        {
            var person = await _personRepository.GetOrAddPersonAsync(donation.Donor.FromDomainModel());
            donation.DonorId = person.Id;
            var fundraiser = await _fundraiserRepository.GetById(fundraiserId);
            fundraiser.CurrentlyDonationAmount += donation.Amount;
            if (fundraiser.CurrentlyDonationAmount > fundraiser.GoalValue)
            {
                fundraiser.Status = "Closed";
            }
            await _fundraiserRepository.Update(fundraiser);
            await _donationRepository.Add(new DataAccessLayer.Models.Donation(donation.Amount, donation.DonorId, fundraiserId));
        }
    }
}

