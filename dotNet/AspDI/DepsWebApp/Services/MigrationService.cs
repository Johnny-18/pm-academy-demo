using System;
using System.Threading;
using System.Threading.Tasks;
using DepsWebApp.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DepsWebApp.Services
{
#pragma warning disable 1591
    public class MigrationService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await using var accountManagerContext = scope.ServiceProvider.GetRequiredService<UserContext>();
            await accountManagerContext.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
#pragma warning restore 1591
}