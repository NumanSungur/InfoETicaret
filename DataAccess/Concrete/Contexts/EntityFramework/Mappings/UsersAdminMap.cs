using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Contexts.EntityFramework.Mappings
{
    public class UsersAdminMap : IEntityTypeConfiguration<UsersAdmin>
    {
        public void Configure(EntityTypeBuilder<UsersAdmin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.NameSurname).HasMaxLength(50);
            builder.Property(x => x.NameSurname).IsRequired();            
            builder.Property(x => x.Email).HasMaxLength(80);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(15);
            builder.Property(x => x.Phone).IsRequired();            
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Roles).HasMaxLength(25);
            builder.Property(x => x.Roles).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(30);
            builder.Property(x => x.Password).IsRequired();
            builder.ToTable("UserAdmin");
        }
    }
}
