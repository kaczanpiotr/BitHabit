using Habits.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions.Dispatchers;

namespace Habits.API.Controllers;

[Route("api/progress")]
[ApiController]
public class ProgressController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public ProgressController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> SetProgress([FromBody] SetOrCreateProgress command)
    {
        return Ok(await _dispatcher.Send(command));
    }
}
