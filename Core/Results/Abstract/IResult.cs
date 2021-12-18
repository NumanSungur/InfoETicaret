using Core.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results.Abstract
{
    public interface IResult
    {
        //Ekle sil güncelle
        public ResultStatus ResultStatus { get; }
        public string Message { get; } // Kullanıcıya verilecek cevap
        public Exception Exception { get; } // log kaydı
    }
}
