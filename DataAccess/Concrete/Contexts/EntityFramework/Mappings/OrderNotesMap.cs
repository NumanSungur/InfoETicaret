using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class OrderNotesMap : IEntityTypeConfiguration<OrderNotes>
    {
        public void Configure(EntityTypeBuilder<OrderNotes> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();            
            builder.Property(x => x.Notes).HasMaxLength(150);
            builder.Property(x => x.Notes).IsRequired();
            builder.Property(x => x.NotDate).IsRequired();
            builder.Property(x => x.OrdersId).IsRequired();

            builder.HasOne<Orders>(o => o.Orders).WithMany(x => x.OrderNotes).HasForeignKey(x => x.OrdersId);

            builder.ToTable("OrderNotes");
        }
    }
}
