using System.Drawing;
using FluentValidation;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class DonationService
{
    private readonly IDonationRepository _donationRepository;
    private readonly IPersonService _personService;
    private readonly IValidator<AddDonationRequest> _donationValidator;

    public DonationService(IDonationRepository donationRepository, IPersonService personService, IValidator<AddDonationRequest> validator)
    {
        _donationValidator = validator;
        _personService = personService;
        _donationRepository = donationRepository;
    }

    public async Task AddDonation(AddDonationRequest addDonationRequest)
    {
        var validationResult = _donationValidator.Validate(addDonationRequest);
        if(!validationResult.IsValid) { throw new ArgumentException(); }

        await _donationRepository.Add(new DataAccessLayer.Models.Donation
        {
            Amount = addDonationRequest.Amount,
            Donor = await _personService.GetPerson(addDonationRequest.Donor),
            DonorId = addDonationRequest.DonorId,
        });
    }

    public async Task<Donation> GetDonation(int id)
    {
        return await _donationRepository.GetById(id);
    }

    public async Task<IReadOnlyCollection<Donation>> GetDonations()
    {
        return await _donationRepository.GetAll();
    }
}