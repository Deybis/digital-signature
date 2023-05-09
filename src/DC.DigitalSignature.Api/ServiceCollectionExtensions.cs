using DC.DigitalSignature.Core;
using DC.DigitalSignature.Core.Service;

namespace dc.digitalsignature.api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IFileService), typeof(FileService));
            services.AddScoped<FileService>();
        }
    }
}
