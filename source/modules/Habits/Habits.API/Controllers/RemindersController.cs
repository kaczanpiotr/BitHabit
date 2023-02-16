using Habits.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions.Dispatchers;

namespace Habits.API.Controllers;

[Route("api/reminders")]
[ApiController]
public class RemindersController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public RemindersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> AddReminder([FromBody] AddReminder command)
    {
        return Ok(await _dispatcher.Send(command));
    }

    [Route("")]
    [HttpDelete]
    public async Task<IActionResult> RemoveReminder([FromBody] RemoveReminder command)
    {
        return Ok(await _dispatcher.Send(command));
    }


}
