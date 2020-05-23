using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users
{
  public class Create
  {
    public class Command : IRequest
    {
      public string Username { get; set; }
      public string Password { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Profession { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
      public CommandValidator()
      {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Profession).NotEmpty();
      }
    }
    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;
      private readonly UserManager<AppUser> _userManager;
     
      public Handler(DataContext context, UserManager<AppUser> userManager)
      {
        _userManager = userManager;
        _context = context;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        if (await _context.Users.Where(x => x.UserName == request.Username).AnyAsync())
          throw new RestException(HttpStatusCode.BadRequest, new { UserName = "Username already exists" });

        var user = new AppUser
        {
          UserName = request.Username,
          FirstName = request.FirstName,
          LastName = request.LastName,
          Profession = request.Profession,
          Role = "Casual" //dafault role
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded) return Unit.Value;
        else throw new Exception("Problem creating user");
      }
    }
  }
}