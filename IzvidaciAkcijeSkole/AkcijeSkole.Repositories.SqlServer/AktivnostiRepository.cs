using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using System.Data;
using AkcijeSkole.Domain.Models;
using BaseLibrary;
using System;

namespace AkcijeSkole.Repositories.SqlServer;

public class AktivnostiRepository : IAktivnostiRepository
{
    private readonly AkcijeSkoleDbContext _dbContext;
        
    public AktivnostiRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Aktivnost model)
    {
        try
        {
            return _dbContext.Aktivnosti
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
            var model = _dbContext.Aktivnosti
                          .AsNoTracking()
                          .FirstOrDefault(aktivnost => aktivnost.IdAktivnost.Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Result<Aktivnost> Get(int id)
    {
        try
        {
            var model = _dbContext.Aktivnosti
                          .AsNoTracking()
                          .FirstOrDefault(aktivnost => aktivnost.IdAktivnost.Equals(id))?
                          .ToDomainAktivnost();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Aktivnost>($"No aktivnost with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<Aktivnost>(e);
        }
    }

    public Result<IEnumerable<Aktivnost>> GetAll()
    {
        try
        {
            var models = _dbContext.Aktivnosti
                           .AsNoTracking()
                           .Select(Mapping.ToDomainAktivnost);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Aktivnost>>(e);
        }
    }

    public Result Insert(Aktivnost model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Aktivnosti.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
            var model = _dbContext.Aktivnosti
                          .AsNoTracking()
                          .FirstOrDefault(aktivnosti => aktivnosti.IdAktivnost.Equals(id));

            if (model is not null)
            {
                _dbContext.Aktivnosti.Remove(model);

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

    public Result Update(Aktivnost model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.Aktivnosti.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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

