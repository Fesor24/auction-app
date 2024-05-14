using AuctionService.Domain;
using AuctionService.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionService.Data.Configurations;

internal sealed class IAuctionEntityTypeConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        builder.ToTable(nameof(Auction), "auc");

        builder.Property(p => p.Status)
            .HasConversion(c => c.ToString(),
            v => (Status)Enum.Parse(typeof(Status), v));
    }
}
