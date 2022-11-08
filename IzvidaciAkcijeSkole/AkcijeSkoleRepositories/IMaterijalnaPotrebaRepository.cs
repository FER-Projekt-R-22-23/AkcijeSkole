
using AkcijeSkole.Domain.Models;
using AkcijeSkole.Repositories;

public interface IMaterijalnaPotrebaRepository
    : IRepository<int, MaterijalnaPotreba>,
      IAggregateRepository<int, MaterijalnaPotreba>
{
}

