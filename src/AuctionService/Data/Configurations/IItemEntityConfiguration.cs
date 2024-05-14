using AuctionService.Domain;
using AuctionService.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionService.Data.Configurations;

internal sealed class IItemEntityConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable(nameof(Item), "auc");

        builder.OwnsOne(item => item.Dimension)
            .WithOwner();

        builder.Property(p => p.Style)
            .HasConversion(c => c.ToString(),
            v => (Style)Enum.Parse(typeof(Style), v));

        builder.Property(p => p.Medium)
            .HasConversion(c => c.ToString(),
            v => (Medium)Enum.Parse(typeof(Medium), v));
    }
}
