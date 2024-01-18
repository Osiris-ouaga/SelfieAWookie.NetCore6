using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;

namespace SelfieAWookie.NetCore6.ExtensionMethods
{
    public static class DIMethods
    {
        public static void AddInjection(IServiceCollection services) 
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
        }
    }
}
