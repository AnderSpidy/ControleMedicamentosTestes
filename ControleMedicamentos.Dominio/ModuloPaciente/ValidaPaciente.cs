using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidaPaciente : AbstractValidator<Paciente>
    {
        public ValidaPaciente() 
        {
            RuleFor(x => x.Nome)
           .NotNull().NotEmpty().MinimumLength(3);

            RuleFor(x => x.CartaoSUS)
            .NotNull().NotEmpty().MinimumLength(3);
        }
    }
}
