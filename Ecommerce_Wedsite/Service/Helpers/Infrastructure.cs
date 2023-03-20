using Autofac.Core.Activators.Reflection;
using Autofac;
using System.Collections.Concurrent;
using System.Reflection;

namespace Ecommerce_Wedsite.Service.Helpers
{
    public class Infrastructure : Autofac.Module
    {
        public Infrastructure()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            //builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
        }

        public class AllConstructorFinder : IConstructorFinder
        {
            private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
                new ConcurrentDictionary<Type, ConstructorInfo[]>();

            public ConstructorInfo[] FindConstructors(Type targetType)
            {
                var result = Cache.GetOrAdd(targetType, t => t.GetTypeInfo().DeclaredConstructors.ToArray());
                return result.Length > 0 ? result : throw new NoConstructorsFoundException(targetType);
            }
        }
    }
}
