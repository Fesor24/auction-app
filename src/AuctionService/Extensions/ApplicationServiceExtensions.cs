using AuctionService.Data;
using AuctionService.Interfaces;
using AuctionService.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Reflection;

namespace AuctionService.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        AddMediator(services);

        AddAutoMapper(services);

        AddDataAccess(services, config);

        AddMessageTransport(services, config);

        return services;
    }

    private static void AddMediator(IServiceCollection services) =>
         services.AddMediatR(config =>
         {
             config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
         });

    private static void AddAutoMapper(IServiceCollection services) => 
        services.AddAutoMapper(Assembly.GetExecutingAssembly());


    private static void AddDataAccess(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AuctionDbContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IAuctionRepository, AuctionRepository>();
    }

    private static void AddMessageTransport(IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(opt =>
        {
            //opt.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("", false));

            opt.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}
