using DevFitness.API.Filters;
using DevFitness.Applictation.Validators;
using DevFitness.Domain.Repositories;
using DevFitness.Infrastructure.Persistence;
using DevFitness.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DevFitness
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
            //Banco de dados - SQL Server
            var connectionString = Configuration.GetConnectionString("DevFitnessSQLServer");
            services.AddDbContext<DevFitnessDbContext>(options => options.UseSqlServer(connectionString));

            //Validações com Fluent Validation
            services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter))) 
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>()); 

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFitness", Version = "v1" });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMealRepository, MealRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFitness v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
