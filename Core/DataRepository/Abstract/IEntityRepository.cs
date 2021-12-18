using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataRepository.Abstract
{
    //Generic Ttpe => bagımsız tip demektir.Eğer bir sınıfa generic özelliği verilirse o sınıf içerisine istenilen sınıf ve veri tipi gönderilebilir demektir.
    //Sadece Ientity interface sini miras alan sınıfları kabul edecektir.
    //new() => Buraya gelen sınıflar aşagıda türetilebilir demektir.
    public interface IEntityRepository<TEntity> where TEntity:class,IEntity,new()
    {
        TEntity GetByIdFirst(Expression<Func<TEntity, bool>> filter);

        //(x=>x.ıd) linq to  Sql sorgusunu kabul edilebilir hale getiriyoruz.
        // filter==predicate==where => Bunlar rastgele isimdir.
        //Bu metot u kullanırken buraya ilişkileri tabloları listeleme isteği gönderebilirim anlamındadır.
        IList<TEntity> GetAll(Expression<Func<TEntity,bool>> filter = null,params Expression<Func<TEntity,object>>[] includeProperties);
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool Delete(Expression<Func<TEntity, bool>> filter);

    }
}
