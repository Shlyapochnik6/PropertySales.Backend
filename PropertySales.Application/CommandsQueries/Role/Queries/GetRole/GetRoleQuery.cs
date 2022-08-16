using MediatR;

namespace PropertySales.Application.CommandsQueries.Role.Queries.GetRole;

public class GetRoleQuery : IRequest<RoleVm>
{
    public long RoleId { get; set; }
}