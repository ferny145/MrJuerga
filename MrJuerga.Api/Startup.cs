﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrJuerga.Repository;
using MrJuerga.Repository.dbcontext;
using MrJuerga.Repository.implementation;
using MrJuerga.Service;
using MrJuerga.Service.implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MrJuerga.Api
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

            
            services.AddDbContext<ApplicationDbContext>(options =>            
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddTransient<IProductoService, ProductoService> ();

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioService, UsuarioService> ();
            
            services.AddTransient<IBoletaRepository, BoletaRepository>();
            services.AddTransient<IBoletaService, BoletaService> ();

            services.AddTransient<IPaqueteRepository, PaqueteRepository>();
            services.AddTransient<IPaqueteService, PaqueteService> ();

             services.AddTransient<IProductoPaqueteRepository,ProductoPaqueteRepository>();
            services.AddTransient<IProductoPaqueteService, ProductoPaqueteService> ();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

             services.AddCors (options => {
                options.AddPolicy ("Todos",
                    builder => builder.WithOrigins ("*").WithHeaders ("*").WithMethods ("*"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseCors ("Todos");
            //app.UseHttpsRedirection();
            app.UseMvc ();
        }
    }
}
