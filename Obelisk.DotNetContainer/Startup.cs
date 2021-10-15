using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using Monument.Containers;
using Monument.Conventions;
using Monument.DotNetContainer;
using Monument.Factories;
using Monument.Types;
using Monument.Types.OpenGeneric;
using Monument.Types.Generic;
using Monument.Types.Utility;
using Obelisk.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Obelisk.DotNetContainer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            });
 
            var types = typeof(EntryPoint).Assembly.GetTypes()
                .Union(typeof(IService).Assembly.GetTypes())
                .Except(new[] { 
                    typeof(OpenGenericDecorator<>), // Generic Decorators not supported
                    typeof(ClosedGenericDecorator),
                    //typeof(ClosedGenericAdapter),
                    typeof(ClosedGenericNode1), // Closed Generic Implementations of Open Generics not supported
                    typeof(ClosedGenericNode2),
                    typeof(ClosedGenericNode3),
                    typeof(ClosedGenericNode4),
                    typeof(SimpleComposite), // Composites not supported (Mistakingly causes circular dependencies)
                    typeof(SimpleDecorator), // Decorators not supported (Potentially can add support with Scrutor)
                    typeof(ClosedGenericImplementation) // Closed Generic Implementations of Open Generic Interfaces are not supported (Simple Injector does indeed support this)
                });

            // Due to the splitting of IServiceCollection and IServiceProvider, we cannot use Factory Registrations
            new TypePatternRegistrationConvention(new DotNetRegisterTimeContainerAdapter(services)).RegisterTypes(types);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
