
using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Domain.Models;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AkcijeSkole.Repositories.SqlServer;

public class MaterijalnaPotrebaRepository : IMaterijalnaPotrebaRepository
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public MaterijalnaPotrebaRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }




	public bool Exists(int idMaterijalnaPotreba)
	{
        try
        {
            return _dbContext.MaterijalnePotrebe
                     .AsNoTracking()
                     .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Exists(MaterijalnaPotreba model)
    {
        try
        {
            return _dbContext.MaterijalnePotrebe
                     .AsNoTracking()
                     .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Result<MaterijalnaPotreba> Get(int id)
    {
        try
        {
            var model = _dbContext.MaterijalnePotrebe
                          .AsNoTracking()
                          .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(id))?
                          .ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<MaterijalnaPotreba>($"Materijalna potreba {id} nije pronadena.");
        }
        catch (Exception e)
        {
            return Results.OnException<MaterijalnaPotreba>(e);
        }
    }

    public Result<MaterijalnaPotreba> GetAkcijaAggregate(int id)
    {
        try
        {
            var model = _dbContext.MaterijalnePotrebe
                          .Include(potreba => potreba.Akcije)
                          .AsNoTracking()
                          .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomain();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<MaterijalnaPotreba>();
        }
        catch (Exception e)
        {
            return Results.OnException<MaterijalnaPotreba>(e);
        }
    }

    public Result<MaterijalnaPotreba> GetSkolaAggregate(int id)
    {
        try
        {
            var model = _dbContext.MaterijalnePotrebe
                          .Include(potreba => potreba.Skole)
                          .AsNoTracking()
                          .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomain();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<MaterijalnaPotreba>();
        }
        catch (Exception e)
        {
            return Results.OnException<MaterijalnaPotreba>(e);
        }
    }

    public Result<MaterijalnaPotreba> GetTerenskaLokacijaAggregate(int id)
    {
        try
        {
            var model = _dbContext.MaterijalnePotrebe
                          .Include(potreba => potreba.TerenskeLokacije)
                          .AsNoTracking()
                          .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomain();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<MaterijalnaPotreba>();
        }
        catch (Exception e)
        {
            return Results.OnException<MaterijalnaPotreba>(e);
        }
    }

    public Result<IEnumerable<MaterijalnaPotreba>> GetAll()
    {
        try
        {
            var models = _dbContext.MaterijalnePotrebe
                           .AsNoTracking()
                           .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<MaterijalnaPotreba>>(e);
        }
    }

    public Result<IEnumerable<MaterijalnaPotreba>> GetAllAggregates()
    {
        try
        {
            var models = _dbContext.MaterijalnePotrebe
                           .Include(potreba => potreba.Akcije)
                          .Include(potreba => potreba.Skole)
                          .Include(potreba => potreba.TerenskeLokacije)
                           .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<MaterijalnaPotreba>>(e);
        }
    }

    public Result Insert(MaterijalnaPotreba model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.MaterijalnePotrebe.Add(dbModel).State == EntityState.Added)
            {
                var isSuccess = _dbContext.SaveChanges() > 0;

                // every Add attaches the entity object and EF begins tracking
                // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
                _dbContext.Entry(dbModel).State = EntityState.Detached;

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
            var model = _dbContext.MaterijalnePotrebe
                          .AsNoTracking()
                          .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(id));

            if (model is not null)
            {
                _dbContext.MaterijalnePotrebe.Remove(model);

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

    public Result Update(MaterijalnaPotreba model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.MaterijalnePotrebe.Update(dbModel).State == EntityState.Modified)
            {
                var isSuccess = _dbContext.SaveChanges() > 0;

                // every Update attaches the entity object and EF begins tracking
                // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
                _dbContext.Entry(dbModel).State = EntityState.Detached;

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

    public Result UpdateAggregate(MaterijalnaPotreba model)
    {
        try
        {
            _dbContext.ChangeTracker.Clear();

            var dbModel = _dbContext.MaterijalnePotrebe
                              .Include(_ => _.Akcije)
                              .Include(_ => _.Skole)
                              .Include(_ => _.TerenskeLokacije)
                              //.AsNoTracking()
                              .FirstOrDefault(_ => _.IdMaterijalnePotrebe == model.Id);
            if (dbModel == null)
                return Results.OnFailure($"Materijalna potreba s id-jem {model.Id} nije pronadena.");

            dbModel.IdMaterijalnePotrebe = model.Id;
            dbModel.Naziv = model.Naziv;
            dbModel.Organizator = model.Organizator;
            dbModel.Davatelj = model.Davatelj;

            // check if persons in roles have been modified or added
            foreach (var akcijaAssignment in model.AkcijaAssignments)
            {
                // it exists in the DB, so just update it
                var potrebaToUpdate =
                    dbModel.Akcije
                           .FirstOrDefault(ak => ak.IdAkcija.Equals(model.Id) && ak.IdAkcija.Equals(akcijaAssignment.Akcija.IdAkcija));
                if (potrebaToUpdate == null)
                 // it does not exist in the DB, so add it
                {
                    dbModel.Akcije.Add(akcijaAssignment.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.Akcije
                   .Where(ak => !model.AkcijaAssignments.Any(_ => _.Akcija.IdAkcija == ak.IdAkcija))
                   .ToList()
                   .ForEach(ak =>
                   {
                       dbModel.Akcije.Remove(ak);
                   });

            _dbContext.MaterijalnePotrebe
                      .Update(dbModel);


            foreach (var skolaAssignment in model.SkolaAssignments)
            {
                // it exists in the DB, so just update it
                var potrebaToUpdate =
                    dbModel.Skole
                           .FirstOrDefault(sk => sk.IdSkole.Equals(model.Id) && sk.IdSkole.Equals(skolaAssignment.Skola.IdSkola));
                if (potrebaToUpdate == null)
                // it does not exist in the DB, so add it
                {
                    dbModel.Skole.Add(skolaAssignment.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.Skole
                   .Where(sk => !model.SkolaAssignments.Any(_ => _.Skola.IdSkole == sk.IdSkole))
                   .ToList()
                   .ForEach(sk =>
                   {
                       dbModel.Skole.Remove(sk);
                   });

            _dbContext.MaterijalnePotrebe
                      .Update(dbModel);



            foreach (var terenskaLokacijaAssignment in model.TerenskaLokacijaAssignments)
            {
                // it exists in the DB, so just update it
                var potrebaToUpdate =
                    dbModel.TerenskeLokacije
                           .FirstOrDefault(tl => tl.IdTerenskeLokacije.Equals(model.Id) && tl.IdTerenskeLokacije.Equals(terenskaLokacijaAssignment.TerenskaLokacija.IdTerenskeLokacije));
                if (potrebaToUpdate == null)
                // it does not exist in the DB, so add it
                {
                    dbModel.TerenskeLokacije.Add(terenskaLokacijaAssignment.ToDbModel(model.Id));
                }
            }

            // check if persons in roles have been removed
            dbModel.TerenskeLokacije
                   .Where(ak => !model.TerenskaLokacijaAssignments.Any(_ => _.TerenskaLokacija.IdTerenskeLokacije == ak.IdTerenskeLokacije))
                   .ToList()
                   .ForEach(tl =>
                   {
                       dbModel.TerenskeLokacije.Remove(tl);
                   });

            _dbContext.MaterijalnePotrebe
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

