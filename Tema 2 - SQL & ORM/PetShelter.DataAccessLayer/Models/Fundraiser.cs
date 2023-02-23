using System;
namespace PetShelter.DataAccessLayer.Models
{
	public class Fundraiser
	{
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int DonationTargetInRons { get; set; }

        public int DonationTotal { get; set; }

        public ICollection<Person>? Donors;

        public ICollection<Donation>? Donations;

    }
}

