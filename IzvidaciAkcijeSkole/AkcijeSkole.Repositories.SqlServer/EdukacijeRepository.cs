using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using System.Data;
using AkcijeSkole.Domain.Models;
using BaseLibrary;

namespace AkcijeSkole.Repositories.SqlServer;
public class EdukacijeRepository : IEdukacijeRepository
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public EdukacijeRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Edukacija model)
    {
        try
        {
            return _dbContext.Edukacije
                             .AsNoTracking()
                             .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }

    }

    public bool Exists(int id)
    {
        try
        {
            return _dbContext.Edukacije
                             .AsNoTracking()
                             .FirstOrDefault(edukacija => edukacija.IdEdukacija.Equals(id)) != null;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public Result<Edukacija> Get(int id)
    {
        try
        {
            var edukacija = _dbContext.Edukacije
                                 .AsNoTracking()
                                 .FirstOrDefault(edukacija => edukacija.IdEdukacija.Equals(id))?
                                 .ToDomain();

            return edukacija is not null
                ? Results.OnSuccess(edukacija)
                : Results.OnFailure<Edukacija>($"No edukacija with such id {id}");
        }
        catch (Exception e)
        {
            return Results.OnException<Edukacija>(e);
        }

    }

    public Result<IEnumerable<Edukacija>> GetAll()
    {
        try
        {
            var edukacije =
                _dbContext.Edukacije
                          .AsNoTracking()
                          .Select(Mapping.ToDomain);
            return Results.OnSuccess(edukacije);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Edukacija>>(e);
        }
    }

    public Result Insert(Edukacija model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Edukacije.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                var isSuccess = _dbContext.SaveChanges() > 0;

                // every Add attaches the entity object and EF begins tracking
                // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
                _dbContext.Entry(dbModel).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                return isSuccess
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }

            return Results.OnFailure();
        }
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }

    public Result Remove(int id)
    {
        try
        {
            var model = _dbContext.Edukacije
                          .AsNoTracking()
                          .FirstOrDefault(edukacija => edukacija.IdEdukacija.Equals(id));
            if (model is not null)
            {
                _dbContext.Edukacije.Remove(model);

                return _dbContext.SaveChanges() > 0
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }
            return Results.OnFailure();
        }
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }

    public Result Update(Edukacija model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Edukacije.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
            {
                var isSuccess = _dbContext.SaveChanges() > 0;

                // every Update attaches the entity object and EF begins tracking
                // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
                _dbContext.Entry(dbModel).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                return isSuccess
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }

            return Results.OnFailure();
        }
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }
}
