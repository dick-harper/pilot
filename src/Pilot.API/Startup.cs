﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Pilot.API
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
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "cookie";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("cookie")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5000/";
                    options.ClientId = "openIdConnectClient";
                    options.SignInScheme = "cookie";
                });

            services.AddMvc();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseAuthentication();
            app.UseAuthentication();
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
