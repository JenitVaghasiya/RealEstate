namespace RealEstate
{
    using System.Threading.Tasks;

    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent @event);
    }
}
