using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApiApp.Core.Commands;
using TestWebApiApp.Core.Models;
using TestWebApiApp.Core.Queries;

namespace TestWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator _mediator) : ControllerBase
    {
        //private readonly IMediator _mediator;

        //public UserController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user, [FromServices] IMediator mediator)
        {
            // Create a LoginCommand
            var loginCommand = new LoginCommand
            {
                Username = user.Username,
                Password = user.Password
            };

            // Use MediatR to handle the command
            var result = mediator.Send(loginCommand);

            // Handle the result and return an appropriate response

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserData(int id)
        {
            var user = await _mediator.Send(new GetUserQuery(id));
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserData(int id, [FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Implement delete user logic
            // ...

            return Ok("User deleted successfully");
        }
    }
}
