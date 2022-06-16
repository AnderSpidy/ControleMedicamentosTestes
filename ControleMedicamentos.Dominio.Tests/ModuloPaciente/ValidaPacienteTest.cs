using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class ValidaPacienteTest
    {
        [TestMethod]
        public void Nome_nao_deve_ser_nulo()
        {
            //arrange
            Paciente paciente = new Paciente();
            paciente.Nome = null;
            paciente.CartaoSUS = "99999999999";

            ValidaPaciente validadorPaciente = new ValidaPaciente();

            //action
            var resutadoValidacao = validadorPaciente.Validate(paciente);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Nome_nao_deve_ser_vazio()
        {
            //arrange
            Paciente paciente = new Paciente();
            paciente.Nome = "";
            paciente.CartaoSUS = "99999999999";

            ValidaPaciente validadorPaciente = new ValidaPaciente();

            //action
            var resutadoValidacao = validadorPaciente.Validate(paciente);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Nome_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Paciente paciente = new Paciente();
            paciente.Nome = "Na";
            paciente.CartaoSUS = "99999999999";

            ValidaPaciente validadorPaciente = new ValidaPaciente();

            //action
            var resutadoValidacao = validadorPaciente.Validate(paciente);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void CartaoSus_nao_deve_ser_nulo()
        {
            //arrange
            Paciente paciente = new Paciente();
            paciente.Nome = "Anderson";
            paciente.CartaoSUS = null;

            ValidaPaciente validadorPaciente = new ValidaPaciente();

            //action
            var resutadoValidacao = validadorPaciente.Validate(paciente);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void CartaoSus_nao_deve_ser_vazio()
        {
            //arrange
            Paciente paciente = new Paciente();
            paciente.Nome = "Anderson";
            paciente.CartaoSUS = "";

            ValidaPaciente validadorPaciente = new ValidaPaciente();

            //action
            var resutadoValidacao = validadorPaciente.Validate(paciente);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void CartaoSus_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Paciente paciente = new Paciente();
            paciente.Nome = "Jose";
            paciente.CartaoSUS = "12";

            ValidaPaciente validadorPaciente = new ValidaPaciente();

            //action
            var resutadoValidacao = validadorPaciente.Validate(paciente);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }
    }
}
