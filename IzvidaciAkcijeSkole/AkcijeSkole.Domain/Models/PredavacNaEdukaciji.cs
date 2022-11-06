using AkcijeSkole.Commons;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models
{
    public class PredavacNaEdukaciji : ValueObject 
    {
        private int _idClan;
        private int _idPredavac;

        public PredavacNaEdukaciji(int idPredavac, int idClan)
        {
            _idClan = idClan;
            _idPredavac = idPredavac;
        }

        public int idClan { get => _idClan; set => _idClan = value; }
        public int idPredavac { get => _idPredavac; set => _idPredavac = value; }

        public override bool Equals(object? obj)
        {
            return obj is not null &&
                    obj is PredavacNaEdukaciji predavacNaEdukaciji &&
                    _idClan == predavacNaEdukaciji.idClan &&
                    _idPredavac == predavacNaEdukaciji.idPredavac;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_idClan, _idPredavac);
        }

        public override Result IsValid()
        => Validation.Validate(
                (() => _idClan != null, "Id clana can't be null")
            );

    }

}
