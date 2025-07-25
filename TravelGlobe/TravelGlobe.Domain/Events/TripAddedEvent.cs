using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Domain.Events;

public sealed class TripAddedEvent : INotification
{
    public User User { get; }
    public Trip Trip { get; }

    public TripAddedEvent(User user, Trip trip)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Trip = trip ?? throw new ArgumentNullException(nameof(trip));
    }
}
