using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using System.Data;
using AkcijeSkole.Domain.Models;
using BaseLibrary;
using System;
using System.Diagnostics;

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
            var model = _dbContext.Edukacije
                          .AsNoTracking()
                          .FirstOrDefault(edukacija => edukacija.IdEdukacija.Equals(id));
            return model is not null;
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
            var model = _dbContext.Edukacije
                          .AsNoTracking()
                          .FirstOrDefault(edukacija => edukacija.IdEdukacija.Equals(id))?
                          .ToDomainEdukacija();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Edukacija>($"No edukacija with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<Edukacija>(e);
        }
    }

    public Result<Edukacija> GetAggregate(int id)
    {
     
        try
        {
            var model = _dbContext.Edukacije
                          .Include(edukacija => edukacija.PolazniciSkole)
                          .Include(edukacija => edukacija.Predavaci)
                          .Include(edukacija => edukacija.PrijavljeniPolazniciSkole)
                          .AsNoTracking()
                          .FirstOrDefault(edukacija => edukacija.IdEdukacija.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomainEdukacija();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Edukacija>();
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
            var models = _dbContext.Edukacije
                           .AsNoTracking()
                           .Select(Mapping.ToDomainEdukacija);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Edukacija>>(e);
        }
    }

    public Result<IEnumerable<Edukacija>> GetAllAggregates()
    {
        try
        {
            var models = _dbContext.Edukacije
                          .Include(edukacija => edukacija.Predavaci)
                          .AsNoTracking().Select(Mapping.ToDomainEdukacija);

            return Results.OnSuccess(models);
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
            // detach
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

    public Result UpdateAggregate(Edukacija model)
    {
        try
        {
            _dbContext.ChangeTracker.Clear();

            var dbModel = _dbContext.Edukacije
                              .Include(edukacija => edukacija.Predavaci)
                              //.AsNoTracking()
                              .FirstOrDefault(_ => _.IdEdukacija == model.Id);
            if (dbModel == null)
                return Results.OnFailure($"Edukacija with id {model.Id} not found.");

            dbModel.NazivEdukacija = model.NazivEdukacije;
            dbModel.OpisEdukacije = model.OpisEdukacije;
            dbModel.MjestoPbr = model.MjestoPbr;
            dbModel.SkolaId = model.SkolaId;


            // check if persons in roles have been modified or added
            foreach (var predavacNaEdukaciji in model.PredavaciNaEdukaciji)
            {
                // it exists in the DB, so just update it
                var predavacNaEdukacijiToUpdate =
                    dbModel.Predavaci
                           .FirstOrDefault(pr => pr.EdukacijaId.Equals(model.Id) && pr.ClanId.Equals(predavacNaEdukaciji.idClan));
                if (predavacNaEdukacijiToUpdate != null)
                {
                    predavacNaEdukacijiToUpdate.IdPredavac = predavacNaEdukaciji.idPredavac;
                    predavacNaEdukacijiToUpdate.ClanId = predavacNaEdukaciji.idClan;
                }
                else // it does not exist in the DB, so add it
                {
                    dbModel.Predavaci.Add(predavacNaEdukaciji.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.Predavaci
                   .Where(pr => !model.PredavaciNaEdukaciji.Any(_ => _.idPredavac == pr.IdPredavac))
                   .ToList()
                   .ForEach(predavac =>
                   {
                       dbModel.Predavaci.Remove(predavac);
                   });

            _dbContext.Edukacije
                      .Update(dbModel);


            var isSuccess = _dbContext.SaveChanges() > 0;
            _dbContext.ChangeTracker.Clear();
            return isSuccess
                ? Results.OnSuccess()
                : Results.OnFailure();
        }
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }
}

    
