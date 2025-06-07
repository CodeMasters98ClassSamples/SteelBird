
namespace SteelBird.Application.Dtos.Product;

public record AddProduct /*: IValidatableObject*/
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
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
    //public static Product MapProduct(this AddProduct addProdcut)
    //{
    //    return new Product();
    //}
}
