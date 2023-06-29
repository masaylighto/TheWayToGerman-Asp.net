﻿using Core.DataKit;
using Core.DataKit.Result;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRespository { get; set; }
    ICategoryRepository CatagoriesRepository { get; set; }
    ILanguageRepository LanguageRepository { get; set; }
    Task<Result<OK>> SaveAsync();
}
