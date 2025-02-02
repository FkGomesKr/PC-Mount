namespace PCMount.Services;

using PCMount.Services.Database;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDBServices(this IServiceCollection services) {
        services.AddScoped<OrdersService>();
        services.AddScoped<ComputersService>();
        services.AddScoped<ComponentesService>();
        services.AddScoped<UsersService>();
        services.AddScoped<InventarioService>();
        return services;
    }
}