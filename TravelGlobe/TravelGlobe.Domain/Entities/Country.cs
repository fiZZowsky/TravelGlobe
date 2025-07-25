using TravelGlobe.Domain.Events;

namespace TravelGlobe.Domain.Entities;

public sealed class Country : Entity<string>, IAggregateRoot
{
    private readonly List<Trip> _trips = new();
    public IReadOnlyCollection<Trip> Trips => _trips.AsReadOnly();

    public string Name { get; private set; }

    private Country() { }

    public Country(string countryCode, string name) : base(countryCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nazwa kraju nie może być pusta.", nameof(name));
        Name = name;
    }

    internal void RegisterTrip(Trip trip)
    {
        _trips.Add(trip);
        RaiseDomainEvent(new TripAddedToCountryEvent(this, trip));
    }
}
