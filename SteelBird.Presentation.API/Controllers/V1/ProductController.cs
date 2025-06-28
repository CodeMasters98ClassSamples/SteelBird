using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteelBird.Application.Dtos.Product;
using SteelBird.Application.Wrappers;
using SteelBird.Domain.Entities;
using SteelBird.Presentation.API.Contracts;
using SteelBird.Presentation.API.Extensions;

namespace SteelBird.Presentation.API.Controllers.V1;

//Seperation Of Concern

public class ProductController : GeneralController
{

    IBaseService<Product> _productService;
    private readonly IMapper _mapper;
    public ProductController(IBaseService<Product> productService, IMapper mapper)
    {
        _mapper = mapper;
        _productService = productService;
    }

    [MapToApiVersion("1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var product = _productService.GetById(id);
        if (product is null)
            return NotFound();

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

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProduct product)
    {
        var oProduct = _mapper.Map<AddProduct, Product>(product);
        var result = _productService.Add(oProduct);
        return Created();
    }

    [Authorize]
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

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = _productService.Delete(id);
        return Ok(result);
    }
}
