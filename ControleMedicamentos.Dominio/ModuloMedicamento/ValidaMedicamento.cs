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
        public ValidaMedicamento() { }
    }
}
