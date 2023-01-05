using AkcijeSkole.Domain.Models;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Repositories
{
    public interface IAkcijeRepository : IRepository<int, Akcija>,
                                         IAggregateRepository<int, Akcija>
    {
        Result<IEnumerable<Akcija>> GetPolaznik(int polaznikId);
    }
}
