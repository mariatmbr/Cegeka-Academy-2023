using System.Linq;
using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private readonly IRegistry<Fundraiser> fundraiserRegistry;
    private int donationsInRon = 0;

    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
        fundraiserRegistry = new Registry<Fundraiser>(new Database());
    }

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
        fundraiserRegistry.Register(fundraiser);
    }

    public IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundraiserRegistry.GetAll().Result;
    }

    public Fundraiser GetFundraiserByName(string name)
    {
        return fundraiserRegistry.GetByName(name).Result;
    }

    public void DonateToFundraiser(string fundraiserName, Person donor, string currency, int amount)
    {
        Fundraiser f = GetFundraiserByName(fundraiserName);

        f.Persons.Add(donor);

        var myDonation = f.TotalDonations.FirstOrDefault(elem => elem.Currency.ToLower() == currency.ToLower());
        if (myDonation != null)
        {
            myDonation.Amount += amount;
        } else
        {
            f.TotalDonations.Add(new Donation(currency.ToUpper(), amount));
        }
    }

    public void RegisterPet(Pet pet)
    {
        petRegistry.Register(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result;
    }

    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public void Donate(Person donor, int amountInRON)
    {
        donorRegistry.Register(donor);
        donationsInRon += amountInRON;
    }

    public int GetTotalDonationsInRON()
    {
        return donationsInRon;
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }
}