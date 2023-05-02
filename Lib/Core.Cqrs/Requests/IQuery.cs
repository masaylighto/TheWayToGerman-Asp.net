﻿using Core.DataKit.Result;
using MediatR;

namespace Core.Cqrs.Requests;

public interface IQuery<Data> : IRequest<Result<Data>>
{
}
