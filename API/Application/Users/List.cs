using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users
{
  public class List
  {
    public class Query : IRequest<List<User>>
    {
    }

    public class Handler : IRequestHandler<Query, List<User>>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken) 
      {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<List<AppUser>, List<User>>(users);
      }
     
    }
  }
}