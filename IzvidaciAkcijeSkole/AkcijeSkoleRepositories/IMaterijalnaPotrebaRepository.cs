
using AkcijeSkole.Domain.Models;
using AkcijeSkole.Repositories;
using BaseLibrary;

public interface IMaterijalnaPotrebaRepository
    : IRepository<int, MaterijalnaPotreba>,
      IAggregateRepository<int, MaterijalnaPotreba>
{
    Result<MaterijalnaPotreba> GetAkcijaAggregate(int id);
    Result<MaterijalnaPotreba> GetSkolaAggregate(int id);
    Result<MaterijalnaPotreba> GetTerenskaLokacijaAggregate(int id);
}

