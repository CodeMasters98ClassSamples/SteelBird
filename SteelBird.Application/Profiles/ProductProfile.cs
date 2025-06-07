using SteelBird.Domain.Entities;
using SteelBird.Application.Dtos.Product;

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
