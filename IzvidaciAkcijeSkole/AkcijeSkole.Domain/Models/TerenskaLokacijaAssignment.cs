using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models;
    public class TerenskaLokacijaAssignment : ValueObject
{

    private TerenskaLokacija _terenskaLokacija;

    public TerenskaLokacijaAssignment(TerenskaLokacija terenskaLokacija)
    {
        _terenskaLokacija = terenskaLokacija ?? throw new ArgumentNullException(nameof(terenskaLokacija));
    }

    public TerenskaLokacija TerenskaLokacija { get => _terenskaLokacija; set => _terenskaLokacija = value; }
    public override bool Equals(object? other)
    {
        return other is not null &&
                other is TerenskaLokacijaAssignment assignment &&
               _terenskaLokacija.Equals(assignment._terenskaLokacija);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_terenskaLokacija);
    }

    public override Result IsValid()
    => Validation.Validate(
                (() => _terenskaLokacija != null, "Terenska lokacija ne smije biti null")
            );
}

