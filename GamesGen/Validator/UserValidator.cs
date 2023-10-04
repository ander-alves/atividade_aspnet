using FluentValidation;
using GamesGen.Model;

namespace GamesGen.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(u => u.Usuario)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(u => u.Foto)
                .MaximumLength(5000);

            RuleFor(u => u.Idade)
           .GreaterThanOrEqualTo(18)
           .WithMessage("O usuário deve ter pelo menos 18 anos de idade.");
        }
    }

}
