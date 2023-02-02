using System;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<ICSDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<ICSDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}