﻿using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationRequest
{
    public decimal Amount { get; set; }
    public int DonorId { get; set; }
    public Person? Donor { get; set; }
}