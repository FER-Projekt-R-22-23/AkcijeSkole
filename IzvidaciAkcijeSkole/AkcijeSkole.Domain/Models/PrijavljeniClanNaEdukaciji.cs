using AkcijeSkole.Commons;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models
{
    public class PrijavljeniClanNaEdukaciji
    : ValueObject
    {
        private int _idPolaznik;
        private DateTime _datumPrijave;

        public PrijavljeniClanNaEdukaciji(int idPolaznik, DateTime datumPrijave)
        {
            _idPolaznik = idPolaznik;
            _datumPrijave = datumPrijave;
        }

        public int idPolaznik { get => _idPolaznik; set => _idPolaznik = value; }
        public DateTime datumPrijave { get => _datumPrijave; set => _datumPrijave = value; }

        public override bool Equals(object? obj)
        {
            return obj is not null &&
                    obj is PrijavljeniClanNaEdukaciji prijavljeniPolaznik &&
                    _idPolaznik == prijavljeniPolaznik.idPolaznik &&
                    _datumPrijave == prijavljeniPolaznik.datumPrijave;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_idPolaznik, _datumPrijave);
        }

        public override Result IsValid()
        => Validation.Validate(
                (() => _idPolaznik != null, "Id polaznik can't be null")
            );

    }
}
