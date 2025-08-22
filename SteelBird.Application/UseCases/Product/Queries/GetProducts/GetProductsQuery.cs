using MediatR;
using SteelBird.Application.Wrappers;

namespace SteelBird.Application.UseCases.Product.Queries.GetProducts;

public record GetProductsQuery(int Skip = 0, int Take = 10) : IRequest<Result<object>>;
