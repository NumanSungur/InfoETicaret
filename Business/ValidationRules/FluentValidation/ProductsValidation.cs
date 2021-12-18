using Entities.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductsValidation : AbstractValidator<ProductsUpdateDto>
    {
        public ProductsValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Name).MaximumLength(150).WithMessage("150 Karakterden Fazla Olamaz.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Price).Must(NumberControl).WithMessage("Fiyat Bilgisi Doğru Girilmedi.");
            RuleFor(x => x.Discount).NotEmpty().When(c=>c.Discount >= 0).WithMessage("0 veya 0'dan Büyük Değer Giriniz.");
            RuleFor(x => x.Keywords).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Keywords).MaximumLength(180).WithMessage("180 Karakterden Fazla Olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Description).MaximumLength(180).WithMessage("180 Karakterden Fazla Olamaz.");
            RuleFor(x => x.MainImages).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Boş Bırakılamaz.");            
            RuleFor(x => x.CategoriesId).NotEmpty().WithMessage("Boş Bırakılamaz.");
        }
        //Overload = Metot aşırı yüklenmesi
        private bool NumberControl(decimal data)
        {
            Regex regex = new Regex(@"\d{1,20}(\.\d{1,2})?");
            return regex.IsMatch(data.ToString());
        }
        private bool NumberControl(int data)
        {
            Regex regex = new Regex(@"^[0-9]{10}$");
            return regex.IsMatch(data.ToString());
        }
    }
}
