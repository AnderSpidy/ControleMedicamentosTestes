using ControleMedicamentos.Dominio.Compartilhado;
using FluentValidation;
using FluentValidation.Validators;


namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidaFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidaFornecedor() 
        {
            RuleFor(x => x.Nome)
           .NotNull().NotEmpty().MinimumLength(3);

            RuleFor(x => x.Telefone)
            .Telefone();

            RuleFor(x => x.Email)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible).NotNull().NotEmpty();

            RuleFor(x => x.Cidade)
            .NotNull().NotEmpty().MinimumLength(3);

            RuleFor(x => x.Estado)
            .NotNull().NotEmpty().MinimumLength(2);
        }
    }
}
