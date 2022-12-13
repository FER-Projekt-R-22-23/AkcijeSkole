using AkcijeSkole.Commons;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models
{
    public class PolaznikNaEdukaciji
    : ValueObject
    {
        private int _idPolaznik;

        public PolaznikNaEdukaciji(int idPolaznik)
        {
            _idPolaznik = idPolaznik;
        }

        public int idPolaznik { get => _idPolaznik; set => _idPolaznik = value; }

        public override bool Equals(object? obj)
        {
            return obj is not null &&
                    obj is PolaznikNaEdukaciji polaznikNaEdukaciji &&
                    _idPolaznik == polaznikNaEdukaciji.idPolaznik;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_idPolaznik);
        }

        public override Result IsValid()
        => Validation.Validate(
                (() => _idPolaznik != null, "Id polaznik can't be null")
            );

    }
}
