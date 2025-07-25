using System.Collections.ObjectModel;
using TravelGlobe.Domain.Events;

namespace TravelGlobe.Domain.Entities;

public sealed class User : Entity<int>, IAggregateRoot
{
    private readonly List<Trip> _trips = new();
    public IReadOnlyCollection<Trip> Trips => new ReadOnlyCollection<Trip>(_trips);

    public string DisplayName { get; private set; }

    private User() { } // ORM / serializer

    public User(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException("Nazwa nie może być pusta.", nameof(displayName));
        DisplayName = displayName;
    }

    public void ChangeDisplayName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Nazwa nie może być pusta.", nameof(newName));
        DisplayName = newName;
    }

    internal void AddTrip(Trip trip)
    {
        if (trip is null) throw new ArgumentNullException(nameof(trip));
        _trips.Add(trip);
        RaiseDomainEvent(new TripAddedEvent(this, trip));
    }

    internal void RemoveTrip(Trip trip)
    {
        if (_trips.Remove(trip))
            RaiseDomainEvent(new TripRemovedEvent(this, trip));
    }
}