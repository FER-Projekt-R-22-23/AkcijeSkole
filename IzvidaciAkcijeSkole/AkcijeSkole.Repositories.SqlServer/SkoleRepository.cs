using AkcijeSkole.Commons;
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
        return _dbContext.Skole.AsNoTracking().Contains(model);
    }

    public bool Exists(int id)
    {
        return _dbContext.Skole
                         .AsNoTracking()
                         .FirstOrDefault(skola => skola.IdSkole.Equals(id)) != null;
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
        if (_dbContext.Skole.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    public bool Remove(int id)
    {
        var model = _dbContext.Skole
                              .AsNoTracking()
                              .FirstOrDefault(skola => skola.IdSkole.Equals(id));
        if (model is not null)
        {
            _dbContext.Skole.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
        return false;
    }

    public bool Update(Skole model)
    {
        if (_dbContext.Skole.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Update attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }
}
