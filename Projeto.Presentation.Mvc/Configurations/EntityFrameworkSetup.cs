using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projeto.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Configurations
{
    public static class EntityFrameworkSetup
    {
        public static void AddEntityFramework(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>
                (options => options.UseSqlServer
                (configuration.GetConnectionString("ProjetoDashboard")));
        }
    }
}
