using Entities.DTO;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class OrdersNoteValidation : AbstractValidator<OrderNotesDto>
    {
        public OrdersNoteValidation()
        {
            RuleFor(x => x.Notes).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Notes).MaximumLength(150).WithMessage("150 Karakterden Fazla Olamaz.");
            RuleFor(x => x.NotDate).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.OrdersId).NotEmpty().WithMessage("Boş Bırakılamaz.");
        }
    }
}
