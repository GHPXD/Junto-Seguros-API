using Microsoft.AspNetCore.Identity;
using JuntoSegurosAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace JuntoSegurosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public object CharSetBehavior { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            IServiceCollection serviceCollection = services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 21), ServerType.MySql).CharSetBehavior(CharSetBehavior.NeverAppend)));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
