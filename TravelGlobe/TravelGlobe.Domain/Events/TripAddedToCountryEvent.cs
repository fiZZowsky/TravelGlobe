using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Domain.Events;

public sealed class TripAddedToCountryEvent : INotification
{
    public Country Country { get; }
    public Trip Trip { get; }

    public TripAddedToCountryEvent(Country country, Trip trip)
    {
        Country = country ?? throw new ArgumentNullException(nameof(country));
        Trip = trip ?? throw new ArgumentNullException(nameof(trip));
    }
}