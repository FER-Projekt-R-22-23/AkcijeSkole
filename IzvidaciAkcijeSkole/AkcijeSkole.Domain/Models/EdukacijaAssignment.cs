using AkcijeSkole.Commons;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models;
    public class EdukacijaAssignment : ValueObject
{

    private Edukacija _edukacija;

    public EdukacijaAssignment(Edukacija edukacija)
    {
        _edukacija = edukacija ?? throw new ArgumentNullException(nameof(edukacija));
    }

    public Edukacija Edukacija { get => _edukacija; set => _edukacija = value; }
    public override bool Equals(object? other)
    {
        return other is not null &&
                other is EdukacijaAssignment assignment &&
               _edukacija.Equals(assignment._edukacija);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_edukacija);
    }

    public override Result IsValid()
    => Validation.Validate(
                (() => _edukacija != null, "Aktivnost ne smije biti null")
            );
}
    }

