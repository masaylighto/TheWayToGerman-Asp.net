﻿using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Api.DTO.Admin;
using TheWayToGerman.Api.DTO.Category;
using TheWayToGerman.Api.ResponseObject.Admin;
using TheWayToGerman.Api.ResponseObject.Category;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Cqrs.Commands.Admin;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Core.ModelBinders.Models;

namespace TheWayToGerman.Api.Controllers;
[ApiController]
[Route("api/v1/Category")]
public class CategoryController : ControllerBase
{
    public IMediator Mediator { get; }
    public CategoryController(IMediator mediator)
    {
        Mediator = mediator;
    }
    [HttpPost]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDTO DTO)
    {
        var userCommand = DTO.Adapt<CreateCategoryCommand>();
        var result = await Mediator.Send(userCommand);
        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
        }
        return Ok(result.GetData().Adapt<CreateCategoryResponse>());
    }
}
