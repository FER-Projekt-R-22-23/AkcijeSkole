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

public class ZahtjeviRepository : IZahtjeviRepository
{
    private readonly AkcijeSkoleDbContext _dbContext;
        
    public ZahtjeviRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Zahtjev model)
    {
        try
        {
            return _dbContext.Zahtjevi
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
            var model = _dbContext.Zahtjevi
                          .AsNoTracking()
                          .FirstOrDefault(z => z.IdZahtjev.Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Result<Zahtjev> Get(int id)
    {
        try
        {
            var model = _dbContext.Zahtjevi
                          .AsNoTracking()
                          .FirstOrDefault(z => z.IdZahtjev.Equals(id))?
                          .ToDomainZahtjev();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Zahtjev>($"No zahtjev with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<Zahtjev>(e);
        }
    }

    public Result<IEnumerable<Zahtjev>> GetAll()
    {
        try
        {
            var models = _dbContext.Zahtjevi
                           .AsNoTracking()
                           .Select(Mapping.ToDomainZahtjev);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Zahtjev>>(e);
        }
    }

    public Result Insert(Zahtjev model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Zahtjevi.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
            var model = _dbContext.Zahtjevi
                          .AsNoTracking()
                          .FirstOrDefault(z => z.IdZahtjev.Equals(id));

            if (model is not null)
            {
                _dbContext.Zahtjevi.Remove(model);

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

    public Result Update(Zahtjev model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.Zahtjevi.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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

    public Result OdgvorNaZahtjev(ZahtjevOdgovor odgovor)
    {
        var zahtjev = _dbContext.Zahtjevi.Where(z => z.IdZahtjev.Equals(odgovor.IdZahtjev)).FirstOrDefault();

        if (zahtjev == null)
        {
            return Results.OnFailure($"No zahtjev with id {odgovor.IdZahtjev} found");
        }

        zahtjev.Status = odgovor.Status;
        _dbContext.Update(zahtjev);
        _dbContext.SaveChanges();

        if (odgovor.Status.Equals("Odbijen")) {
            return Results.OnSuccess();
        }
        if (odgovor.Status.Equals("Ispunjen")) { 
            var matPotreba = _dbContext.MaterijalnePotrebe.Where(m => m.IdMaterijalnePotrebe.Equals(zahtjev.IdMatPotreba)).FirstOrDefault();

            if (matPotreba == null)
            {
                return Results.OnFailure($"No materijalna potreba with id {zahtjev.IdMatPotreba} found");
            }

            matPotreba.Davatelj = odgovor.Davatelj;
            matPotreba.Zadovoljeno = true;
            _dbContext.Update(zahtjev);
            _dbContext.SaveChanges();
        }
        return Results.OnSuccess("Isti status zahtjeva");
    }

    public Result<ZahtjevDetails> GetZahtjevDetails(int id)
    {
        try
        {
            var model = _dbContext.Zahtjevi
                          .AsNoTracking()
                          .FirstOrDefault(z => z.IdZahtjev.Equals(id))?
                          .ToDomainZahtjev();

            if (model == null)
            {
                return Results.OnFailure<ZahtjevDetails>($"No zahtjev with id {id} found");
            }
            var idMatPotrebe = model.IdMatPotreba;
            var matPotreba = _dbContext.MaterijalnePotrebe
                                .AsNoTracking()
                                .FirstOrDefault(m => m.IdMaterijalnePotrebe.Equals(idMatPotrebe));
            var nazivMatPotreba = matPotreba.Naziv;
            var kolicina = matPotreba.Kolicina.GetValueOrDefault(0);
            var mjernaJedinica = matPotreba.MjernaJedinica;
            var mjestoPbr = pbrMatPotrebe(idMatPotrebe);
            var organizator = matPotreba.Organizator;

            var returnModel = new ZahtjevDetails(
                id,
                idMatPotrebe,
                nazivMatPotreba,
                kolicina,
                mjernaJedinica,
                mjestoPbr,
                organizator
                );
            return Results.OnSuccess(returnModel);
        }
        catch (Exception e)
        {
            return Results.OnException<ZahtjevDetails>(e);
        }
    }

    public int pbrMatPotrebe(int idMatPotrebe) {
        var matPotreba = _dbContext.MaterijalnePotrebe
                                .Include(potreba => potreba.Akcije)
                                .Include(potreba => potreba.Skole)
                                .Include(potreba => potreba.TerenskeLokacije)
                                .AsNoTracking()
                                .FirstOrDefault(m => m.IdMaterijalnePotrebe.Equals(idMatPotrebe));
        if (matPotreba.Akcije.Count != 0) {
            return matPotreba.Akcije.FirstOrDefault()!.MjestoPbr;
        }
        if (matPotreba.Skole.Count != 0)
        {
            return matPotreba.Skole.FirstOrDefault()!.MjestoPbr;
        }
        if (matPotreba.TerenskeLokacije.Count != 0)
        {
            return matPotreba.TerenskeLokacije.FirstOrDefault()!.MjestoPbr;
        }
        return 0;
    }


}

