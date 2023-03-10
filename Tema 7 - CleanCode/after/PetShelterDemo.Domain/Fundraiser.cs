using System;
namespace PetShelterDemo.Domain
{
	public class Fundraiser: INamedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DonationTargetInRons { get; set; }
        public List<Donation> Donations = new List<Donation>();
        public List<Person> Persons = new List<Person>();

        public Fundraiser(string name, string description, int donationTargetInRons)
        {
            Name = name;
            Description = description;
            DonationTargetInRons = donationTargetInRons;
        }
       
	}
}

