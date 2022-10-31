﻿using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using System.Data;

namespace AkcijeSkole.Repositories.SqlServer;
public class SkoleRepository : ISkoleRepository<int, Skole>
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public SkoleRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Skole model)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        throw new NotImplementedException();
    }

    public Option<Skole> Get(int id)
    {
        var skola = _dbContext.Skole
                             .AsNoTracking()
                             .FirstOrDefault(skola => skola.IdSkole.Equals(id));

        return skola is null
            ? Options.None<Skole>()
            : Options.Some(skola);
    }

    public IEnumerable<Skole> GetAll()
    {
        return _dbContext.Skole.ToList();
    }

    public bool Insert(Skole model)
    {
        throw new NotImplementedException();
    }

    public bool Remove(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(Skole model)
    {
        throw new NotImplementedException();
    }
}
