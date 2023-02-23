using System;
using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repositories
{
    public interface IPetRepository
    {
        Task AddPet(Pet pet);
        Task<List<Pet>> GetHealthyPets();
    }
}

