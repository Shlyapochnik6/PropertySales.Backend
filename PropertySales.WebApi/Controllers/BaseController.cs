using System.Security.Claims;
using AutoMapper.Internal.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PropertySales.WebApi.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    internal long? UserId => User.Identity!.IsAuthenticated
        ? long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
        : null;
}