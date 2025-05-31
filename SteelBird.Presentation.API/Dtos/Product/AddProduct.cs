using System.Runtime.CompilerServices;
using DomainEntities = SteelBird.Presentation.API.Entities;

namespace SteelBird.Presentation.API.Dtos.Product;

public record AddProduct /*: IValidatableObject*/
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    if (string.IsNullOrEmpty(Name))
    //    {
    //        return new ValidateOptionsResult()
    //        {

    //        };
    //    }
    //}
}

public static class AddProductExtension
{
    public static DomainEntities.Product MapProduct(this AddProduct addProdcut)
    {
        return new DomainEntities.Product();
    }
}
