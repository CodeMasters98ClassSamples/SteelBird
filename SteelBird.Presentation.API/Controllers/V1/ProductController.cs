using Microsoft.AspNetCore.Mvc;
using SteelBird.Presentation.API.Contracts;
using SteelBird.Presentation.API.Dtos.Product;
using SteelBird.Presentation.API.Entities;
using SteelBird.Presentation.API.Extensions;
using SteelBird.Presentation.API.Wrappers;

namespace SteelBird.Presentation.API.Controllers.V1;

//Seperation Of Concern

public class ProductController : GeneralController
{

    IBaseService<Product> _productService;
    public ProductController(IBaseService<Product> productService)
    {
        _productService = productService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var product = _productService.GetById(id);

        return Ok(product);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = _productService.GetAll();
        //if (products is null)
        //    return Result.Failure(error: Error.NullValue).WithMessage("");

        //return Result.Success().ToObjectResult(false);
        return Result.Success(products).ToObjectResult(false);
        //return Ok(products);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProduct product)
    {
        Product oProduct = new Product()
        {
            Name = product.Name,
        };
        var result = _productService.Add(oProduct);
        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProduct product)
    {
        Product oProduct = new Product()
        {
            Name = product.Name,
        };
        var result = _productService.Update(oProduct);
        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = _productService.Delete(id);
        return Ok(result);
    }
}
