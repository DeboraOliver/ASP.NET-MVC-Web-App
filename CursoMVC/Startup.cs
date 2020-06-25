using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CursoMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); //adiciona as injeções de dependência
            services.AddDbContext<Context>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //midlers
        {
            if (env.IsDevelopment()) //valida se estamos em  ambiente de desenvolvimento
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); //se não tivermosem ambiente de desenvolvimente ele ativa o hsts
            }
            app.UseHttpsRedirection(); //middler para redirect para https se ousuario tentar acessar o site com http
            app.UseStaticFiles();

            app.UseRouting(); //middler que usa routing

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            { //rota default:
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); //id opcional
            });
        }
    }
}
