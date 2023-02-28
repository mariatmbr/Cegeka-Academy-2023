using FluentValidation;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer.Validators;

public class PersonValidator: AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.IdNumber).Length(PersonConstants.IdNumberLength);
        RuleFor(x => x.Name).NotEmpty().MinimumLength(PersonConstants.NameMinLength);
        RuleFor(x => x.DateOfBirth).NotEmpty()
            .Must(dateOfBirth => IsMoreThan18YearsOld(dateOfBirth ?? DateTime.Now, DateTime.Now));
    }

    public bool IsMoreThan18YearsOld(DateTime birthDate, DateTime now)
    {
        int age = now.Year - birthDate.Year;

        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            age--;
        if (age >= 18)
            return true;
        else
            return false;
    }
}