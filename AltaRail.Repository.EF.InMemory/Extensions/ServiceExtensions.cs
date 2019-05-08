using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AltaRail.Repository.EF.InMemory.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services, string name = "AltaRailDb")
        {
            services.AddDbContext<GenericContext>(options =>
              options.UseInMemoryDatabase(name));
        }
    }
}
