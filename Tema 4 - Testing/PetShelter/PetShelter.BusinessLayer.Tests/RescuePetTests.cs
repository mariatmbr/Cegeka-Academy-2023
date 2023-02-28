using FluentAssertions;
using Moq;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Models;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests
{
    public class RescuePetTests
    {
        private readonly IPersonService _personService;
        private readonly PetService _petServiceSut;

        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IPetRepository> _mockPetRepository;
        private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;

        private RescuePetRequest _request;

        public RescuePetTests()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockPetRepository = new Mock<IPetRepository>();
            _mockIdNumberValidator = new Mock<IIdNumberValidator>();

            _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object, new PersonValidator());
            _petServiceSut = new PetService(_personService, _mockPetRepository.Object, new RescuePetRequestValidator(), new AdoptPetRequestValidator());
        }

        private void Setup()
        {
            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);

            _request = new RescuePetRequest
            {
                PetName = "Alondra",
                Type = Constants.PetType.Dog,
                Description = "Nice dog",
                IsHealthy = true,
                ImageUrl = "test",
                WeightInKg = 10,
                Person = new BusinessLayer.Models.Person
                {
                    DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge),
                    IdNumber = "1111222233334",
                    Name = "Maria"
                }
            };
        }

        [Fact]
        public async void GivenValidData_WhenRescuePet_PetIsAdded()
        {
            //Arrange
            Setup();

            //Act
            await _petServiceSut.RescuePet(_request);

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Once);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(-5)]
        [InlineData(0)]
        public async Task GiventWeightIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(decimal weight)
        {
            // Arrange
            Setup();
            _request.WeightInKg = weight;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }


        [Fact]
        public async Task GivenIdNumberIsInvalid_WhenRescuePet_ThenThowsArgumentException()
        {
            //Arrange
            Setup();

            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(false);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            exception.Message.Should().Be("CNP format is invalid");

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GivenPetNameIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(string name)
        {
            // Arrange
            Setup();
            _request.PetName = name;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public async Task GivenTypeIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(int type)
        {
            // Arrange
            Setup();
            _request.Type = (Constants.PetType)type;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenPersonNull_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded()
        {
            // Arrange
            Setup();
            _request.Person = null;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }


        [Theory]
        [InlineData("111")]
        [InlineData("10")]
        [InlineData(" ")]
        public async Task GivenPersonWithInvalidIdNumberLength_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(string id)
        {
            // Arrange
            Setup();
            _request.Person.IdNumber = id;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("M")]
        [InlineData("m")]
        public async Task GivenPersonWithInvalidName_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(string name)
        {
            // Arrange
            Setup();
            _request.Person.Name = name;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }
    }
}

