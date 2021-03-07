using CalculateShoppingChange.Abstract;
using CalculateShoppingChange.Business.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CalculateShoppingChange.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configuring Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //fluent validation
            //exception handler
            services.AddControllers();
            services.AddScoped<IChangeService, ChangeManager>();
            services.AddCors(options =>
            options.AddDefaultPolicy(builder =>
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddSwaggerDocument(config => {
                config.PostProcess = (doc => {
                    doc.Info.Title = "Shopping Change Calculator Api";
                    doc.Info.Version = "1.1.12";
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Name = "Emre Pelit",
                        Url = "http://www.linkedin.com/in/emrepelit",
                        Email = "emre.pelit@outlook.com"
                    };
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseOpenApi();  //swagger
            app.UseSwaggerUi3();
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // Any origin allowed.
               .AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
