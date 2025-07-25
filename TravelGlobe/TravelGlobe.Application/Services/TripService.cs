using TravelGlobe.Domain.Entities;
using TravelGlobe.Domain.Factories;
using TravelGlobe.Domain.Interfaces;

namespace TravelGlobe.Application.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly IAirportRepository _airportRepo;
        private readonly ITripFactory _tripFactory;

        public TripService(
            ITripRepository tripRepo,
            ICountryRepository countryRepo,
            IAirportRepository airportRepo,
            ITripFactory tripFactory)
        {
            _tripRepo = tripRepo;
            _countryRepo = countryRepo;
            _airportRepo = airportRepo;
            _tripFactory = tripFactory;
        }

        public async Task<TripDTO> AddTripAsync(TripDTO req)
        {
            var country = await _countryRepo.GetByCodeAsync(req.CountryCode)
                                   ?? throw new KeyNotFoundException($"Kraj {req.CountryCode} nie istnieje.");
            var depAirport = await _airportRepo.GetByIdAsync(req.DepartureAirportId)
                               ?? throw new KeyNotFoundException($"Lotnisko {req.DepartureAirportId} nie znalezione.");
            var arrAirport = await _airportRepo.GetByIdAsync(req.ArrivalAirportId)
                               ?? throw new KeyNotFoundException($"Lotnisko {req.ArrivalAirportId} nie znalezione.");
            var retDepAirport = await _airportRepo.GetByIdAsync(req.ReturnDepartureAirportId)
                               ?? throw new KeyNotFoundException($"Lotnisko {req.ReturnDepartureAirportId} nie znalezione.");
            var retArrAirport = await _airportRepo.GetByIdAsync(req.ReturnArrivalAirportId)
                               ?? throw new KeyNotFoundException($"Lotnisko {req.ReturnArrivalAirportId} nie znalezione.");

            var user = new User("PROFILE");

            var trip = _tripFactory.Create(
                user,
                country,
                depAirport,
                arrAirport,
                retDepAirport,
                retArrAirport
            );

            await _tripRepo.AddAsync(trip);

            return new TripDTO
            {
                TripId = trip.Id,
                CountryCode = trip.CountryCode,
                DepartureAirportId = trip.DepartureAirportId,
                ArrivalAirportId = trip.ArrivalAirportId,
                ReturnDepartureAirportId = trip.ReturnDepartureAirportId,
                ReturnArrivalAirportId = trip.ReturnArrivalAirportId,
                DistanceKm = trip.DistanceKm
            };
        }

        public async Task RemoveTripAsync(int tripId)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId)
                   ?? throw new KeyNotFoundException($"Trip {tripId} nie odnaleziony.");
            await _tripRepo.RemoveAsync(trip);
        }

        public async Task<IReadOnlyList<TripDTO>> ListTripsByUserAsync(int userId)
        {
            var trips = await _tripRepo.ListByUserAsync(userId);
            return trips.Select(t => new TripDTO
            {
                TripId = t.Id,
                CountryCode = t.CountryCode,
                DepartureAirportId = t.DepartureAirportId,
                ArrivalAirportId = t.ArrivalAirportId,
                ReturnDepartureAirportId = t.ReturnDepartureAirportId,
                ReturnArrivalAirportId = t.ReturnArrivalAirportId,
                DistanceKm = t.DistanceKm
            }).ToList();
        }

        public async Task<StatisticsDTO> GetStatisticsAsync(int userId)
        {
            var trips = await _tripRepo.ListByUserAsync(userId);

            var visitedCodes = trips.Select(t => t.CountryCode).Distinct().Count();
            var totalCountries = (await _countryRepo.ListAllAsync()).Count;

            var totalKm = trips.Sum(t => t.DistanceKm);
            var percent = totalCountries == 0
                ? 0
                : Math.Round((double)visitedCodes / totalCountries * 100, 1);

            return new StatisticsDTO
            {
                VisitedCount = visitedCodes,
                TotalCountries = totalCountries,
                VisitedPercentage = percent,
                TotalDistanceKm = totalKm
            };
        }
    }
}
