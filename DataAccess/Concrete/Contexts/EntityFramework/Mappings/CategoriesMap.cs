using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Contexts.EntityFramework
{
    public class CategoriesMap : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(180);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ParentId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.ToTable("Categories");

            //Ön tanımlı veri eklemek istiyorsak : veritabanı oluşturulurken içerisine otomatik veri atma işlemi
            builder.HasData(
                new Categories { Id=1 , Name = "Teknoloji", ParentId=0, Status=true},
                new Categories { Id=2 ,  Name = "Ev Aletleri", ParentId = 0, Status = true }
                );
        }
    }
}
