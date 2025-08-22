using MediatR;
using SteelBird.Application.Contracts;
using SteelBird.Application.Wrappers;
using DomainEntity = SteelBird.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SteelBird.Application.UseCases;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<object>>
{
    private readonly IAppDbContext _appDbContext;
    public GetOrderByIdQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<object>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _appDbContext
            .Set<DomainEntity.Order>()
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new
            {
                x.OrderState,
                x.CreateAt,
                x.Code,
                x.CreateByUserId
            })
            .FirstOrDefaultAsync(cancellationToken);

        return Result.Success<object>(order);
    }
}