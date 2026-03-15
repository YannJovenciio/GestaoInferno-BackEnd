using Inferno.src.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inferno.src.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasData(
            new Client
            {
                Id = 1,
                ClientId = "Client1",
                Name = "Client Application 1",
                ClientURL = "https://client1.com",
            },
            new Client
            {
                Id = 2,
                ClientId = "Client2",
                Name = "Client Application 2",
                ClientURL = "https://client2.com",
            }
        );
    }
}
