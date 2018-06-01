namespace RealEstate
{
    using System.Collections.Generic;

    public interface IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }
}
