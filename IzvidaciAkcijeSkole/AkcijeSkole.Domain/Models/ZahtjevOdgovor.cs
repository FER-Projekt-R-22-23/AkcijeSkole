using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models
{
    public class ZahtjevOdgovor
    {
        private int _IdZahtjev;
        private int _Davatelj;
        private string _Status;


        public ZahtjevOdgovor(int idZahtjev, int davatelj, string status)
        {
            _IdZahtjev = idZahtjev;
            _Davatelj = davatelj;
            _Status = status;
        }

        public int IdZahtjev { get => _IdZahtjev; set => _IdZahtjev = value; }
        public int Davatelj { get => _Davatelj; set => _Davatelj = value; }
        public string Status { get => _Status; set => _Status = value; }
    }
}
