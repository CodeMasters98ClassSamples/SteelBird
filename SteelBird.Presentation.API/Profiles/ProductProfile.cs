using SteelBird.Presentation.API.Dtos.Product;
using SteelBird.Presentation.API.Entities;

namespace SteelBird.Presentation.API.Profiles;

public class ProductProfile : AutoMapper.Profile
{
    //AddProduct -> Product
    //UpdateProduct -> Product

    //Product -> ProductDto


    public ProductProfile()
    {
        CreateMap<AddProduct, Product>();
        CreateMap<UpdateProduct, Product>();
    }
}
