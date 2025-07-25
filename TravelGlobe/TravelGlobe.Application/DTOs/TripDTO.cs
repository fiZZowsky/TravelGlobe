namespace TravelGlobe.Application;

public class TripDTO
{
    public int TripId { get; set; }
    public string CountryCode { get; set; } = null!;
    public int DepartureAirportId { get; set; }
    public int ArrivalAirportId { get; set; }
    public int ReturnDepartureAirportId { get; set; }
    public int ReturnArrivalAirportId { get; set; }
    public int DistanceKm { get; set; }
}