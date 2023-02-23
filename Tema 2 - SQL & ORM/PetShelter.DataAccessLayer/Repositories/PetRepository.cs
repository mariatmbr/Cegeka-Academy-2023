using System;
using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repositories;

public class PetRepository
{
	private readonly PetShelterContext _context;

	public PetRepository(PetShelterContext context)
	{
		_context = context;
	}

	public async Task AddPet(Pet pet)
	{
		await _context.Pets.AddAsync(pet);
		await _context.SaveChangesAsync();
	}

    public async Task<List<Pet>> GetHealthyPets()
	{
		return await _context.Pets.Where(x => x.IsHealthy).ToListAsync();
	}

}


