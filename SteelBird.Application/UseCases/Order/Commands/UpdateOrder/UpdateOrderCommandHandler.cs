using MediatR;
using SteelBird.Application.Contracts;
using SteelBird.Application.UseCases.Order.Commands.AddOrder;
using SteelBird.Application.Wrappers;
using DomainEntity = SteelBird.Domain.Entities;

namespace SteelBird.Application.UseCases;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<bool>>
{
    private readonly IAppDbContext _appDbContext;
    public UpdateOrderCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _appDbContext.Set<DomainEntity.Order>().FirstOrDefault(x => x.Id == request.Id);
        if (order is null)
            throw new Exception("");

        order.Description = request.Description;
        await _appDbContext.SaveChangesAsync();

        return Result.Success<bool>(true);
    }
}
