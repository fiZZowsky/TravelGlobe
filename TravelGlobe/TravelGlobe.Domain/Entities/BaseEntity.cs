namespace TravelGlobe.Domain
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        private List<INotification>? _domainEvents;

        public IReadOnlyCollection<INotification>? DomainEvents
            => _domainEvents?.AsReadOnly();

        protected Entity() { }

        protected Entity(TId id) => Id = id;

        protected void RaiseDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public override bool Equals(object obj)
            => obj is Entity<TId> other && EqualityComparer<TId>.Default.Equals(Id, other.Id);

        public override int GetHashCode() => HashCode.Combine(Id);
    }
}
