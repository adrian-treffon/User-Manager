using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace API.Controllers
{

  public class UsersController : BaseController
  {

    [HttpGet]
    public async Task<ActionResult<List<User>>> List()
     => await Mediator.Send(new List.Query());

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Details(Guid id)
    => await Mediator.Send(new Details.Query { Id = id });

    [HttpPost]
    public async Task<ActionResult<Unit>> Create(Create.Command command)
     => await Mediator.Send(command);

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
    {
      command.Id = id;
      return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Delete(Guid id)
     => await Mediator.Send(new Delete.Command { Id = id });

    [HttpPost("photo/{id}")]
    public async Task<string> UploadProfilePicture([FromForm(Name = "file")] IFormFile file, Guid id)
    {
      Photo.Command command = new Photo.Command()
      {
        Id = id,
        File = file
      };
      return await Mediator.Send(command);
    }
  }

}