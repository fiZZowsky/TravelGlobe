namespace TravelGlobe.Domain.ValueObjects;

public interface IDistanceCalculator
{
    double Calculate(GeoCoordinate pointA, GeoCoordinate pointB);
}
