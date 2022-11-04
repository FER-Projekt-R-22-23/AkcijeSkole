using AkcijeSkole.Domain.Models;
using AkcijeSkole.Repositories;
using System;

public interface IMjestoRepository
    : IRepository<int, Mjesto>,
      IAggregateRepository<int, Mjesto>
{
}

