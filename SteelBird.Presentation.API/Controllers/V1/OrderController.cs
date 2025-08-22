using Microsoft.AspNetCore.Mvc;
using SteelBird.Application.UseCases;
using SteelBird.Application.UseCases.Order.Commands.AddOrder;

namespace SteelBird.Presentation.API.Controllers.V1;

public class OrderController : GeneralController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddOrderCommand command, CancellationToken ct = default)
        => await SendAsync(command, ct);

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOrderCommand command, CancellationToken ct = default)
        => await SendAsync(command, ct);

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetOrderByIdQuery query, CancellationToken ct = default)
        => await SendAsync(query, ct);

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetOrdersQuery query, CancellationToken ct = default)
       => await SendAsync(query, ct);
}
