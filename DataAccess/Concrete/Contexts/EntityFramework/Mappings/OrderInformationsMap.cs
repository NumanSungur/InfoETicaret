using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class OrderInformationsMap : IEntityTypeConfiguration<OrderInformations>
    {
        public void Configure(EntityTypeBuilder<OrderInformations> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.Sms).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(150);
            builder.Property(x => x.Message).IsRequired();           
            builder.Property(x => x.InfoDate).IsRequired();
            builder.Property(x => x.OrdersId).IsRequired();

            builder.HasOne<Orders>(o => o.Orders).WithMany(x => x.OrderInformations).HasForeignKey(x => x.OrdersId);

            builder.ToTable("OrderInformations");
        }
    }
}
