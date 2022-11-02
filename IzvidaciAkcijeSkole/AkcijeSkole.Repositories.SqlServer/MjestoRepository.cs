using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkole.Repositories.SqlServer;

public class MjestoRepository : IMjestoRepository<int, Mjesta>
{

    private readonly AkcijeSkoleDbContext _dbContext;

    public MjestoRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Mjesta model)
    {
        return _dbContext.Mjesta
                         .AsNoTracking()
                         .Contains(model);
    }

    public bool Exists(int pbr)
    {
        var model = _dbContext.Mjesta
                              .AsNoTracking()
                              .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(pbr));
        return model is not null;
    }

    public Option<Mjesta> Get(int pbr)
    {
        var model = _dbContext.Mjesta
                              .AsNoTracking()
                              .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(pbr));

        return model is not null
            ? Options.Some(model)
            : Options.None<Mjesta>();
    }


    public IEnumerable<Mjesta> GetAll()
    {
        var models = _dbContext.Mjesta
                               .ToList();

        return models;
    }


    public bool Insert(DataAccess.SqlServer.Data.DbModels.Mjesta model)
    {
        if (_dbContext.Mjesta.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Add attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    public bool Remove(int id)
    {
        var model = _dbContext.Mjesta
                              .AsNoTracking()
                              .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(id));

        if (model is not null)
        {
            _dbContext.Mjesta.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
        return false;
    }

    public bool Update(DataAccess.SqlServer.Data.DbModels.Mjesta model)
    {
        // detach
        if (_dbContext.Mjesta.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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
