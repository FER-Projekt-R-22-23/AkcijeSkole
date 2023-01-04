using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models
{
    public class ZahtjevDetails {

        private int _IdZahtjev;
        private int _IdMaterijalnaPotreba;
        private string _NazivMaterijalnaPotreba;
        private double _Kolicina;
        private string _MjernaJedinica;
        private int _MjestoPbr;
        private int _Organizator;

        public ZahtjevDetails(int idZahtjev, int idMaterijalnaPotreba, string nazivMaterijalnaPotreba, double kolicina, string mjernaJedinica, int mjestoPbr, int organizator)
        {
            _IdZahtjev = idZahtjev;
            _IdMaterijalnaPotreba = idMaterijalnaPotreba;
            _NazivMaterijalnaPotreba = nazivMaterijalnaPotreba;
            _Kolicina = kolicina;
            _MjernaJedinica = mjernaJedinica;
            _MjestoPbr = mjestoPbr;
            _Organizator = organizator;
        }

        public int IdZahtjev { get => _IdZahtjev; set => _IdZahtjev = value; }
        public int IdMatPotreba { get => _IdMaterijalnaPotreba; set => _IdMaterijalnaPotreba = value; }
        public string NazivMatPotreba { get => _NazivMaterijalnaPotreba; set => _NazivMaterijalnaPotreba = value; }
        public double Kolicina { get => _Kolicina; set => _Kolicina = value; }
        public string MjernaJedinica { get => _MjernaJedinica; set => MjernaJedinica = value; }
        public int MjestoPbr { get => _MjestoPbr; set => _MjestoPbr = value; }
        public int Organizator { get => _Organizator; set => Organizator = value; }
    }
}
