using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Domain.Models;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkole.Repositories.SqlServer;

public class TerenskaLokacijaRepository : ITerenskaLokacijaRepository
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public TerenskaLokacijaRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(TerenskaLokacija model)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        throw new NotImplementedException();
    }

    public Result<TerenskaLokacija> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Result<IEnumerable<TerenskaLokacija>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Result Insert(TerenskaLokacija model)
    {
        throw new NotImplementedException();
    }

    public Result Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Result Update(TerenskaLokacija model)
    {
        throw new NotImplementedException();
    }
}