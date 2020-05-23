using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{

  [Authorize]
  [Produces("application/json")]
  public class UsersController : BaseController
  {

    /// <summary>
    /// Gets a list of users
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     POST /users
    /// </remarks>
    /// <returns>List of users</returns>
    /// <response code="200">If successful</response>
    /// <response code="401">If you are not logged in</response>
    /// <response code="500">If is a problem with database</response>            
    [HttpGet]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<User>>> List()
    => await Mediator.Send(new List.Query());

    /// <summary>
    /// Gets a specific user
    /// </summary>
    /// <param name="id">user's id</param>  
    ///<returns>Specify user</returns>
    /// <remarks>
    /// Sample request:
    ///     POST /users/1
    /// </remarks>
    /// <response code="200">If successful</response>
    /// <response code="400">If validation error - incorrect id</response>
    /// <response code="401">If you are not logged in</response>
    /// <response code="404">If user with specify id doesn't exists</response>
    /// <response code="500">If is a problem with database</response>      
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<User>> Details(int id)
    => await Mediator.Send(new Details.Query { Id = id });

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="command"> user object </param>  
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /users
    ///     {
    ///        "username": "AndKow221",
    ///        "password": "qwerty",
    ///        "firstName": "Andrzej",
    ///        "lastName": "Kowalski",
    ///        "profession": "Kowal"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">If successful</response>
    /// <response code="400">If validation error - incorrect data or username is in use</response>
    /// <response code="401">If you are not logged in</response>
    /// <response code="403">If you are not logged as admin</response>
    /// <response code="500">If is a problem with database</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = "RequireAdministratorRole")]
    public async Task<ActionResult<Unit>> Create(Create.Command command)
     => await Mediator.Send(command);


    /// <summary>
    /// Edit a user
    /// </summary>
    /// <param name="command"> User object  </param>  
    /// <param name="id"> user's id </param>  
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /users/1
    ///     {
    ///        "username": "AndKow221",
    ///        "password": "qwerty",
    ///        "firstName": "Andrzej",
    ///        "lastName": "Kowalski",
    ///        "profession": "Kowal"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">If successful</response>
    /// <response code="400">If validation error - incorrect data</response>
    /// <response code="401">If you are not logged in</response>
    /// <response code="403">If you are not logged as admin</response>
    /// <response code="404">If user with specify id doesn't exists</response>
    /// <response code="500">If is a problem with database</response>
    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdministratorRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
    {
      command.Id = id;
      return await Mediator.Send(command);
    }

    /// <summary>
    /// Deletes a specific user
    /// </summary>
    /// <param name="id">user's id</param>  
    /// <remarks>
    /// Sample request:
    ///     DELETE /users/1
    /// </remarks>
    /// <response code="200">If successful</response>
    /// <response code="404">If user with specify id doesn't exists</response>
    /// <response code="401">If you are not logged in</response>
    /// <response code="403">If you are not logged as admin</response>
    /// <response code="500">If is a problem with database</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdministratorRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Unit>> Delete(int id)
     => await Mediator.Send(new Delete.Command { Id = id });


    /// <summary>
    /// Changes a specific user's photo
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST users/1/photo
    ///     {
    ///        "file": photo.png,
    ///     }
    ///
    /// </remarks>
    /// <param name="id">user's id</param>  
    /// <param name="file">photo file</param>  
    /// <response code="200">If successful</response>
    /// <response code="404">if  file in request == null</response>
    /// <response code="401">If you are not logged in</response>
    /// <response code="403">If you are not logged as admin</response>
    /// <response code="500">If is a problem with database</response>
    /// <returns>Path to photo</returns>
    [HttpPost("{id}/photo")]
    [Authorize(Policy = "RequireAdministratorRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<string> UploadProfilePicture([FromForm(Name = "file")] IFormFile file, int id)
    {
      Photo.Command command = new Photo.Command()
      {
        Id = id,
        File = file
      };
      return await Mediator.Send(command);
    }

    /// <summary>
    /// Authorize user
    /// </summary>
    /// <param name="query">{"username" : "" , "password": ""}</param>  
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /users/login
    ///     {
    ///        "username": "admin",
    ///        "password": "admin",
    ///     }
    ///
    /// </remarks>
    /// <response code="200">If successful</response>
    /// <response code="400">If validation error - incorrect data or null</response>
    /// <response code="401">If the username or password is wrong</response>
    /// <response code="500">If is a problem with database</response>
    /// <returns>Token and user's role</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserIdentity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserIdentity>> Login(Login.Query query)
    {
      return await Mediator.Send(query);
    }

  }

}