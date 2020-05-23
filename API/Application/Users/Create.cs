using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Users
{
  public class Create{
    public class Command : IRequest
    {
       public string PhotoUrl {get;set;} 
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Profession { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
      public CommandValidator()
      {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Profession).NotEmpty();
      }
    }
    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;
    
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        var user = new User
        {
          FirstName = request.FirstName,
          LastName = request.LastName,
          Profession = request.Profession,
          PhotoUrl = request.PhotoUrl
        };

        _context.Users.Add(user);

        var success = await _context.SaveChangesAsync() > 0;

        if (success) return Unit.Value;

        throw new Exception("Problem saving changes");
      }
    }
  }
}