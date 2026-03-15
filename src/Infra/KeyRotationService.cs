using System.Security.Cryptography;
using Inferno.src.Adapters.Outbound.Persistence.Repositories;
using Inferno.src.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Inferno.src.Infra
{
    public class KeyRotationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly TimeSpan _rotationInterval = TimeSpan.FromDays(7);

        public KeyRotationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // This method is executed when the background service starts.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await RotateKeysAsync();
                await Task.Delay(_rotationInterval, stoppingToken);
            }
        }

        private async Task RotateKeysAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HellDbContext>();
            var activeKey = await context.SigninKeys.FirstOrDefaultAsync(k => k.IsActive);
            if (activeKey == null || activeKey.ExpiresAt <= DateTime.UtcNow.AddDays(10))
            {
                if (activeKey != null)
                {
                    activeKey.IsActive = false;
                    context.SigninKeys.Update(activeKey);
                }
                using var rsa = RSA.Create(2048);
                var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                var newKeyId = Guid.NewGuid().ToString();
                var newKey = new SigninKey
                {
                    KeyId = newKeyId,
                    PrivateKey = privateKey,
                    PublicKey = publicKey,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddYears(1),
                };
                await context.SigninKeys.AddAsync(newKey);
                await context.SaveChangesAsync();
            }
        }
    }
}
