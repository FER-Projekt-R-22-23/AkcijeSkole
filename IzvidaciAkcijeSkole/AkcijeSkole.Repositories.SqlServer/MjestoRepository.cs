
using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Domain.Models;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using System;
namespace AkcijeSkole.Repositories.SqlServer;

public class MjestoRepository : IMjestoRepository
{

    private readonly AkcijeSkoleDbContext _dbContext;

    public MjestoRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Mjesto model)
    {
        try
        {
            return _dbContext.Mjesta
                     .AsNoTracking()
                     .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Exists(int pbr)
    {
        var model = _dbContext.Mjesta
                              .AsNoTracking()
                              .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(pbr));
        return model is not null;
    }

    public Result<Mjesto> Get(int id)
    {
        try
        {
            var model = _dbContext.Mjesta
                          .AsNoTracking()
                          .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(id))?
                          .ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Mjesto>($"Ne postoji mjesto s pbr {id}");
        }
        catch (Exception e)
        {
            return Results.OnException<Mjesto>(e);
        }
    }

    public Result<Mjesto> GetAkcijaAggregate(int id)
    {
        try
        {
            var model = _dbContext.Mjesta
                          .Include(mjesto => mjesto.Akcije)
                          .AsNoTracking()
                          .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomain();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Mjesto>();
        }
        catch (Exception e)
        {
            return Results.OnException<Mjesto>(e);
        }
    }

    public Result<Mjesto> GetAktivnostAggregate(int id)
    {
        try
        {
            var model = _dbContext.Mjesta
                          .Include(mjesto => mjesto.Aktivnosti)
                          .AsNoTracking()
                          .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomain();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Mjesto>();
        }
        catch (Exception e)
        {
            return Results.OnException<Mjesto>(e);
        }
    }

    public Result<Mjesto> GetEdukacijaAggregate(int id)
    {
        try
        {
            var model = _dbContext.Mjesta
                          .Include(mjesto => mjesto.Edukacije)
                          .AsNoTracking()
                          .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomain();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Mjesto>();
        }
        catch (Exception e)
        {
            return Results.OnException<Mjesto>(e);
        }
    }

    public Result<Mjesto> GetSkoleAggregate(int id)
    {
        try
        {
            var model = _dbContext.Mjesta
                          .Include(mjesto => mjesto.Skole)
                          .AsNoTracking()
                          .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomain();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Mjesto>();
        }
        catch (Exception e)
        {
            return Results.OnException<Mjesto>(e);
        }
    }

    public Result<Mjesto> GetTerenskeLokacijeAggregate(int id)
    {
        try
        {
            var model = _dbContext.Mjesta
                          .Include(Mjesto => Mjesto.TerenskeLokacije)
                          .AsNoTracking()
                          .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomain();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Mjesto>();
        }
        catch (Exception e)
        {
            return Results.OnException<Mjesto>(e);
        }
    }


    public Result<IEnumerable<Mjesto>> GetAll()
    {
        try
        {
            var models = _dbContext.Mjesta
                           .AsNoTracking()
                           .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Mjesto>>(e);
        }
    }


    public Result<IEnumerable<Mjesto>> GetAllAggregates()
    {
        try
        {
            var models = _dbContext.Mjesta
                           .Include(mjesto => mjesto.Akcije)
                           .Include(mjesto => mjesto.Aktivnosti)
                           .Include(mjesto => mjesto.Edukacije)
                           .Include(mjesto => mjesto.Skole)
                           .Include(Mjesto => Mjesto.TerenskeLokacije)
                           .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Mjesto>>(e);
        }
    }


    public Result Insert(Mjesto model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Mjesta.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
            var model = _dbContext.Mjesta
                          .AsNoTracking()
                          .FirstOrDefault(mjesto => mjesto.PbrMjesta.Equals(id));

            if (model is not null)
            {
                _dbContext.Mjesta.Remove(model);

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

    public Result Update(Mjesto model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.Mjesta.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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

    public Result UpdateAggregate(Mjesto model)
    {
        try
        {
            _dbContext.ChangeTracker.Clear();

            var dbModel = _dbContext.Mjesta
                              .Include(_ => _.Akcije)
                              .Include(mjesto => mjesto.Aktivnosti)
                              .Include(mjesto => mjesto.Edukacije)
                              .Include(mjesto => mjesto.Skole)
                              .Include(Mjesto => Mjesto.TerenskeLokacije)
                              //.AsNoTracking()
                              .FirstOrDefault(_ => _.PbrMjesta == model.Id);
            if (dbModel == null)
                return Results.OnFailure($"Mjesto s pbr {model.Id} nije pronadeno.");

            
            dbModel.NazivMjesta = model.NazivMjesta;
            dbModel.PbrMjesta = model.Id;

            // check if mjesta in akcije have been modified or added
            foreach (var akcijeAssignment in model.AkcijaAssignments)
            {
                // it exists in the DB, so just update it
                var mjToUpdate =
                    dbModel.Akcije
                           .FirstOrDefault(pr => pr.MjestoPbr.Equals(model.Id) && pr.MjestoPbr.Equals(akcijeAssignment.Akcija.MjestoPbr));
                
                if(mjToUpdate != null) // it does not exist in the DB, so add it
                {
                    dbModel.Akcije.Add(akcijeAssignment.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.Akcije
                   .Where(pr => !model.AkcijaAssignments.Any(_ => _.Akcija.IdAkcija == pr.IdAkcija))
                   .ToList()
                   .ForEach(p =>
                   {
                       dbModel.Akcije.Remove(p);
                   });

            _dbContext.Mjesta
                      .Update(dbModel);

            foreach (var aktivnostAssignment in model.AktivnostAssignments)
            {
                // it exists in the DB, so just update it
                var mjToUpdate =
                    dbModel.Aktivnosti
                           .FirstOrDefault(pr => pr.MjestoPbr.Equals(model.Id) && pr.MjestoPbr.Equals(aktivnostAssignment.Aktivnost.MjestoPbr));

                if (mjToUpdate != null) // it does not exist in the DB, so add it
                {
                    dbModel.Aktivnosti.Add(aktivnostAssignment.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.Aktivnosti
                   .Where(pr => !model.AktivnostAssignments.Any(_ => _.Aktivnost.IdAktivnost == pr.IdAktivnost))
                   .ToList()
                   .ForEach(p =>
                   {
                       dbModel.Aktivnosti.Remove(p);
                   });

            _dbContext.Mjesta
                      .Update(dbModel);

            foreach (var edukacijaAssignment in model.EdukacijaAssignments)
            {
                // it exists in the DB, so just update it
                var mjToUpdate =
                    dbModel.Edukacije
                           .FirstOrDefault(pr => pr.MjestoPbr.Equals(model.Id) && pr.MjestoPbr.Equals(edukacijaAssignment.Edukacija.MjestoPbr));

                if (mjToUpdate != null) // it does not exist in the DB, so add it
                {
                    dbModel.Edukacije.Add(edukacijaAssignment.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.Edukacije
                   .Where(pr => !model.EdukacijaAssignments.Any(_ => _.Edukacija.IdEdukacija == pr.IdEdukacija))
                   .ToList()
                   .ForEach(p =>
                   {
                       dbModel.Edukacije.Remove(p);
                   });

            _dbContext.Mjesta
                      .Update(dbModel);

            foreach (var skolaAssignment in model.SkolaAssignments)
            {
                // it exists in the DB, so just update it
                var mjToUpdate =
                    dbModel.Skole
                           .FirstOrDefault(pr => pr.MjestoPbr.Equals(model.Id) && pr.MjestoPbr.Equals(skolaAssignment.Skola.MjestoPbr));

                if (mjToUpdate != null) // it does not exist in the DB, so add it
                {
                    dbModel.Skole.Add(skolaAssignment.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.Skole
                   .Where(pr => !model.SkolaAssignments.Any(_ => _.Skola.IdSkole == pr.IdSkole))
                   .ToList()
                   .ForEach(p =>
                   {
                       dbModel.Skole.Remove(p);
                   });

            _dbContext.Mjesta
                      .Update(dbModel);

            foreach (var terenskaLokacijaAssignment in model.TerenskaLokacijaAssignments)
            {
                // it exists in the DB, so just update it
                var mjToUpdate =
                    dbModel.TerenskeLokacije
                           .FirstOrDefault(pr => pr.MjestoPbr.Equals(model.Id) && pr.MjestoPbr.Equals(terenskaLokacijaAssignment.TerenskaLokacija.MjestoPbr));

                if (mjToUpdate != null) // it does not exist in the DB, so add it
                {
                    dbModel.TerenskeLokacije.Add(terenskaLokacijaAssignment.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.TerenskeLokacije
                   .Where(pr => !model.TerenskaLokacijaAssignments.Any(_ => _.TerenskaLokacija.IdTerenskeLokacije == pr.IdTerenskeLokacije))
                   .ToList()
                   .ForEach(p =>
                   {
                       dbModel.TerenskeLokacije.Remove(p);
                   });

            _dbContext.Mjesta
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

    Result<IEnumerable<Mjesto>> IAggregateRepository<int, Mjesto>.GetAllAggregates()
    {
        try
        {
            var models = _dbContext.Mjesta
                           .Include(_ => _.Akcije)
                           .Include(mjesto => mjesto.Aktivnosti)
                           .Include(mjesto => mjesto.Edukacije)
                           .Include(mjesto => mjesto.Skole)
                           .Include(Mjesto => Mjesto.TerenskeLokacije)
                           .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Mjesto>>(e);
        }
    }
}

