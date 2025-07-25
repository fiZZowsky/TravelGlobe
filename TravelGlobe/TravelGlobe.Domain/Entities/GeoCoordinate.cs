namespace TravelGlobe.Domain.ValueObjects
{
    public sealed class GeoCoordinate : IValueObject
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoCoordinate(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentOutOfRangeException(nameof(latitude));
            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude));

            Latitude = latitude;
            Longitude = longitude;
        }

        public double DistanceTo(GeoCoordinate other, double radius = 6371.0)
        {
            var dLat = ToRadians(other.Latitude - Latitude);
            var dLon = ToRadians(other.Longitude - Longitude);
            var lat1 = ToRadians(Latitude);
            var lat2 = ToRadians(other.Latitude);

            var a = Math.Pow(Math.Sin(dLat / 2), 2)
                  + Math.Pow(Math.Sin(dLon / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Asin(Math.Sqrt(a));
            return radius * c;
        }

        private static double ToRadians(double deg) => deg * (Math.PI / 180);

        public override bool Equals(object obj)
            => obj is GeoCoordinate other
            && Latitude == other.Latitude
            && Longitude == other.Longitude;

        public override int GetHashCode() => HashCode.Combine(Latitude, Longitude);
    }
}
