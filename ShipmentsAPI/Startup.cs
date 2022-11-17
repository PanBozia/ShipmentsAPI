using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShipmentsAPI.EFDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShipmentsAPI.Services;
using ShipmentsAPI.Middleware;

namespace ShipmentsAPI
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
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddDbContext<ShipmentsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ShipmentsDbConnection")));
            services.AddScoped<ShipmentsDataSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IForwarderService, ForwarderService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IWarehouseAreaService, WarehouseAreaService >();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddHttpContextAccessor();

            services.AddScoped<ErrorHandlingMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShipmentsDataSeeder seeder)
        {
            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShipmentsAPI");
            });

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
