using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models
{
    public class Zahtjev : Entity<int>
    {
        private int _IdMatPotreba;
        private string _Status;


        public Zahtjev(int id, int idMatPotreba, string status) : base(id)
        {
            _IdMatPotreba = idMatPotreba;
            _Status = status;
        }

        public int IdMatPotreba { get => _IdMatPotreba; set => _IdMatPotreba = value; }
        public string Status { get => _Status; set => _Status = value; }
        public override bool Equals(object? other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override Result IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
