using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results.Abstract
{
    //Generic Table = hangi tabloyu verirsem döndürsün
    //Eğer iki veya daha fazla geriye değer döndürmek istiyorsak out parametresini kullanırız.
    public interface IDataResult<out T> :IResult
    {
        //new DataResult<Products>(ResultStatus.Success,products);
        public T Data { get; }
    }
}
