using AkcijeSkole.Domain.Models;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Repositories
{
    public interface IZahtjeviRepository
    : IRepository<int, Zahtjev>
    {
        public Result<ZahtjevDetails> GetZahtjevDetails(int id);
        public Result OdgvorNaZahtjev(ZahtjevOdgovor odgovor);
    }
}
