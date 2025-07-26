using MediatR;
using SteelBird.Application.Wrappers;
using DomainEntity = SteelBird.Domain.Entities;

namespace SteelBird.Application.UseCases.Order.Commands.AddOrder;

public class AddOrderCommand : IRequest<Result<DomainEntity.Order>>
{
    public string Description { get; set; }
}
