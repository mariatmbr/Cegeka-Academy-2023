using PetShelterDemo.DAL;
using PetShelterDemo.Domain;

var shelter = new PetShelter();

Console.WriteLine("Hello, Welcome the the Pet Shelter!");

var exit = false;
try
{
    while (!exit)
    {
        PresentOptions(
            "Here's what you can do.. ",
            new Dictionary<string, Action>
            {
                { "Register a newly rescued pet", RegisterPet },
                { "Donate", Donate },
                { "See current donations total", SeeDonations },
                { "See our residents", SeePets },
                { "Register a new fundraiser", RegisterFundraiser},
                { "See all fundraisers", SeeFundraisers},
                { "Check info about a specific fundraiser", SeeInfoAboutFundraiser},
                { "Donate to a specific fundraiser", DonateToFundraiser},
                { "Break our database connection", BreakDatabaseConnection },
                { "Leave:(", Leave }
            }
        );
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
    Console.WriteLine("Please try again later.");
}

void RegisterPet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");
    var pet = new Pet(name, description);

    shelter.RegisterPet(pet);
}

void Donate()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    Console.WriteLine("How much would you like to donate?");
    var amountInRon = ReadInteger();
    shelter.Donate(person, amountInRon);
}

void SeeDonations()
{
    Console.WriteLine($"Our current donation total is {shelter.GetTotalDonationsInRON()}RON");
    Console.WriteLine("Special thanks to our donors:");

    var donors = shelter.GetAllDonors();

    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}

void SeePets()
{
    var pets = shelter.GetAllPets();
    var petOptions = new Dictionary<string, Action>();

    foreach (var pet in pets)
    {
        petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
    }

    PresentOptions("We got..", petOptions);
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name);
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void BreakDatabaseConnection()
{
    Database.ConnectionIsDown = true;
}

void Leave()
{
    Console.WriteLine("Good bye!");
    exit = true;
}

void PresentOptions(string header, IDictionary<string, Action> options)
{

    Console.WriteLine(header);

    for (var index = 0; index < options.Count; index++)
    {
        Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
    }

    var userInput = ReadInteger(options.Count);

    options.ElementAt(userInput - 1).Value();
}

string ReadString(string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var value = Console.ReadLine();
    Console.WriteLine("");
    return value;
}

int ReadInteger(int maxValue = int.MaxValue, string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadInteger(maxValue, header);
    }

    Console.WriteLine("");
    return userInput;
}

void RegisterFundraiser()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");
    Console.WriteLine("Donation Target");
    var donationTarget = ReadInteger();

    var fundraiser = new Fundraiser(name, description, donationTarget);

    shelter.RegisterFundraiser(fundraiser);
}

void SeeFundraisers()
{
    Console.WriteLine("Our fundraisers are: ");
    var fundraisers = shelter.GetAllFundraisers();
    foreach (var fundraiser in fundraisers)
    {
        Console.WriteLine(fundraiser.Name);
    }
}

void DonateToFundraiser()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();
    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);
    Console.WriteLine("Choose your currency (3 letters e.g. EUR)");
    var currency = ReadString();
    Console.WriteLine("How much would you like to donate?");
    var amount = ReadInteger();
    Console.WriteLine("To whom do you want to donate?");
    SeeFundraisers();
    Console.WriteLine("Please choose from them.");
    var fName = ReadString();
    shelter.DonateToFundraiser(fName, person, currency, amount);
}

void SeeInfoAboutFundraiser()
{
    Console.WriteLine("Fundraisers:");
    SeeFundraisers();

    Console.WriteLine("Please choose from them.");
    string name = ReadString();

    Fundraiser fundraiser = shelter.GetFundraiserByName(name);

    Console.WriteLine("Fundraiser Name:" + fundraiser.Name);
    Console.WriteLine("Fundraiser Description:" + fundraiser.Description);
    Console.WriteLine("Fundraiser Donations Target:" + fundraiser.DonationTargetInRons);
    Console.WriteLine("Fundraiser Donations:");

    foreach (var donation in fundraiser.Donations)
    {
        Console.WriteLine(donation.Amount + " " + donation.Currency);
    }

    Console.WriteLine("Fundraiser Donors:");
    
    foreach(var person in fundraiser.Persons)
    {
        Console.WriteLine(person.Name);

    }
}
