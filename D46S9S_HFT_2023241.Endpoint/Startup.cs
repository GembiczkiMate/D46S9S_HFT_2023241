using Castle.Core.Configuration;
using D46S9S_HFT_2023241.Endpoint.Services;
using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using D46S9S_HFT_2023241.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Endpoint
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<OrderDB>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();

            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IProductLogic, ProductLogic>();
            services.AddTransient<IUserLogic, UserLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "D46S9S_HFT_2023241.Endpoint", Version = "v1" });
            });


        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "D46S9S_HFT_2023241.Endpoint v1"));
            }



            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseCors(x => x
             .AllowCredentials()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .WithOrigins("http://localhost:18844"));

            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");







            });
        }

    }
}
