using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projeto.Presentation.Mvc.Configurations;

namespace Projeto.Presentation.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //habilitar o MVC
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //configurando o entityframework
            EntityFrameworkSetup.AddEntityFramework(services, Configuration);

            //configurando a injeção de dependência
            DependencyInjectionSetup.AddDependencyInjection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                        
            app.UseStaticFiles(); //mapeamento para a pasta wwwroot
            app.UseRouting(); //navegação baseada em rotas (MVC)

            //mapeamento da rota inicial do projeto
            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllerRoute(
                        name: "default", //define o padrão de navegação
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                        );
                });
        }
    }
}
