namespace RealEstate
{
    using Autofac;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacDomainEventDispatcher>().As<IDomainEventDispatcher>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IDomainEventHandler<>))
                .AsImplementedInterfaces();
        }
    }
}
