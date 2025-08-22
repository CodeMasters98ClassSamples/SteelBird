using MediatR;
using Microsoft.EntityFrameworkCore;
using SteelBird.Application.Contracts;
using SteelBird.Application.Wrappers;
using DomainEntity = SteelBird.Domain.Entities;

namespace SteelBird.Application.UseCases;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<object>>
{
    private readonly IAppDbContext _appDbContext;
    public GetOrdersQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<object>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        //PageSize, PageNumber
        //Skip,Take
        if (request.Take > 10)
            return new Exception();

        var order = await _appDbContext
            .Set<DomainEntity.Order>()
            .AsNoTracking()
            .Select(x => new
            {
                x.OrderState,
                x.CreateAt,
                x.Code,
                x.CreateByUserId
            })
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        return Result.Success<object>(order);
    }
}