using MediatR;
using SteelBird.Application.Wrappers;

namespace SteelBird.Application.UseCases;

public record GetOrdersQuery(int Skip = 0,int Take = 10) : IRequest<Result<object>>;