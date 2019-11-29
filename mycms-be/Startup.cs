using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mycms.Data;
using mycms.Data.Infrastructure;
using mycms.Models.ApplicationServices;
using StackExchange.Redis;

namespace mycms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyCmsDbContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));
            services.AddTransient<IArticlesApplicationService, ArticlesApplicationService>();
            
            var conn = Configuration.GetValue<string>("AppSettings:ServiceBusConnectionString");
            
            services.AddSingleton<ITopicClient>(
                new TopicClient(
                    Configuration.GetValue<string>("AppSettings:ServiceBusConnectionString"),
                    Configuration.GetValue<string>("AppSettings:TopicName")
                    ));

            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                    pattern: "{controller=Articles}/{action=Index}/{id?}");
            });
        }
    }
}
