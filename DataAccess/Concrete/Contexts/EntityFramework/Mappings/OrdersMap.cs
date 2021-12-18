using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class OrdersMap : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasKey(x => x.Id);            
            builder.Property(x => x.CustomersId).IsRequired();
            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.CargoNumber).HasMaxLength(50);
            builder.Property(x => x.CargoNumber).IsRequired();            
            builder.Property(x => x.CargoPrice).HasColumnType("money");
            builder.Property(x => x.CargoPrice).IsRequired();
            builder.Property(x => x.OrderStatus).HasMaxLength(30);
            builder.Property(x => x.OrderStatus).IsRequired();
            builder.Property(x => x.TotalPrice).HasColumnType("money");
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.Kdv).IsRequired();
            builder.Property(x => x.TotalDiscount).HasColumnType("money");
            builder.Property(x => x.TotalDiscount).IsRequired();
            builder.Property(x => x.CouponPrice).HasColumnType("money");
            builder.Property(x => x.CouponPrice).IsRequired();

            builder.HasOne<Customers>(p => p.Customers).WithMany(x => x.Orders).HasForeignKey(x => x.CustomersId);           

            builder.ToTable("Orders");
        }
    }
}
