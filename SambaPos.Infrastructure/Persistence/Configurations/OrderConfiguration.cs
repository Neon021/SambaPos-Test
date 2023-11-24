using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SambaPos.Domain.Orders;
using SambaPos.Domain.Orders.ValueObjects;

namespace SambaPos.Infrastructure.Persistence.Configurations;
internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value));

        builder
            .Property(m => m.Name)
            .HasMaxLength(100);

        builder
            .Property(m => m.Description)
            .HasMaxLength(100);

        ConfigureOrderContentsTable(builder);
    }

    private void ConfigureOrderContentsTable(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsMany(m => m.Content, sectionBuilder =>
        {
            sectionBuilder.ToTable("OrderContents");

            sectionBuilder
                .WithOwner()
                .HasForeignKey("OrderId");

            sectionBuilder.HasKey("Id", "OrderId");

            sectionBuilder.Property(s => s.Id)
                .HasColumnName("OrderContentId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => OrderContentId.Create(value));

            sectionBuilder
                .Property(s => s.Name)
                .HasMaxLength(100);

            sectionBuilder
                .Property(s => s.Description)
                .HasMaxLength(100);
        });

        builder.Metadata
            .FindNavigation(nameof(Order.Content))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
