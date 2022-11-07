/*
using AkcijeSkole.Commons;
using BaseLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Domain.Models;
public class MaterijalnaPotreba : AggregateRoot<int>
{ 
    private string _naziv;
    private int _organizator;
    private int _davatelj;
    private bool _zadovoljeno;
    private readonly List<AkcijaAssignment> _akcijaAssignments;
    private readonly List<SkolaAssignment> _skolaAssignments;
    private readonly List<TerenskaLokacijaAssignment> _terenskaLokacijaAssignments;

    public string Naziv { get => _naziv; set => _naziv = value; }
    public int Organizator { get => _organizator; set => _organizator = value; }
    public int Davatelj { get => _davatelj; set => _davatelj = value; }
    public bool Zadovoljeno { get => _zadovoljeno; set => _zadovoljeno = value; }
    public IReadOnlyList<AkcijaAssignment> AkcijaAssignments => _akcijaAssignments.ToList();
    public IReadOnlyList<SkolaAssignment> SkolaAssignments => _skolaAssignments.ToList();
    public IReadOnlyList<TerenskaLokacijaAssignment> TerenskaLokacijaAssignments => _terenskaLokacijaAssignments.ToList();
    public MaterijalnaPotreba(int id, string naziv,int organitator, int davatelj, bool zadovoljeno, IEnumerable<AkcijaAssignment>? akcije = null, IEnumerable<SkolaAssignment>? skole = null,
        IEnumerable<TerenskaLokacijaAssignment>? terLokacije = null) : base(id)
    {
        if (string.IsNullOrEmpty(naziv))
        {
            throw new ArgumentException($"'{nameof(naziv)}' cannot be null or empty.", nameof(naziv));
        }

        _naziv = naziv;
        _organizator = organitator;
        _davatelj = davatelj;
        _zadovoljeno = zadovoljeno;
        _akcijaAssignments = akcije?.ToList() ?? new List<AkcijaAssignment>();
        _skolaAssignments = skole?.ToList() ?? new List<SkolaAssignment>();
        _terenskaLokacijaAssignments = terLokacije?.ToList() ?? new List<TerenskaLokacijaAssignment>();
    }

    public bool AssignAkcija(Akcija akcija)
    {

        var akcijaAssignment = new AkcijaAssignment(akcija);

        _akcijaAssignments.Add(akcijaAssignment);

        return true;
    }

    public bool AssignAkcija(AkcijaAssignment akcijaAssignment)
    {
        return AssignAkcija(akcijaAssignment.Akcija);
    }

    public bool DismissFromAkcija(AkcijaAssignment akcijaAssignment)
    {
        return _akcijaAssignments.Remove(akcijaAssignment);
    }

    public bool DismissFromAkcija(Akcija akcija)
    {
        var targetAssignment = _akcijaAssignments.FirstOrDefault(aa => aa.Akcija.Equals(akcija));

        return targetAssignment != null &&
               _akcijaAssignments.Remove(targetAssignment);
    }


    public bool AssignSkola(Skola skola)
    {

        var skolaAssignment = new SkolaAssignment(skola);

        _skolaAssignments.Add(skolaAssignment);

        return true;
    }

    public bool AssignSkola(SkolaAssignment skolaAssignment)
    {
        return AssignSkola(skolaAssignment.Skola);
    }

    public bool DismissFromSkola(SkolaAssignment skolaAssignment)
    {
        return _skolaAssignments.Remove(skolaAssignment);
    }

    public bool DismissFromSkola(Skola skola)
    {
        var targetAssignment = _skolaAssignments.FirstOrDefault(sa => sa.Skola.Equals(skola));

        return targetAssignment != null &&
               _skolaAssignments.Remove(targetAssignment);
    }

    public bool AssignTerenskaLokacija(TerenskaLokacija terenskaLokacija)
    {

        var terenskaLokacijaAssignment = new TerenskaLokacijaAssignment(terenskaLokacija);

        _terenskaLokacijaAssignments.Add(terenskaLokacijaAssignment);

        return true;
    }

    public bool AssignTerenskaLokacija(TerenskaLokacijaAssignment terenskaLokacijaAssignment)
    {
        return AssignTerenskaLokacija(terenskaLokacijaAssignment.TerenskaLokacija);
    }

    public bool DismissFromTerenskaLokacija(TerenskaLokacijaAssignment terenskaLokacijaAssignment)
    {
        return _terenskaLokacijaAssignments.Remove(terenskaLokacijaAssignment);
    }

    public bool DismissFromTerenskaLokacija(TerenskaLokacija terenskaLokacija)
    {
        var targetAssignment = _terenskaLokacijaAssignments.FirstOrDefault(tl => tl.TerenskaLokacija.Equals(terenskaLokacija));

        return targetAssignment != null &&
               _terenskaLokacijaAssignments.Remove(targetAssignment);
    }

    public override Result IsValid()
    => Validation.Validate(
            (() => _naziv.Length <= 50, "Naziv ne smije biti duži od 50 znakova."),
            (() => !string.IsNullOrEmpty(_naziv.Trim()), "Naziv ne smije biti null, prazan ili whitespace.")
            );

    public override bool Equals(object? other)
    {
        return other is not null &&
                other is MaterijalnaPotreba potreba &&
               _id == potreba._id &&
               _naziv == potreba._naziv &&
               _organizator == potreba._organizator &&
               _davatelj == potreba._davatelj &&
               _zadovoljeno == potreba._zadovoljeno &&
               _akcijaAssignments.SequenceEqual(potreba._akcijaAssignments) &&
               _skolaAssignments.SequenceEqual(potreba._skolaAssignments) &&
               _terenskaLokacijaAssignments.SequenceEqual(potreba._terenskaLokacijaAssignments);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _naziv, _organizator, _davatelj, _zadovoljeno, _akcijaAssignments, _skolaAssignments, _terenskaLokacijaAssignments);
    }
}
*/

