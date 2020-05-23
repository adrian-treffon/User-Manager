using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users
{
  public class Login
  {
    public class Query : IRequest<UserIdentity>
    {
      public string Username { get; set; }
      public string Password { get; set; }
    }

    public class QueryValidator : AbstractValidator<Query>
    {
      public QueryValidator()
      {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
      }
    }
    public class Handler : IRequestHandler<Query, UserIdentity>
    {
      private readonly UserManager<AppUser> _userManager;
      private readonly SignInManager<AppUser> _signInManager;
      private readonly IJwtGenerator _jwtGenerator;

      public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
      {
        _jwtGenerator = jwtGenerator;
        _signInManager = signInManager;
        _userManager = userManager;
      }

      public async Task<UserIdentity> Handle(Query request, CancellationToken cancellationToken)
      {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null) throw new RestException(HttpStatusCode.Unauthorized);

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
          return new UserIdentity
          {
            Token = _jwtGenerator.CreateToken(user),
            Role = user.Role,
          };
        }
        throw new RestException(HttpStatusCode.Unauthorized);
      }
    }
  }
}