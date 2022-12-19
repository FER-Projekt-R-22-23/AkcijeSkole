using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Domain.Models;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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
                            .FirstOrDefault(terenskaLokacija => terenskaLokacija.IdTerenskeLokacije.Equals(id));
            
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Exists(CvrstiNamjenskiObjekt model)
    {
        try
        {
            return _dbContext.CvrstiNamjenskiObjekti
                             .AsNoTracking()
                             .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Exists(CvrstiObjektZaObitavanje model)
    {
        try
        {
            return _dbContext.CvrstiObjektiZaObitavanje
                             .AsNoTracking()
                             .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Exists(Logoriste model)
    {
        try
        {
            return _dbContext.Logorista
                             .AsNoTracking()
                             .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Exists(PrivremeniObjekt model)
    {
        try
        {
            return _dbContext.PrivremeniObjekti
                             .AsNoTracking()
                             .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ExistsCvrstiNamjenski(int id)
    {
        try
        {
            var model = _dbContext.CvrstiNamjenskiObjekti
                                  .AsNoTracking()
                                  .FirstOrDefault(cvrstiNamjenski => cvrstiNamjenski.IdNamjenskiObjekt.Equals(id));

            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ExistsCvrstiObitavanje(int id)
    {
        try
        {
            var model = _dbContext.CvrstiObjektiZaObitavanje
                                  .AsNoTracking()
                                  .FirstOrDefault(cvrstiObitavanje => cvrstiObitavanje.IdObjektaZaObitavanje.Equals(id));

            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ExistsLogoriste(int id)
    {
        try
        {
            var model = _dbContext.Logorista
                                  .AsNoTracking()
                                  .FirstOrDefault(logoriste => logoriste.IdLogoriste.Equals(id));

            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ExistsPrivremeni(int id)
    {
        try
        {
            var model = _dbContext.PrivremeniObjekti
                                  .AsNoTracking()
                                  .FirstOrDefault(privremeni => privremeni.IdPrivremeniObjekt.Equals(id));

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
                : Results.OnFailure<TerenskaLokacija>($"No Terenska lokacija with id {id} found");
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

    public Result<IEnumerable<CvrstiNamjenskiObjekt>> GetAllCvrstiNamjenski()
    {
        try
        {
            var models = _dbContext.CvrstiNamjenskiObjekti
                                   .AsNoTracking()
                                   .Select(Mapping.ToDomainCvrstiNamjenski());

            return Results.OnSuccess(models);
        }
        catch(Exception e)
        {
            return Results.OnException<IEnumerable<CvrstiNamjenskiObjekt>>(e);
        }
    }

    public Result<IEnumerable<CvrstiObjektZaObitavanje>> GetAllCvrstiObitavanje()
    {
        try
        {
            var models = _dbContext.CvrstiObjektiZaObitavanje
                                   .AsNoTracking()
                                   .Select(Mapping.ToDomainCvrstiObitavanje());

            return Results.OnSuccess(models);
        }
        catch(Exception e)
        {
            return Results.OnException<IEnumerable<CvrstiObjektZaObitavanje>>(e);
        }
    }

    public Result<IEnumerable<Logoriste>> GetAllLogoriste()
    {
        try
        {
            var models = _dbContext.Logorista
                                   .AsNoTracking()
                                   .Select(Mapping.ToDomainLogoriste());

            return Results.OnSuccess(models);
        }
        catch(Exception e)
        {
            return Results.OnException<IEnumerable<Logoriste>>(e);
        }
    }

    public Result<IEnumerable<PrivremeniObjekt>> GetAllPrivremeni()
    {
        try
        {
            var models = _dbContext.PrivremeniObjekti
                                   .AsNoTracking()
                                   .Select(Mapping.ToDomainPrivremeni());

            return Results.OnSuccess(models);
        }
        catch(Exception e)
        {
            return Results.OnException<IEnumerable<PrivremeniObjekt>>(e);
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

    public Result Insert(CvrstiNamjenskiObjekt model)
    {
        try
        {
            var dbModel = model.ToDbModelCvrstiNamjenski();
            if(_dbContext.CvrstiNamjenskiObjekti.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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

    public Result Insert(CvrstiObjektZaObitavanje model)
    {
        try
        {
            var dbModel = model.ToDbModelCvrstiObitavanje();
            if(_dbContext.CvrstiObjektiZaObitavanje.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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

    public Result Insert(Logoriste model)
    {
        try
        {
            var dbModel = model.ToDbModelLogoriste();
            if(_dbContext.Logorista.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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

    public Result Insert(PrivremeniObjekt model)
    {
        try
        {
            var dbModel = model.ToDbModelPrivremeni();
            if(_dbContext.PrivremeniObjekti.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }

    public Result Update(CvrstiNamjenskiObjekt model)
    {
        try
        {
            var dbModel = model.ToDbModelCvrstiNamjenski();

            if(_dbContext.CvrstiNamjenskiObjekti.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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

    public Result Update(CvrstiObjektZaObitavanje model)
    {
        try
        {
            var dbModel = model.ToDbModelCvrstiObitavanje();

            if(_dbContext.CvrstiObjektiZaObitavanje.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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

    public Result Update(Logoriste model)
    {
        try
        {
            var dbModel = model.ToDbModelLogoriste();

            if(_dbContext.Logorista.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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

    public Result Update(PrivremeniObjekt model)
    {
        try
        {
            var dbModel = model.ToDbModelPrivremeni();

            if(_dbContext.PrivremeniObjekti.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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
}