using Microsoft.EntityFrameworkCore;
using OfferService.ApiReference;
using OfferService.Database.DbContext;
using OfferService.Database.Repositories;
using OfferService.Database.Repositories.Interfaces;
using OfferService.Services;
using OfferService.Services.Interfaces;
using Refit;

namespace OfferService;

public class Startup
{
    private readonly IConfiguration _configuration;
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureControllers(services);
        ConfigureSwagger(services);
        ConfigureScopedServices(services);
        ConfigureDatabaseContext(services);
        ConfigureApiReferences(services);
    }
    
    protected virtual void ConfigureApiReferences(IServiceCollection services)
    {
        var apiReferencesConfiguration = _configuration.GetSection("ApiReferences");
        AddApiReference<IUsersApiReference>(services, apiReferencesConfiguration, "Users");
    }
    
    private static void AddApiReference<TApiReference>(IServiceCollection services,
        IConfiguration apiReferencesConfiguration, string key)
        where TApiReference : class
    {
        services
            .AddRefitClient<TApiReference>()
            .ConfigureHttpClient(client =>
                client.BaseAddress = new Uri(apiReferencesConfiguration.GetValue<string>(key)!));
    }
    
    private static void ConfigureControllers(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        services.AddEndpointsApiExplorer();
    }

    private void ConfigureDatabaseContext(IServiceCollection services)
    {
        services.AddDbContext<OfferDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("OfferDbContext"));
        });
    }

    private static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }
    private static void ConfigureScopedServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddScoped<IOfferService, Services.OfferService>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IRatingRepository, RatingRepository>();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseWebSockets();
        app.UseCors(policyBuilder =>
        {
            policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllers();
            endpoint.MapRazorPages();
        });
    }
    
}