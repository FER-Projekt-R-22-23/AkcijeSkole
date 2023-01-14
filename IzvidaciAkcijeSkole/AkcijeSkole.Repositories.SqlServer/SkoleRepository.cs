using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Domain.Models;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using System.Data;
using AkcijeSkole.Repositories.SqlServer;
using BaseLibrary;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;

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
                                 .ToDomainSkola();

            return role is not null
                ? Results.OnSuccess(role)
                : Results.OnFailure<Skola>($"No skola with such id {id}");
        }
        catch (Exception e)
        {
            return Results.OnException<Skola>(e);
        }

    }

    public Result<IEnumerable<Skola>> GetSkole(int polaznikId)
    {
        try
        {
            var polaznik = _dbContext.PolazniciSkole.AsNoTracking().FirstOrDefault(p => p.Polaznik == polaznikId);
            if(polaznik == null) return Results.OnSuccess<IEnumerable<Skola>>(new List<Skola>());
            var sk = from s in _dbContext.Skole
                       where s.PolazniciSkole.Contains(polaznik)
                       select s;

            var skole = sk.Select(Mapping.ToDomainSkola);
            return Results.OnSuccess(skole);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Skola>>(e);
        }

    }

    public Result<Skola> GetAggregate(int id)
    {
        try
        {
            var model = _dbContext.Skole
                          .Include(skola => skola.Edukacije)
                          .AsNoTracking()
                          .FirstOrDefault(skola => skola.IdSkole.Equals(id)) // give me the first or null; substitute for .Where() // single or default throws an exception if more than one element meets the criteria
                          ?.ToDomainSkola();


            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Skola>();
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
                          .Select(Mapping.ToDomainSkola);
            return Results.OnSuccess(skole);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Skola>>(e);
        }
    }

    public Result<IEnumerable<Skola>> GetAllAggregates()
    {
        try
        {
            var models = _dbContext.Skole
                          .Include(skola => skola.Edukacije)
                          .AsNoTracking().Select(Mapping.ToDomainSkola);

            return Results.OnSuccess(models);
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
                          .Include(_skole => _skole.Edukacije)
                          .Include(_skole => _skole.PolazniciSkole)
                          .Include(_skole => _skole.PrijavljeniPolazniciSkole)
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

    public Result UpdateAggregate(Skola model)
    {
        try
        {
            _dbContext.ChangeTracker.Clear();

            var dbModel = _dbContext.Skole
                              .Include(skola => skola.Edukacije)
                              //.AsNoTracking()
                              .FirstOrDefault(_ => _.IdSkole == model.Id);
            if (dbModel == null)
                return Results.OnFailure($"Skola with id {model.Id} not found.");

            dbModel.NazivSkole = model.NazivSkole;
            dbModel.MjestoPbr = model.MjestoPbr;
            dbModel.Organizator = model.Organizator;
            dbModel.KontaktOsoba = model.KontaktOsoba;


            // check if persons in roles have been modified or added
            foreach (var edukacija in model.EdukacijeUSkoli)
            {
                // it exists in the DB, so just update it
                var edukacijaToUpdate =
                    dbModel.Edukacije
                           .FirstOrDefault(pr => pr.SkolaId.Equals(model.Id) && pr.IdEdukacija.Equals(edukacija.Id));
                if (edukacijaToUpdate != null)
                {
                    edukacijaToUpdate.NazivEdukacija = edukacija.NazivEdukacije;
                    edukacijaToUpdate.OpisEdukacije = edukacija.OpisEdukacije;
                    edukacijaToUpdate.MjestoPbr = edukacija.MjestoPbr;
                }
                else // it does not exist in the DB, so add it
                {
                    dbModel.Edukacije.Add(edukacija.ToDbModel());
                }
            }


            dbModel.Edukacije
                   .Where(pr => !model.EdukacijeUSkoli.Any(_ => _.Id == pr.IdEdukacija))
                   .ToList()
                   .ForEach(edukacija =>
                   {
                       dbModel.Edukacije.Remove(edukacija);
                   });


            _dbContext.Skole
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
