﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ProductCatalog {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();

            var service = Configuration.GetSection("Service");
            services.AddSingleton<IProductProvider>(new ProductProvider(service.GetValue<Uri>("Url"), service.GetValue<string>("ApiToken")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            } else {
                app.UseExceptionHandler();
            }

            var supportedCultures = new List<CultureInfo> { new CultureInfo("en") };
            app.UseRequestLocalization(new RequestLocalizationOptions {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                DefaultRequestCulture = new RequestCulture("en")
            });

            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Products}/{action=Index}");
            });
        }
    }
}
