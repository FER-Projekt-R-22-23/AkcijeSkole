using AkcijeSkole.Commons;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models
{
    public class PolaznikSkole
    : ValueObject
    {
        private int _idPolaznik;
        private int _idEdukacija;

        public PolaznikSkole(int idPolaznik, int idEdukacija)
        {
            _idPolaznik = idPolaznik;
            _idEdukacija = idEdukacija;
        }

        public int idPolaznik { get => _idPolaznik; set => _idPolaznik = value; }
        public int idEdukacija { get => _idEdukacija; set => _idEdukacija = value; }

        public override bool Equals(object? obj)
        {
            return obj is not null &&
                    obj is PolaznikSkole polaznikSkole &&
                    _idEdukacija == polaznikSkole.idEdukacija &&
                    _idPolaznik == polaznikSkole.idPolaznik;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_idEdukacija, _idPolaznik);
        }

        public override Result IsValid()
        => Validation.Validate(
                (() => _idEdukacija != null, "Id edukacije can't be null"),
                (() => _idPolaznik != null, "Id polaznik can't be null")
            );

    }
}
