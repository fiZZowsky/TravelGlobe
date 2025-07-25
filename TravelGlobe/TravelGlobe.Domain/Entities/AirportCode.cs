namespace TravelGlobe.Domain.ValueObjects
{
    public sealed class AirportCode : IValueObject
    {
        public string Iata { get; }

        public AirportCode(string iata)
        {
            if (string.IsNullOrWhiteSpace(iata) || iata.Length != 3)
                throw new ArgumentException("Kod IATA musi mieć 3 znaki.", nameof(iata));
            Iata = iata.ToUpperInvariant();
        }

        public override bool Equals(object obj)
            => obj is AirportCode other && Iata == other.Iata;

        public override int GetHashCode() => Iata.GetHashCode();
        public override string ToString() => Iata;
    }
}