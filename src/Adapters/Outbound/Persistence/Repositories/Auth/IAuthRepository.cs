using Inferno.src.Core.Domain.Models;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories.Auth;

public interface IAuthRepository
{
    Task<Client> GetClientByIdAsync(string clientId);
    Task<Entity.Demon> GetDemonByEmailAsync(string email);
    Task<SigninKey> GetActiveSigningKeyAsync();
    Task<bool> ValidateClientIdAsync(string clientId);
    Task<bool> ValidateDemonAsync(Entity.Demon demon);
    Task<Entity.Demon> RegisterAsync(Entity.Demon demon);
    public Task<bool> ExistsAsync(string demonEmail);
}
