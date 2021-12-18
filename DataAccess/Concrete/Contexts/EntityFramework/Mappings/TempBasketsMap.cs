using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class TempBasketsMap : IEntityTypeConfiguration<TempBaskets>
    {
        public void Configure(EntityTypeBuilder<TempBaskets> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ProductsId).IsRequired(true);
            builder.Property(x => x.Name).HasMaxLength(150);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Piece).IsRequired(true);            
            builder.Property(x => x.Price).HasColumnType("money");
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.VName).HasMaxLength(50);
            builder.Property(x => x.VName).IsRequired();            
            builder.Property(x => x.CustomersId).IsRequired(true);
            builder.Property(x => x.CookiesID).IsRequired(true);
            builder.ToTable("TempBaskets");
        }
    }
}
