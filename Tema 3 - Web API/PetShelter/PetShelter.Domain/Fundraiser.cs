﻿
using PetShelter.DataAccessLayer.Models;
using PetShelter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain;

public class Fundraiser : INamedEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal GoalValue { get; set; }
    public decimal CurrentDonationAmount { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; }
    public Person Owner { get; set; }
    public ICollection<Donation> Donations { get; set; }
}
