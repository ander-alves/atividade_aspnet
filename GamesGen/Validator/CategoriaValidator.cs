using FluentValidation;
using GamesGen.Model;

namespace GamesGen.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator() {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);
            
        }
    }
}
