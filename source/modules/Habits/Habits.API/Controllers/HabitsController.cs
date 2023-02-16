using Habits.Application.Commands;
using Habits.Application.DTO;
using Habits.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions.Dispatchers;
using System.Net;

namespace Habits.API.Controllers;

[Route("api/habits")]
[ApiController]
public class HabitsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public HabitsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [Route("")]
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<HabitDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> BrowseHabits()
    {
        var habits = await _dispatcher.Send(new BrowseHabits());
        return Ok(habits);
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> CreateHabit([FromBody] CreateHabit command)
    {
        return Created(string.Empty, await _dispatcher.Send(command));
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> RemoveHabit([FromRoute] Guid id)
    {
        return Ok(await _dispatcher.Send(new RemoveHabit(id)));
    }
}
