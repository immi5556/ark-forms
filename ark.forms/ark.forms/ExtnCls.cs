using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace ark.forms
{
    public static class ExtnCls
    {
        public static IServiceCollection AddArkForm(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddApplicationPart(typeof(ExtnCls).Assembly);
            return services;
        }

        public static IApplicationBuilder UseArkForm(this IApplicationBuilder app)
        {
            var assembly = typeof(ExtnCls).Assembly;
            var wwwrootPath = Path.Combine(Path.GetDirectoryName(assembly.Location), "wwwroot");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(wwwrootPath),
                RequestPath = "/ark.forms"
            });

            return app;
        }
    }
}