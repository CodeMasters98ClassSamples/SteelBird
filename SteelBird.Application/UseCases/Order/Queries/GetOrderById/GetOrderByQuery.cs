using MediatR;
using SteelBird.Application.Wrappers;

namespace SteelBird.Application.UseCases;

public record GetOrderByIdQuery(int Id) : IRequest<Result<object>>;
