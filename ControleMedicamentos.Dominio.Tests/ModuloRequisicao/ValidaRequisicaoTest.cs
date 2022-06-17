using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class ValidaRequisicaoTest
    {
    

        public Fornecedor gerarFornecedor()
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Cleberson";
            fornecedor.Email = "Clebinho@Fornecedor.com";
            fornecedor.Telefone = "(41)4002-8922";
            fornecedor.Cidade = "Lages";
            fornecedor.Estado = "SC";

            return fornecedor;
        }
        public Medicamento criarMedicamento()
        {
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Descricao = "Tomou doril a dor sumiu.";
            medicamento.Lote = "abc-213";
            medicamento.Validade = new DateTime(2022, 01, 09, 09, 15, 00);
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();
            return medicamento;
        }

       

        public Paciente criarPaciente()
        {
            Paciente paciente = new Paciente();
            paciente.Nome = "Anderson";
            paciente.CartaoSUS = "99999999";

            return paciente;
        }

        public Funcionario criarFuncionario()
        {
            Funcionario funcionario = new Funcionario();
            funcionario.Login = "julio@gmail.com";
            funcionario.Senha = "senhaboa*";
            funcionario.Nome = "Julio";

            return funcionario;
        }

        [TestMethod]
        public void QuantidadeMedicamento_nao_deve_ser_nulo()
        {
            //arrange
            Requisicao requisicao = new Requisicao();
            requisicao.Data = new DateTime(2022, 01, 09, 09, 15, 00);
            requisicao.QtdMedicamento = 0;
            requisicao.Id = 1;
            requisicao.Medicamento = criarMedicamento();
            requisicao.Paciente = criarPaciente();
            requisicao.Funcionario = criarFuncionario();


            ValidaRequisicao validadorPaciente = new ValidaRequisicao();

            //action
            var resutadoValidacao = validadorPaciente.Validate(requisicao);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
       
        public void medicamento_nao_deve_ser_null()
        {
            //arrange
            Requisicao requisicao = new Requisicao();
            requisicao.Data = new DateTime(2022, 01, 09, 09, 15, 00);
            requisicao.QtdMedicamento = 5;
            requisicao.Id = 1;
            requisicao.Medicamento = null;
            requisicao.Paciente = criarPaciente();
            requisicao.Funcionario = criarFuncionario();


            ValidaRequisicao validadorPaciente = new ValidaRequisicao();

            //action
            var resutadoValidacao = validadorPaciente.Validate(requisicao);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

        [TestMethod]
        public void Funcionario_nao_deve_ser_nulo()
        {
            //arrange
            Requisicao requisicao = new Requisicao();
            requisicao.Data = new DateTime(2022, 01, 09, 09, 15, 00);
            requisicao.QtdMedicamento = 0;
            requisicao.Id = 1;
            requisicao.Medicamento = criarMedicamento();
            requisicao.Paciente = criarPaciente();
            requisicao.Funcionario = null;


            ValidaRequisicao validadorPaciente = new ValidaRequisicao();

            //action
            var resutadoValidacao = validadorPaciente.Validate(requisicao);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }
        [TestMethod]
        public void Paciente_nao_deve_ser_nulo()
        {
            //arrange
            Requisicao requisicao = new Requisicao();
            requisicao.Data = new DateTime(2022, 01, 09, 09, 15, 00);
            requisicao.QtdMedicamento = 0;
            requisicao.Id = 1;
            requisicao.Medicamento = criarMedicamento();
            requisicao.Paciente = null;
            requisicao.Funcionario = criarFuncionario();


            ValidaRequisicao validadorPaciente = new ValidaRequisicao();

            //action
            var resutadoValidacao = validadorPaciente.Validate(requisicao);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }
        [TestMethod]
        public void data_nao_deve_ser_minima()
        {
            //arrange
            Requisicao requisicao = new Requisicao();
            requisicao.Data = DateTime.MinValue;
            requisicao.QtdMedicamento = 0;
            requisicao.Id = 1;
            requisicao.Medicamento = criarMedicamento();
            requisicao.Paciente = criarPaciente();
            requisicao.Funcionario = criarFuncionario();


            ValidaRequisicao validadorPaciente = new ValidaRequisicao();

            //action
            var resutadoValidacao = validadorPaciente.Validate(requisicao);

            //assert
            Assert.AreEqual(false, resutadoValidacao.IsValid);
        }

    }
}
