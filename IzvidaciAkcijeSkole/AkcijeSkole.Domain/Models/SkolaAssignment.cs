using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models;
    public class SkolaAssignment : ValueObject
{

    private Skola _skola;

    public SkolaAssignment(Skola skola)
    {
        _skola = skola ?? throw new ArgumentNullException(nameof(skola));
    }

    public Skola Skola { get => _skola; set => _skola = value; }
    public override bool Equals(object? other)
    {
        return other is not null &&
                other is SkolaAssignment assignment &&
               _skola.Equals(assignment._skola);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_skola);
    }

    public override Result IsValid()
    => Validation.Validate(
                (() => _skola != null, "Skola ne smije biti null")
            );
}

