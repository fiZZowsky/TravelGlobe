using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Domain.Factories
{
    public interface ITripFactory
    {
        Trip Create(
            User user,
            Country country,
            Airport departure,
            Airport arrival,
            Airport returnDeparture,
            Airport returnArrival);
    }
}
