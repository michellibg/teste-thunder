using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using System.Reflection;

namespace Thunders.TechTest.OutOfBox.Queues
{
    public static class RebusServiceCollectionExtensions
    {
        public static IServiceCollection AddBus(
            this IServiceCollection services, 
            IConfiguration configuration, 
            SubscriptionBuilder? subscriptionBuilder = null)
        {
            services.AutoRegisterHandlersFromAssembly(Assembly.GetEntryAssembly());

            return services;
        }
    }

    public class SubscriptionBuilder
    {
        internal List<Type> TypesToSubscribe { get; private set; } = [];

        public SubscriptionBuilder Add<T>()
        {
            TypesToSubscribe.Add(typeof(T));

            return this;
        }
    }
}
