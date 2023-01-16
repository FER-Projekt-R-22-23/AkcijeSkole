using AkcijeSkole.Domain.Models;
using BaseLibrary;

namespace AkcijeSkole.Repositories;
/// <summary>
/// Facade interface for a Role repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDomainModel"></typeparam>
public interface ISkoleRepository
    : IRepository<int, Skola>,
      IAggregateRepository<int, Skola>
{
    Result<IEnumerable<Skola>> GetSkole(int polaznikId);
}
