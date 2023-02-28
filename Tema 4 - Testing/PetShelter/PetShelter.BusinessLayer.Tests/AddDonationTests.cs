using Moq;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationTests
{
    private readonly Mock<IDonationRepository> _mockDonationRepository;
    private readonly Mock<IPersonRepository> _mockPersonRepository;
    private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;
    private readonly DonationService _donationServiceSut;
    private readonly IPersonService _personService;
    private AddDonationRequest _request;

    public AddDonationTests()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
        _mockIdNumberValidator = new Mock<IIdNumberValidator>();
        _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object, new PersonValidator());

        _mockDonationRepository = new Mock<IDonationRepository>();
        _donationServiceSut = new DonationService(_mockDonationRepository.Object, _personService, new AddDonationRequestValidator());
    }

    private void Setup()
    {
        _request = new AddDonationRequest
        {
            Amount = 1,
            DonorId = 1,
            Donor = new BusinessLayer.Models.Person
            {
                DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge),
                IdNumber = "1111222233334",
                Name = "Maria"
            }
        };
    }

    [Fact]
    public async Task GivenValidRequest_WhenAddDonation_DonationIsAdded()
    {
        //Arrange 
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);
        Setup();

        //Act
        await _donationServiceSut.AddDonation(_request);

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Once);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.0001)]
    [InlineData(0)]
    [InlineData(-10)]
    public async Task GivenRequestWithWrongAmount_WhenAddDonation_DonationIsNotAdded(decimal amount)
    {
        //Arrange 
        Setup();
        _request.Amount = amount;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Never);

    }

    [Fact]
    public async Task GivenRequestWithNoPerson_WhenAddDonation_DonationIsNotAdded()
    {
        //Arrange 
        Setup();
        _request.Donor = null;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData("M")]
    [InlineData("m")]
    [InlineData(null)]
    public async Task GivenRequestWithInvalidDonorName_WhenAddDonation_DonationIsNotAdded(string name)
    {
        //Arrange 
        Setup();
        _request.Donor.Name = name;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenRequestWithInvalidDonorId_WhenAddDonation_DonationIsNotAdded()
    {
        //Arrange 
        Setup();
        _request.DonorId = -3;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenRequestWithDonorUnder18_WhenAddDonation_DonationIsNotAdded()
    {
        //Arrange 
        Setup();
        _request.Donor.DateOfBirth = DateTime.Now;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }


}
