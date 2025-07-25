using System.ComponentModel.DataAnnotations.Schema;
using TravelGlobe.Domain.ValueObjects;

namespace TravelGlobe.Domain.Entities;

public sealed class Trip : Entity<int>, IAggregateRoot
{
    public int UserId { get; private set; }
    public string CountryCode { get; private set; }
    public int DepartureAirportId { get; private set; }
    public int ArrivalAirportId { get; private set; }
    public int ReturnDepartureAirportId { get; private set; }
    public int ReturnArrivalAirportId { get; private set; }
    [NotMapped]
    public AirportCode Departure { get; private set; }
    [NotMapped]
    public AirportCode Arrival { get; private set; }
    [NotMapped]
    public AirportCode ReturnDeparture { get; private set; }
    [NotMapped]
    public int DistanceKm { get; private set; }

    private Trip() { }

    internal Trip(
        int userId,
        string countryCode,
        int departureAirportId,
        int arrivalAirportId,
        int returnDepartureAirportId,
        int returnArrivalAirportId,
        int distanceKm)
    {
        UserId = userId;
        CountryCode = countryCode;
        DepartureAirportId = departureAirportId;
        ArrivalAirportId = arrivalAirportId;
        ReturnDepartureAirportId = returnDepartureAirportId;
        ReturnArrivalAirportId = returnArrivalAirportId;
        DistanceKm = distanceKm;
    }
}