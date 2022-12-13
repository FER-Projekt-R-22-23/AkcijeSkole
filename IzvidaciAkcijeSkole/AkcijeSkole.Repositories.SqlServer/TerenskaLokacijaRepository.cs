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
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }
/*
    public Result<TerenskaLokacija> GetAggregate(int id)
    {
        try{
            var model = _dbContext.TerenskeLokacije
                            .Include(terenskaLokacija => terenskaLokacija.CvrstiNamjenskiObjekti)
                            .Include(terenskaLokacija => terenskaLokacija.CvrstiObjektiZaObitavanje)
                            .Include(terenskaLokacija => terenskaLokacija.Logorista)
                            .Include(terenskaLokacija => terenskaLokacija.PrivremeniObjekti)
                            .AsNoTracking()
                            .FirstOrDefault(terenskaLokacija => terenskaLokacija.IdTerenskeLokacije.Equals(id))
                            ?.ToDomainTerenskaLokacija();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<TerenskaLokacija>();
        }
        catch(Exception e)
        {
            return Results.OnException<TerenskaLokacija>(e);
        }
    }

    public Result<IEnumerable<TerenskaLokacija>> GetAllAggregates()
    {
        try
        {
            var models = _dbContext.TerenskeLokacije
                            .Include(terenskaLokacija => terenskaLokacija.CvrstiNamjenskiObjekti)
                            .Include(terenskaLokacija => terenskaLokacija.CvrstiObjektiZaObitavanje)
                            .Include(terenskaLokacija => terenskaLokacija.Logorista)
                            .Include(terenskaLokacija => terenskaLokacija.PrivremeniObjekti)
                            .AsNoTracking()
                            .Select(Mapping.ToDomainTerenskaLokacija);

            return Results.OnSuccess(models);
        }
        catch(Exception e)
        {
            return Results.OnException<IEnumerable<TerenskaLokacija>>(e);
        }
    }

    public Result UpdateAggregate(TerenskaLokacija model)
    {
        try
        {
            _dbContext.ChangeTracker.Clear();

            var dbModel = _dbContext.TerenskeLokacije
                            .Include(_ => _.CvrstiNamjenskiObjekti)
                            .Include(_ => _.CvrstiObjektiZaObitavanje)
                            .Include(_ => _.Logorista)
                            .Include(_ => _.PrivremeniObjekti)
                            .FirstOrDefault(_ => _.IdTerenskeLokacije == model.Id);

            if(dbModel == null)
            {
                return Results.OnFailure($"Terenska lokacija sa zadanim id-jem {model.id} ne postoji");
            }

            dbModel.IdTerenskeLokacije = model.Id;
            dbModel.NazivTerenskeLokacije = model.NazivTerenskaLokacija;
            dbModel.Slika = model.Slika;
            dbModel.ImaSanitarniCvor = model.ImaSanitarniCvor;
            dbModel.MjestoPbr = model.MjestoPbr;
            dbModel.Opis = model.Opis;
            

            var CvrstiNamjenskiToUpdate = _dbContext.TerenskeLokacije
                                            .FirstOrDefault(_ => _.IdTerenskeLokacije.Equals(model.Id))
                                            .CvrstiNamjenskiObjekti;
            if(CvrstiNamjenskiToUpdate != null)
            {
                CvrstiNamjenskiToUpdate.IdNamjenskiObjekt = model.CvrstiNamjenskiObjekt.IdCvrstiNamjenskiObjekt;
                CvrstiNamjenskiToUpdate.Opis = model.CvrstiNamjenskiObjekt.Opis;
            }
            else
            {
                dbModel.CvrstiNamjenskiObjekti = model.CvrstiNamjenskiObjekt;
            }
            _dbContext.TerenskeLokacije.Update(model);

            var CvrstiObjektObitavanjeToUpdate = _dbContext.TerenskeLokacije
                                                    .FirstOrDefault(_ => _.IdTerenskeLokacije.Equals(model.Id))
                                                    .CvrstiObjektiZaObitavanje;
            if(CvrstiObjektiZaObitavanjeToUpdate != null)
            {
                CvrstiObjektObitavanjeToUpdate.IdObjektaZaObitavanje = model.CvrstiObjektZaObitavanje.IdObjektZaObitavanje;
                CvrstiObjektObitavanjeToUpdate.BrojPredvidenihSpavacihMjesta = model.CvrstiObjektZaObitavanje.BrojPredvidenihSpavacihMjesta;
            }
            else
            {
                dbModel.CvrstiObjektiZaObitavanje = model.CvrstiObjektZaObitavanje;
            }
            _dbContext.TerenskeLokacije.Update(model);

            var LogoristeToUpdate = _dbContext.TerenskeLokacije
                                        .FirstOrDefault(_ => _.IdTerenskeLokacije.Equals(model.Id))
                                        .Logorista;
            if(LogoristeToUpdate != null)
            {
                LogoristeToUpdate.IdLogoriste = model.Logoriste.IdLogoriste;
                LogoristeToUpdate.KoodinateMreze = model.Logoriste.KoordinateMreze;
                LogoristeToUpdate.PredvideniBrojClanova = model.Logoriste.PredvideniBrojClanova;
            }
            else
            {
                dbModel.Logorista = model.Logoriste;
            }
            _dbContext.TerenskeLokacije.Update(model);

            var PrivremeniToUpdate = _dbContext.TerenskeLokacije
                                        .FirstOrDefault(_ => _.IdTerenskeLokacije.Equals(model.Id))
                                        .PrivremeniObjekti;
            if(PrivremeniToUpdate != null)
            {
                PrivremeniToUpdate.IdPrivremeniObjekt = model.PrivremeniObjekt.IdPrivremeniObjekt;
                PrivremeniToUpdate.Opis = model.PrivremeniObjekt.Opis;
            }
            else
            {
                dbModel.PrivremeniObjekti = model.PrivremeniObjekt;
            }
            _dbContext.TerenskeLokacije.Update(model);

            var isSuccess = _dbContext.SaveChanges() > 0;
            _dbContext.ChangeTracker.Clear();
            return isSuccess
                ? Results.OnSuccess()
                : Results.OnFailure();
        }
        catch(Exception e)
        {
            return Results.OnException(e);
        }
    }*/
}