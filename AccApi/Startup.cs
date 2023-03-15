using AccApi.Repository;
using AccApi.Repository.GlobalServise;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AccApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            services.AddControllers();


            //services.AddDbContext<AccDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<MasterDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("MasterConnection")));
            services.AddDbContext<PolicyDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("PolicyConnection")));

            services.AddTransient<ISearchRepository, SearchRepository>();
            services.AddTransient<IPackageRepository, PackageRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ISupplierPackagesRepository, SupplierPackagesRepository>();
            services.AddTransient<ISupplierPackagesRevRepository, SupplierPackagesRevRepository>();
            services.AddTransient<IRevisionDetailsRepository, RevisionDetailsRepository>();
            services.AddTransient<IlogonRepository, LogonRepository>();
            services.AddTransient<IConditionsRepository, ConditionsRepository>();
            services.AddTransient<IComparisonGroupRepository, ComparisonGroupRepository>();
            services.AddTransient<ICurrencyConverterRepository, CurrencyConverterRepository>();

            services.AddSingleton<GlobalLists>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AccApi", Version = "v1" });
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AccApi v1"));

  

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
