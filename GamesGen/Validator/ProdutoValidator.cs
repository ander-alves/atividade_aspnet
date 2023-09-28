using FluentValidation;
using GamesGen.Model;

namespace blogPessoal.Validator
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .MinimumLength(05)
                .MaximumLength(100);

            RuleFor(p => p.Descricao)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(1000);
        }
    }
}
