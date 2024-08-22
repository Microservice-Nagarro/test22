using System.Diagnostics.CodeAnalysis;

namespace BHF.MS.MyMicroservice
{
    [ExcludeFromCodeCoverage(Justification = "It's a main Program class")]
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            DependencyInjectionInitializers.AddKeyVaultIntegration(builder);
            DependencyInjectionInitializers.AddOptionsConfiguration(builder);
            DependencyInjectionInitializers.AddCustomServices(builder.Services);

            builder.Services.AddResponseCaching();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            DependencyInjectionInitializers.AddApplicationInsights(builder);
            Database.DependencyInjectionInitializers.AddHealthCheckConfiguration(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseResponseCaching();

            app.MapControllers();
            DependencyInjectionInitializers.MapHealthChecks(app);

            await app.RunAsync();
        }
    }
}