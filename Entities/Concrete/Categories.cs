using Core.Entities.Abstract;
using System.Collections.Generic;

namespace Entities.Concrete
{
    //Ientity =>Bu interface si alan sınıflar kesinlikle veritabanı dosyalarıdır.
    public class Categories:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; } //alt kategori ise üst kategorinin ID bilgisini tutacak.
        public bool Status { get; set; }
        public ICollection<Products> Products { get; set; }

        //TblAnaKategori
        //TblAltKategori
    }
}
