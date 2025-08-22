using MediatR;
using Microsoft.EntityFrameworkCore;
using SteelBird.Application.Contracts;
using SteelBird.Application.Wrappers;
using DomainEntity = SteelBird.Domain.Entities;

namespace SteelBird.Application.UseCases.Product.Queries.GetProducts;


public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<object>>
{
    private readonly IAppDbContext _appDbContext;
    public GetProductsQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<object>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {

        //PageSize, PageNumber
        //Skip,Take
        if (request.Take > 10)
            return new Exception();

        var product = await _appDbContext
            .Set<DomainEntity.Product>()
                .Include(x => x.Category)
            .AsNoTracking()
            .Select(x => new
            {
                x.Price,
                x.Description,
                x.Name
            })
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        return Result.Success<object>(product);
    }
}