using PetShelter.Domain;
using PetShelter.Domain.Services;

namespace PetShelter.Api.Resources.Extensions;

public static class FundraiserExtension
{
    public static Domain.Fundraiser AsDomainModel(this AddedFundraiser fundraiser)
    {

        var domainModel = new Domain.Fundraiser();
        domainModel.Owner = fundraiser.Owner.AsDomainModel();
        domainModel.Name = fundraiser.Name;
        domainModel.DueDate = fundraiser.DueDate;
        domainModel.GoalValue = fundraiser.GoalValue;
        return domainModel;
    }

    public static Resources.Fundraiser AsResource(this Domain.Fundraiser fundraiser)
    {
        var resourceModel = new Resources.Fundraiser();
        resourceModel.Id = fundraiser.Id;
        resourceModel.Name = fundraiser.Name;
        resourceModel.CreationDate = fundraiser.CreationDate;
        resourceModel.DueDate = fundraiser.DueDate;
        resourceModel.GoalValue = fundraiser.GoalValue;
        resourceModel.CurrentDonationAmount = fundraiser.CurrentDonationAmount;
        resourceModel.Status = fundraiser.Status;
        return resourceModel;
    }


}