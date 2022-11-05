using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Domain.Models;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkole.Repositories.SqlServer;

public class TerenskeLokacijeRepository : ITerenskeLokacijeRepository
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public TerenskeLokacijeRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(TerenskeLokacije model)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        throw new NotImplementedException();
    }

    public Result<TerenskeLokacije> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Result<IEnumerable<TerenskeLokacije>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Result Insert(TerenskeLokacije model)
    {
        throw new NotImplementedException();
    }

    public Result Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Result Update(TerenskeLokacije model)
    {
        throw new NotImplementedException();
    }
}