
using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace AkcijeSkole.Repositories.SqlServer;

public class MaterijalnaPotrebaRepository : IMaterijalnaPotrebaRepository<int, MaterijalnePotrebe>
{
    private readonly AkcijeSkoleDbContext _dbContext;

    public MaterijalnaPotrebaRepository(AkcijeSkoleDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public bool Exists(MaterijalnePotrebe model)
	{
        return _dbContext.MaterijalnePotrebe
                         .AsNoTracking()
                         .Contains(model);
    }

	public bool Exists(int idMaterijalnaPotreba)
	{
        var model = _dbContext.MaterijalnePotrebe
                              .AsNoTracking()
                              .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(idMaterijalnaPotreba));
        return model is not null;
    }

	public Option<MaterijalnePotrebe> Get(int idMaterijalnaPotreba)
	{
        var model = _dbContext.MaterijalnePotrebe
                              .AsNoTracking()
                              .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(idMaterijalnaPotreba));

        return model is not null
            ? Options.Some(model)
            : Options.None<MaterijalnePotrebe>();
    }

    public Option<MaterijalnePotrebe> GetAggregate(int idMaterijalnaPotreba)
    {
        var model = _dbContext.MaterijalnePotrebe
                              .Include(potreba => potreba.Akcije)
                              .AsNoTracking()
                              .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(idMaterijalnaPotreba)); // give me the first or null; substitute for .Where()
                                                                               // single or default throws an exception if more than one element meets the criteria

        return model is not null
            ? Options.Some(model)
            : Options.None<MaterijalnePotrebe>();
    }

    public IEnumerable<MaterijalnePotrebe> GetAll()
	{
        var models = _dbContext.MaterijalnePotrebe
                               .ToList();

        return models;
    }

    public IEnumerable<MaterijalnePotrebe> GetAllAggregates()
    {
        var models = _dbContext.MaterijalnePotrebe
                                .Include(potreba => potreba.Akcije)
                                .ToList();

        return models;
    }

    public bool Insert(MaterijalnePotrebe model)
	{
        if (_dbContext.MaterijalnePotrebe.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Add attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

	public bool Remove(int idMaterijalnaPotreba)
	{
        var model = _dbContext.MaterijalnePotrebe
                             .AsNoTracking()
                             .FirstOrDefault(potreba => potreba.IdMaterijalnePotrebe.Equals(idMaterijalnaPotreba));

        if (model is not null)
        {
            _dbContext.MaterijalnePotrebe.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
        return false;
    }

	public bool Update(MaterijalnePotrebe model)
	{
        if (_dbContext.MaterijalnePotrebe.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Update attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    public bool UpdateAggregate(MaterijalnePotrebe model)
    {
        if (_dbContext.MaterijalnePotrebe.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Update attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }
}
