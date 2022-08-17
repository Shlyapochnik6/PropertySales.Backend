using MediatR;

namespace PropertySales.Application.CommandsQueries.User.Commands.BalanceRefill;

public class BalanceRefillCommand : IRequest<string>
{
    public long? UserId { get; set; }
    public decimal ReplenishmentAmount { get; set; }
}