using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results.ComplexTypes
{
    public enum ResultStatus
    {
        //Değişkenlerin alabileceği değerlerin belli olduğu durumlarda programı daha okunabilir hale getirmek için kullanırız.
        
        Success = 0,
        Error = 1,
        Warning = 2,
        Info = 3
    }
}
