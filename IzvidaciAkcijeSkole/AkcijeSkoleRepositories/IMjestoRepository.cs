
using AkcijeSkole.Domain.Models;
using AkcijeSkole.Repositories;
using BaseLibrary;

public interface IMjestoRepository
    : IRepository<int, Mjesto>,
      IAggregateRepository<int, Mjesto>
{
    Result<Mjesto> GetAkcijaAggregate(int id);
    Result<Mjesto> GetAktivnostAggregate(int id);
    Result<Mjesto> GetEdukacijaAggregate(int id);
    Result<Mjesto> GetSkoleAggregate(int id);
    Result<Mjesto> GetTerenskeLokacijeAggregate(int id);
}


