using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Domain.Events;

public sealed class TripRemovedEvent : INotification
{
    public User User { get; }
    public Trip Trip { get; }

    public TripRemovedEvent(User user, Trip trip)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Trip = trip ?? throw new ArgumentNullException(nameof(trip));
    }
}
