using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;
using Application.Errors;
using System.Net;

namespace Application.Users
{
  public class Edit
  {
    public class Command : IRequest
    {
      public int Id { get; set; }
      public string PhotoUrl { get; set; }
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
        RuleFor(x => x.Id).NotEmpty();
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
        var user = await _context.Users.FindAsync(request.Id);
        if (user == null) throw new RestException(HttpStatusCode.NotFound, new { activity = "Not found" });

        if (user.FirstName == request.FirstName && user.LastName == request.LastName && user.PhotoUrl == request.PhotoUrl && user.Profession == request.Profession) return Unit.Value;

        user.FirstName = request.FirstName ?? user.FirstName;
        user.LastName = request.LastName ?? user.LastName;
        user.PhotoUrl = request.PhotoUrl ?? user.PhotoUrl;
        user.Profession = request.Profession ?? user.Profession;

        var success = await _context.SaveChangesAsync() > 0;
        if (success) return Unit.Value;
        throw new Exception("Problem saving changes");
      }
    }
  }
}