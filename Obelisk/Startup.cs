using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Monument.Conventions;
using Monument.SimpleInjector;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Container = SimpleInjector.Container;

namespace Obelisk
{
    public class Startup
    {
        private readonly Container container = new();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            });

            services.AddSimpleInjector(container, options =>
            {
                // Allows service components to be injected into Api Controllers
                options.AddAspNetCore()
                        .AddControllerActivation();
            });

            var adapter = new ContainerAdapter(container);
            var convention = new TypePatternRegistrationConvention();
            convention.Register(typeof(Monument.Types.Trivial.ITrivialService).Assembly.GetTypes(), adapter);

            services.AddMvcCore();
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

            container.Verify();
        }
    }
}
