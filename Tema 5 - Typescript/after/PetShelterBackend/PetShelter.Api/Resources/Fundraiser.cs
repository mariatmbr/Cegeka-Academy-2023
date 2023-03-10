using PetShelter.Domain;

namespace PetShelter.Api.Resources
{
    public class Fundraiser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal GoalValue { get; set; }
        public decimal CurrentDonationAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public Person Owner { get; set; }
        public ICollection<Person> Donors { get; set; }
    }
}
