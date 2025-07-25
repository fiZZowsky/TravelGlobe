using TravelGlobe.Domain.ValueObjects;

namespace TravelGlobe.Application.Services
{
    public class DistanceCalculator : IDistanceCalculator
    {
        public double Calculate(GeoCoordinate a, GeoCoordinate b)
        {
            double ToRad(double deg) => deg * (Math.PI / 180);

            var dLat = ToRad(b.Latitude - a.Latitude);
            var dLon = ToRad(b.Longitude - a.Longitude);
            var lat1 = ToRad(a.Latitude);
            var lat2 = ToRad(b.Latitude);

            var sinDLat = Math.Sin(dLat / 2);
            var sinDLon = Math.Sin(dLon / 2);
            var h = sinDLat * sinDLat +
                    sinDLon * sinDLon * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Asin(Math.Sqrt(h));

            const double R = 6371.0; // km
            return R * c;
        }
    }
}
