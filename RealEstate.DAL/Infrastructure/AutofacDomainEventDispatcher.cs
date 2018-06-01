namespace RealEstate
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;

    public class AutofacDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IComponentContext _context;

        public AutofacDomainEventDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task Dispatch(IDomainEvent @event)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            var collectionType = typeof(IEnumerable<>).MakeGenericType(handlerType);
            var handlers = ((IEnumerable<object>)_context.Resolve(collectionType)).ToList();

            foreach (var handler in handlers)
            {
                var handleMethod = handlerType.GetMethod("Handle");

                var resultTask = (Task)handleMethod.Invoke(handler, new object[] { @event });

                await resultTask;
            }
        }
    }
}
