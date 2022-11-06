using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Domain.Models;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkole.Repositories.SqlServer;

public class TerenskaLokacijaRepository : ITerenskaLokacijaRepository
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public TerenskaLokacijaRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(TerenskaLokacija model)
    {
        try
        {
            return _dbContext.TerenskeLokacije
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
            var model = _dbContext.TerenskeLokacije
                            .AsNoTracking()
                            .FirstOrDefault(terenskaLokacija => terenskaLokacija.IdTerenskeLokacije.Equals(id))
            
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Result<TerenskaLokacija> Get(int id)
    {
        try
        {
            var model = _dbContext.TerenskeLokacije
                            .AsNoTracking()
                            .FirstOrDefault(terenskaLokacija => terenskaLokacija.IdTerenskeLokacije.Equals(id))?
                            .ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<TerenskaLokacija>($"No Terenska lokacija with id {id} found);
        }
        catch (Exception e)
        {
            return Results.OnException<TerenskaLokacija>(e);
        }
    }

    public Result<IEnumerable<TerenskaLokacija>> GetAll()
    {
        try
        {
            var models = _dbContext.TerenskeLokacije
                            .AsNoTracking()
                            .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch(Exception e)
        {
            return Results.OnException<IEnumerable<TerenskaLokacija>>(e);
        }
    }

    public Result Insert(TerenskaLokacija model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if(_dbContext.TerenskeLokacije.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                var isSuccess = _dbContext.SaveChanges() > 0;

                _dbContext.Entry(dbModel).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                return isSuccess
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }

            return Results.OnFailure();
        }
        catch(Exception e)
        {
            return Results.OnException(e);
        }
    }

    public Result Remove(int id)
    {
        try
        {
            var model = _dbContext.TerenskeLokacije
                            .AsNoTracking()
                            .FirstOrDefault(terenskaLokacija => terenskaLokacija.IdTerenskeLokacije.Equals(id));

            if(model is not null)
            {
                _dbContext.TerenskeLokacije.Remove(model);

                return _dbContext.SaveChanges() > 0
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }
            return Results.OnFailure();
        }
        catch(Exception e)
        {
            return Results.OnException(e);
        }
    }

    public Result Update(TerenskaLokacija model)
    {
        try
        {
            var dbModel = model.ToDbModel();

            if(_dbContext.TerenskeLokacije.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
            {
                var isSuccess = _dbContext.SaveChanges() > 0;

                _dbContext.Entry(dbModel).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                return isSuccess
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }

            return Results.OnFailure();
        }
        catch(Exception e)
        {
            return Results.OnException(e);
        }
    }
}