using MediatR;

namespace PropertySales.Application.CommandsQueries.Role.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest
{
    public long RoleId { get; set; }
    public string Name { get; set; }
}