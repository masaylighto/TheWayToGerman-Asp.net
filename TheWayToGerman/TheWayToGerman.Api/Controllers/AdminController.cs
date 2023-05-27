﻿using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Api.DTO.Admin;
using TheWayToGerman.Api.DTO.Owner;
using TheWayToGerman.Api.ResponseObject;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Core.ModelBinders.Models;
using TheWayToGerman.Core.ParametersBinders;

namespace TheWayToGerman.Api.Controllers;

[ApiController]
[Route("v1/Admin")]
public class AdminController : ControllerBase
{
    public IMediator Mediator { get; }
    public AdminController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpPut]
    [Authorize(AuthPolicies.AdminPolicy)]
    public async Task<ActionResult> UpdateSelf([FromBody]UpdateAdminDTO DTO, UserAuthClaim UserAuthClaims)
    {
        var command = DTO.Adapt<UpdateAdminCommand>();
        command.Id = UserAuthClaims.ID;
        var result = await Mediator.Send(command);
        if (result.ContainError())
        {
            return BadRequest(new ErrorResponse() { Error = result.GetError().Message });
        }
        return Ok();
    }
}