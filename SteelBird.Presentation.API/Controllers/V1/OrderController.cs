using Microsoft.AspNetCore.Mvc;
using SteelBird.Application.UseCases.Order.Commands.AddOrder;
using SteelBird.Application.Wrappers;
using SteelBird.Presentation.API.Service;

namespace SteelBird.Presentation.API.Controllers.V1;

public class OrderController : GeneralController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddOrderCommand command, CancellationToken ct = default)
        => await SendAsync(command, ct);

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }
}
