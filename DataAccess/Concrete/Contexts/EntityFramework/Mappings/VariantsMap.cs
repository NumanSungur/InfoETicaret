using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class VariantsMap : IEntityTypeConfiguration<Variants>
    {
        public void Configure(EntityTypeBuilder<Variants> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.GroupName).HasMaxLength(50);
            builder.Property(x => x.GroupName).IsRequired(true);
            builder.Property(x => x.Price).HasColumnType("money");
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.ProductsId).IsRequired(true);

            builder.HasOne<Products>(p => p.Products).WithMany(x => x.Variants).HasForeignKey(x => x.ProductsId);

            builder.ToTable("Variants");
        }
    }
}
