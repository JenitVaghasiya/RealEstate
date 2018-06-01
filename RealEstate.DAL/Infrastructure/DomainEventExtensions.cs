namespace RealEstate
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public static class DomainEventExtensions
    {
        public static async Task DispatchDomainEvents(this DbContext context, IDomainEventDispatcher dispatcher)
        {
            var entitiesWithEvents = context.ChangeTracker.Entries()
                .Where(x => x.Entity is IEntity entity && entity.Events.Any())
                .Select(x => (IEntity)x.Entity)
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var @event in events)
                {
                    await dispatcher.Dispatch(@event);
                }
            }
        }
    }
}
