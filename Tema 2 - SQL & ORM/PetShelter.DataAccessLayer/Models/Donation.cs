using System;
namespace PetShelter.DataAccessLayer.Models
{
	public class Donation
	{
		public int Id { get; set; }

		public decimal Amount { get; set; }

		public int DonorId { get; set; }

		public Person? Donor { get; set; }
	}
}

