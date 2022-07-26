﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.User.Commands.BalanceRefill;
using PropertySales.Application.CommandsQueries.User.Queries.GetUser;
using PropertySales.SecureAuth;
using PropertySales.SecureAuth.Commands.RefreshToken;
using PropertySales.SecureAuth.Commands.Registration;
using PropertySales.SecureAuth.Commands.UpdateUser;
using PropertySales.SecureAuth.Queries.Login;
using PropertySales.WebApi.Models.User;

namespace PropertySales.WebApi.Controllers;

[Authorize]
[Route("api/user")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticatedResponse>> LoginAsync([FromBody] LoginQuery query)
    {
        return await Mediator.Send(query);
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<ActionResult<AuthenticatedResponse>> Registration([FromBody] RegistrationCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [ResponseCache(CacheProfileName = "Caching")]
    [HttpGet("get-user")]
    public async Task<ActionResult<UserVm>> Get()
    {
        var getUserQuery = new GetUserQuery()
        {
            UserId = UserId
        };
        var userVm = await Mediator.Send(getUserQuery);

        return Ok(userVm);
    }

    [HttpPatch("refill-balance/{money:decimal}")]
    public async Task<ActionResult<string>> RefillBalance(decimal money)
    {
        var balanceRefillCommand = new BalanceRefillCommand()
        {
            UserId = UserId,
            ReplenishmentAmount = money
        };
        var replenishmentAmount = await Mediator.Send(balanceRefillCommand);

        return Ok(replenishmentAmount);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticatedResponse>> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPut("update-user")]
    public async Task<ActionResult> Update([FromBody] UpdateUserDto dto)
    {
        var updateUserCommand = _mapper.Map<UpdateUserCommand>(dto);
        updateUserCommand.Id = UserId;

        await Mediator.Send(updateUserCommand);

        return NoContent();
    }
}
