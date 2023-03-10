using System;
namespace PetShelterDemo.Domain
{
	public class Donation
	{
		public string Currency { get; set; }
		public int Amount { get; set; }
		
		public Donation(string currency, int amount)
		{
			Currency = currency;
			Amount = amount;
		}
	}
}

