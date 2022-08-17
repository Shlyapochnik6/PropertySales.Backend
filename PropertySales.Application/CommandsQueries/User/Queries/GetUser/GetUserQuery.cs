using MediatR;

namespace PropertySales.Application.CommandsQueries.User.Queries.GetUser;

public class GetUserQuery : IRequest<UserVm>
{
    public long? UserId { get; set; }
}