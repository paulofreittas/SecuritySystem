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
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

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
            // Injeta o contexto para ser usado em qualquer ponto da aplicação, passando a connection string
            // SQLServer
            // services.AddDbContext<SSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("local")));
            // MySQL
            services.AddDbContext<SSContext>(options => options.UseMySql(Configuration.GetConnectionString("local")));

            // Injeta o repositório e o Validator
            services.AddTransient<ISystemRepository, SystemRepository>();
            services.AddTransient<IValidator<Systems>, SystemValidator>();

            // Faz a liberação do CORS para que não tenha problema no navegador
            services.AddCors(Options => Options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            // Configuração para não retornar os atributos nulos no json, assim econimizando em tráfego.
            // Logo em seguida configura o retorno de BadRequest com o erro validado pelo Fluent Validador quando receber informações inconsistentes do modelo
            services.AddControllers().AddFluentValidation().AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            }).ConfigureApiBehaviorOptions(options => {
                options.InvalidModelStateResponseFactory = c =>
                {
                    return new BadRequestObjectResult(new { errorMessage = c.ModelState.Values.Select(c => c.Errors.FirstOrDefault().ErrorMessage) });
                };
            });

            // Registro do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Security Systems",
                        Version = "v1",
                        Description = "Caso de uso manter sistemas",
                        Contact = new OpenApiContact
                        {
                            Name = "Paulo Henrique de Freitas",
                            Url = new Uri("http://github.com/paulofreittas")
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Security Systems v1.0");
            });

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
