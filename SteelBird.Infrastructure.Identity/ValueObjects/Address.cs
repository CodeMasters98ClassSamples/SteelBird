namespace SteelBird.Infrastructure.Identity.ValueObjects;

public class Address
{
    // ctor برای EF و برای خودمان
    private Address() { }
    public Address(string country, string city, string street, string postalCode)
    {
        if (string.IsNullOrEmpty(postalCode) || postalCode.Length != 10)
        {
            //Domain Exception
        }
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public string Country { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string PostalCode { get; private set; }
}
