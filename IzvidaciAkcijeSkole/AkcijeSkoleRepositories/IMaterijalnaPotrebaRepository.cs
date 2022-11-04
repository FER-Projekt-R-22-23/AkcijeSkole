using AkcijeSkole.Domain.Models;
using AkcijeSkole.Repositories;
using System;

public interface IMaterijalnaPotrebaRepository
    : IRepository<int, MaterijalnaPotreba>,
      IAggregateRepository<int, MaterijalnaPotreba>
{
}
