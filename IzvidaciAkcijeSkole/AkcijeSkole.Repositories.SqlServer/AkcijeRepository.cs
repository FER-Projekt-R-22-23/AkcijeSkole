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
            return Results.OnException<IEnumerable<Akcije>>(e);
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
}

