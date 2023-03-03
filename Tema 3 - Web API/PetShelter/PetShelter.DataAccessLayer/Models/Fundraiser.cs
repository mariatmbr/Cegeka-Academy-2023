using System;
using Microsoft.VisualBasic;

namespace PetShelter.DataAccessLayer.Models
{
    public class Fundraiser : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal CurrentlyDonationAmount { get; set; }

        public decimal GoalValue { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DueDate { get; set; }

        public ICollection<Donation> Donations { get; set; }

        public Person Owner { get; set; }

        public string Status { get; set; }

        public int OwnerId { get; set; }

        public Fundraiser(int id, string name, decimal goalValue, DateTime dueDate)
        {
            Name = name;
            GoalValue = goalValue;
            CurrentlyDonationAmount = 0;
            CreationDate = DateTime.Now;
            DueDate = dueDate;
            Donations = new List<Donation>();
            Status = "Active";
            Owner = new Person();
        }
    }
}

