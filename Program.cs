using Microsoft.EntityFrameworkCore;
using music_web_service1.Models.DAL;
using music_web_service1.Models.RabbitMQ;

namespace music_web_service1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddSession();

            var connectionString = builder.Configuration.GetConnectionString("PgSql");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddScoped<DataService>();

            builder.Services.AddScoped<RabbitMQService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Music}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Search",
                    pattern: "{controller=Tracks}/{action=SearchResult}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Index",
                    pattern: "{controller=Account}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Index",
                    pattern: "{controller=SearchHistory}/{action=GetIndex}/{id?}");
            });

            app.Run();
        }
    }
}