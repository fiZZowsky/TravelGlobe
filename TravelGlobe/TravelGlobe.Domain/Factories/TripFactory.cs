using TravelGlobe.Domain.Entities;
using TravelGlobe.Domain.ValueObjects;

namespace TravelGlobe.Domain.Factories
{
    public class TripFactory : ITripFactory
    {
        private readonly IDistanceCalculator _calculator;

        public TripFactory(IDistanceCalculator calculator)
        {
            _calculator = calculator;
        }

        public Trip Create(
            User user,
            Country country,
            Airport departure,
            Airport arrival,
            Airport returnDeparture,
            Airport returnArrival)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (country == null) throw new ArgumentNullException(nameof(country));

            // Oblicz dystans
            var coord1 = departure.Location;
            var coord2 = arrival.Location;
            var coord3 = returnDeparture.Location;
            var coord4 = returnArrival.Location;

            var outDist = _calculator.Calculate(coord1, coord2);
            var backDist = _calculator.Calculate(coord3, coord4);
            var totalKm = (int)Math.Round(outDist + backDist);

            // Stwórz Trip
            var trip = (Trip)Activator.CreateInstance(
                typeof(Trip),
                true,
                user.Id,
                country.Id,
                departure.Code,
                arrival.Code,
                returnDeparture.Code,
                returnArrival.Code,
                totalKm);

            // Powiadom agregaty o nowej podróży
            user.AddTrip(trip);
            country.RegisterTrip(trip);

            return trip;
        }
    }
}
