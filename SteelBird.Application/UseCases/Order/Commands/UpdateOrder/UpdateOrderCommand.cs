using MediatR;
using SteelBird.Application.Wrappers;

namespace SteelBird.Application.UseCases;

public class UpdateOrderCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public string Description { get; set; }
}