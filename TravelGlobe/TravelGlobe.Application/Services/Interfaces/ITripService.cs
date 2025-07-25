namespace TravelGlobe.Application.Services;

public interface ITripService
{
    Task<TripDTO> AddTripAsync(TripDTO request);
    Task RemoveTripAsync(int tripId);
    Task<IReadOnlyList<TripDTO>> ListTripsByUserAsync(int userId);
    Task<StatisticsDTO> GetStatisticsAsync(int userId);
}
