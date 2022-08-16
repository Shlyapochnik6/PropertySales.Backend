using MediatR;

namespace PropertySales.Application.CommandsQueries.Role.Commands.CreateRole;

public class CreateRoleCommand : IRequest<long>
{
    public string Name { get; set; }
}