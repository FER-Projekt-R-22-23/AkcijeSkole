using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Domain.Models;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using System.Data;
using AkcijeSkole.Repositories.SqlServer;
using BaseLibrary;

namespace AkcijeSkole.Repositories.SqlServer;
public class SkolaRepository : ISkoleRepository
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public SkolaRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Skola model)
    {
        try
        {
            return _dbContext.Skole
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
            return _dbContext.Skole
                             .AsNoTracking()
                             .FirstOrDefault(skola => skola.IdSkole.Equals(id)) != null;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public Result<Skola> Get(int id)
    {
        try
        {
            var role = _dbContext.Skole
                                 .AsNoTracking()
                                 .FirstOrDefault(skola => skola.IdSkole.Equals(id))?
                                 .ToDomain();

            return role is not null
                ? Results.OnSuccess(role)
                : Results.OnFailure<Skola>($"No skola with such id {id}");
        }
        catch (Exception e)
        {
            return Results.OnException<Skola>(e);
        }

    }

    public Result<IEnumerable<Skola>> GetAll()
    {
        try
        {
            var skole =
                _dbContext.Skole
                          .AsNoTracking()
                          .Select(Mapping.ToDomain);
            return Results.OnSuccess(skole);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Skola>>(e);
        }
    }

    public Result Insert(Skola model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Skole.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
            var model = _dbContext.Skole
                          .AsNoTracking()
                          .FirstOrDefault(skola => skola.IdSkole.Equals(id));
            if (model is not null)
            {
                _dbContext.Skole.Remove(model);

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

    public Result Update(Skola model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Skole.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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
