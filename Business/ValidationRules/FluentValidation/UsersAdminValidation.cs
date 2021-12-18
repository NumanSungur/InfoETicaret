using Entities.DTO;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UsersAdminValidation : AbstractValidator<UsersAdminDto>
    {
        public UsersAdminValidation()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.NameSurname).MaximumLength(50).WithMessage("50 Karakterden Fazla Olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Email).MaximumLength(80).WithMessage("80 Karakterden Fazla Olamaz.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Phone).MaximumLength(15).WithMessage("15 Karakterden Fazla Olamaz.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Roles).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Roles).MaximumLength(25).WithMessage("25 Karakterden Fazla Olamaz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Boş Bırakılamaz.");
            RuleFor(x => x.Password).MaximumLength(30).WithMessage("30 Karakterden Fazla Olamaz.");
        }
    }
}
