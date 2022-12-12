using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Domain.Models;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using System.Data;
using AkcijeSkole.Repositories.SqlServer;
using BaseLibrary;

namespace AkcijeSkole.Repositories.SqlServer;

    public class AkcijeRepository : IAkcijeRepository
    {
        private readonly AkcijeSkoleDbContext _dbContext;

        public AkcijeRepository(AkcijeSkoleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(Akcija model)
        {
            try
            {
                return _dbContext.Akcije.
                    AsNoTracking().
                    Contains(model.ToDbModel());
                                 
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
                return _dbContext.Akcije
                                 .AsNoTracking()
                                 .FirstOrDefault(akcija => akcija.IdAkcija.Equals(id)) != null;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Result<Akcija> Get(int id)
        {
            try
            {
                var akcijaDb = _dbContext.Akcije.
                    AsNoTracking().
                    FirstOrDefault(akcija => akcija.IdAkcija.Equals(id))?
                    .ToDomainAkcija();

                return akcijaDb is not null
                    ? Results.OnSuccess(akcijaDb)
                    : Results.OnFailure<Akcija>($"No akcija with such id {id}");
            }
            catch (Exception e)
            {
                return Results.OnException<Akcija>(e);
            }
        }

    public Result<IEnumerable<Akcija>> GetAll()
    {
        try
        {
            var akcije =
                _dbContext.Akcije
                          .AsNoTracking()
                          .Select(Mapping.ToDomainAkcija);
            return Results.OnSuccess(akcije);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Akcija>>(e);
        }
    }

    public Result Insert(Akcija model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Akcije.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
            var model = _dbContext.Akcije
                          .AsNoTracking()
                          .FirstOrDefault(akcija => akcija.IdAkcija.Equals(id));
            if (model is not null)
            {
                _dbContext.Akcije.Remove(model);

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

    public Result Update(Akcija model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Akcije.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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

    public Result<Akcija> GetAggregate(int id)
    {
        try
        {
            var model = _dbContext.Akcije
                .Include(akcija => akcija.Aktivnosti)
                .AsNoTracking()
                .FirstOrDefault(akcija => akcija.IdAkcija.Equals(id))
                ?.ToDomainAkcija();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Akcija>();
        }
        catch (Exception e)
        {
            return Results.OnException<Akcija>(e);
        }
    }

    public Result<IEnumerable<Akcija>> GetAllAggregates()
    {
        try
        {
            var models = _dbContext.Akcije
                          .Include(akcija => akcija.Aktivnosti)
                          .AsNoTracking().Select(Mapping.ToDomainAkcija);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Akcija>>(e);
        }
    }

    public Result UpdateAggregate(Akcija model)
    {
        try
        {
            _dbContext.ChangeTracker.Clear();

            var dbModel = _dbContext.Akcije
                              .Include(akcija => akcija.Aktivnosti)
                              .FirstOrDefault(akcija => akcija.IdAkcija == model.Id);
            if (dbModel == null)
                return Results.OnFailure($"Akcija with id {model.Id} not found.");

            dbModel.Naziv = model.Naziv;
            dbModel.MjestoPbr = model.MjestoPbr;
            dbModel.Organizator = model.Organizator;
            dbModel.KontaktOsoba = model.KontaktOsoba;
            dbModel.Vrsta = model.Vrsta;


            foreach (var aktivnost in model.AktivnostiAkcije)
            {
                var akcijaToUpdate =
                    dbModel.Aktivnosti
                           .FirstOrDefault(a => a.AkcijaId.Equals(model.Id) && a.IdAktivnost.Equals(aktivnost.Id));
                if(akcijaToUpdate == null)
                {
                    dbModel.Aktivnosti.Add(aktivnost.ToDbModel());
                }
            }

            dbModel.Aktivnosti
                   .Where(ak => !model.AktivnostiAkcije.Any(_ => _.Id == ak.IdAktivnost))
                   .ToList()
                   .ForEach(ak =>
                   {
                       dbModel.Aktivnosti.Remove(ak);
                   });

            _dbContext.Akcije
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

