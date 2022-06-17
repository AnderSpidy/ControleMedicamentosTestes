using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class ValidaFuncionarioTest
    {
        [TestMethod]
        public void Nome_nao_deve_ser_nulo()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = null;
            funcionario.Login = "clebinho@farmacia.com";
            funcionario.Senha = "crebinho*";


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Nome_nao_deve_ser_vazio()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = "";
            funcionario.Login = "clebinho@farmacia.com";
            funcionario.Senha = "crebinho*";


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Nome_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = "cl";
            funcionario.Login = "clebinho@farmacia.com";
            funcionario.Senha = "crebinho*";


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Login_nao_deve_ser_nulo()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = "clebinho";
            funcionario.Login = null;
            funcionario.Senha = "crebinho*";


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Login_nao_deve_ser_vazio()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = "clebinho";
            funcionario.Login = "";
            funcionario.Senha = "crebinho*";


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Login_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = "clebinho";
            funcionario.Login = "as";
            funcionario.Senha = "crebinho*";


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Senha_nao_deve_ser_nulo()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = "clebinho";
            funcionario.Login = "clebinho@farmacia.com";
            funcionario.Senha = null;


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Senha_nao_deve_ser_vazio()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = "clebinho";
            funcionario.Login = "clebinho@farmacia.com";
            funcionario.Senha = "";


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Senha_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Funcionario funcionario = new Funcionario();
            funcionario.Id = 1;
            funcionario.Nome = "clebinho";
            funcionario.Login = "clebinho@farmacia.com";
            funcionario.Senha = "creb";


            ValidaFuncionario validadorPaciente = new ValidaFuncionario();

            //action
            var resutadoValidacao = validadorPaciente.Validate(funcionario);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

    }
}
