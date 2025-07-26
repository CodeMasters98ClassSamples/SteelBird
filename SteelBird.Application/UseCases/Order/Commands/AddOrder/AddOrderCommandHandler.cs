using MediatR;
using SteelBird.Application.Contracts;
using SteelBird.Application.Wrappers;
using DomainEntity = SteelBird.Domain.Entities;
namespace SteelBird.Application.UseCases.Order.Commands.AddOrder;

public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Result<DomainEntity.Order>>
{
    private readonly IAppDbContext _appDbContext;
    public AddOrderCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<DomainEntity.Order>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new DomainEntity.Order() { Description = request.Description };
        return Result.Success<DomainEntity.Order>(order);
    }
}
