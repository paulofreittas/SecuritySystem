using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SecuritySystem.Repositories.Context;
using SecuritySystem.Repositories.Interfaces;
using SecuritySystem.Repositories.Repositories;
using FluentValidation.AspNetCore;
using Systems = SecuritySystem.Domain.Entities.System;
using SecuritySystem.Domain.Validators;
using FluentValidation;

namespace Application
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
            services.AddDbContext<SSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("local")));

            services.AddTransient<ISystemRepository, SystemRepository>();
            services.AddTransient<IValidator<Systems>, SystemValidator>();

            services.AddCors(Options => Options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddControllers().AddFluentValidation().AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            }).ConfigureApiBehaviorOptions(options => {
                options.InvalidModelStateResponseFactory = c =>
                {
                    return new BadRequestObjectResult(new { errorMessage = c.ModelState.Values.Select(c => c.Errors.FirstOrDefault().ErrorMessage) });
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
