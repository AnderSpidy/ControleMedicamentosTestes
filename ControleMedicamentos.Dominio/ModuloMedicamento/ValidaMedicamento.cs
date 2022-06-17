using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidaMedicamento : AbstractValidator<Medicamento>
    {
        public ValidaMedicamento() 
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty().MinimumLength(3);

            RuleFor(x => x.Descricao)
            .NotNull().NotEmpty().MinimumLength(3);

            RuleFor(x => x.Lote)
            .NotNull().NotEmpty().MinimumLength(3);

            RuleFor(x => x.Validade)
            .NotNull().NotEmpty();

            

            RuleFor(x => x.Fornecedor)
            .NotNull().NotEmpty();
        }
    }
}
