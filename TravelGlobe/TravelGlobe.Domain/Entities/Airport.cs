using TravelGlobe.Domain.ValueObjects;

namespace TravelGlobe.Domain.Entities;

public sealed class Airport : Entity<int>
{
    public string Name { get; private set; }
    public string City { get; private set; }
    public string CountryCode { get; private set; }
    public AirportCode Code { get; private set; }
    public GeoCoordinate Location { get; private set; }

    private Airport() { }

    public Airport(
        string name,
        string city,
        string countryCode,
        AirportCode code,
        GeoCoordinate location)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        City = city;
        CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }
}
