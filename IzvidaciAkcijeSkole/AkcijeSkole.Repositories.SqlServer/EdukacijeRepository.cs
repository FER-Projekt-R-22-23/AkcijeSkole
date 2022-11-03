using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using System.Data;

namespace AkcijeSkole.Repositories.SqlServer;
public class EdukacijeRepository : IEdukacijeRepository<int, Edukacije>
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public EdukacijeRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Edukacije model)
    {
        return _dbContext.Edukacije.AsNoTracking().Contains(model);
    }

    public bool Exists(int id)
    {
        return _dbContext.Edukacije
                         .AsNoTracking()
                         .FirstOrDefault(edukacije => edukacije.IdEdukacija.Equals(id)) != null;
    }

    public Option<Edukacije> Get(int id)
    {
        var edukacije = _dbContext.Edukacije
                             .AsNoTracking()
                             .FirstOrDefault(edukacije => edukacije.IdEdukacija.Equals(id));

        return edukacije is null
            ? Options.None<Edukacije>()
            : Options.Some(edukacije);
    }

    public IEnumerable<Edukacije> GetAll()
    {
        return _dbContext.Edukacije.ToList();
    }

    public bool Insert(Edukacije model)
    {
        if (_dbContext.Edukacije.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    public bool Remove(int id)
    {
        var model = _dbContext.Edukacije
                              .AsNoTracking()
                              .FirstOrDefault(edukacije => edukacije.IdEdukacija.Equals(id));
        if (model is not null)
        {
            _dbContext.Edukacije.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
        return false;
    }

    public bool Update(Edukacije model)
    {
        if (_dbContext.Edukacije.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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
