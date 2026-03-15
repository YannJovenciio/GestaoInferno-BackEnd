using Inferno.src.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly HellDbContext _context;

    public AuthRepository(HellDbContext context)
    {
        _context = context;
    }

    public async Task<Client> GetClientByIdAsync(string clientId)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId)
            ?? throw new InvalidOperationException("Invalid client credentials.");
    }

    public async Task<Entity.Demon> GetDemonByEmailAsync(string email)
    {
        return await _context.Demons.FirstOrDefaultAsync(d =>
                d.DemonEmail.ToLower() == email.ToLower()
            ) ?? throw new InvalidOperationException("Invalid demon credentials.");
    }

    public async Task<SigninKey> GetActiveSigningKeyAsync()
    {
        return await _context.SigninKeys.FirstOrDefaultAsync(k => k.IsActive)
            ?? throw new InvalidOperationException("Invalid signing key credentials.");
    }

    public async Task<bool> ValidateClientIdAsync(string clientId)
    {
        var client = await GetClientByIdAsync(clientId);
        return client != null;
    }

    public async Task<bool> ValidateDemonAsync(Entity.Demon demon)
    {
        return await _context.Demons.AnyAsync(d => d.IdDemon == demon.IdDemon);
    }

    public async Task<Entity.Demon> RegisterAsync(Entity.Demon demon)
    {
        await _context.Demons.AddAsync(demon);
        await _context.SaveChangesAsync();
        return demon;
    }

    public Task<bool> ExistsAsync(string demonEmail)
    {
        return _context.Demons.AnyAsync(d => d.DemonEmail.ToLower() == demonEmail.ToLower());
    }
}
