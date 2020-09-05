using Microsoft.Extensions.DependencyInjection;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IContaRepository, ContaRepository>();
        }
    }
}
