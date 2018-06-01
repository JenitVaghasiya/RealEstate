namespace RealEstate
{
    using System.Threading.Tasks;

    public interface IDomainEventHandler<in TEvent>
        where TEvent : IDomainEvent
    {
        Task Handle(TEvent @event);
    }
}
