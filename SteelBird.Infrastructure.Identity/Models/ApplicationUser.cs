using Microsoft.AspNetCore.Identity;
using SteelBird.Infrastructure.Identity.ValueObjects;

namespace SteelBird.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    //public List<RefreshToken> RefreshTokens { get; set; }
    //public bool OwnsToken(string token)
    //{
    //    return this.RefreshTokens?.Find(x => x.Token == token) != null;
    //}
    public Address Address { get; set; }
    public bool Regiser(string nationalCode, int age)
    {
        if (string.IsNullOrEmpty(nationalCode) || age < 18 )
            throw new Exception();

        return false;
    }

}
