using AkcijeSkole.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Repositories
{
    public interface IAktivnostiRepository
    : IRepository<int, Aktivnost>,
    IAggregateRepository<int, Aktivnost>
    {
    }
}
