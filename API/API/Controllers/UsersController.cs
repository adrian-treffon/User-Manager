using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Domain;

namespace API.Controllers
{

  [Authorize]
  public class UsersController : BaseController
  {

    [HttpGet]
    public async Task<ActionResult<List<User>>> List()
    => await Mediator.Send(new List.Query());

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Details(int id)
    => await Mediator.Send(new Details.Query { Id = id });

    [HttpPost]
    [Authorize(Policy = "RequireAdministratorRole")]
    public async Task<ActionResult<Unit>> Create(Create.Command command)
     => await Mediator.Send(command);

    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
    {
      command.Id = id;
      return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public async Task<ActionResult<Unit>> Delete(int id)
     => await Mediator.Send(new Delete.Command { Id = id });

    [HttpPost("{id}/photos")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public async Task<string> UploadProfilePicture([FromForm(Name = "file")] IFormFile file, int id)
    {
      Photo.Command command = new Photo.Command()
      {
        Id = id,
        File = file
      };
      return await Mediator.Send(command);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserIdentity>> Login(Login.Query query)
    {
      return await Mediator.Send(query);
    }

  }

}