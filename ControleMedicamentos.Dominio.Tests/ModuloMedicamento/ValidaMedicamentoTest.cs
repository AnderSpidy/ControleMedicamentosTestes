
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class ValidaMedicamentoTest
    {
        private Fornecedor gerarFornecedor()
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Id = 1;
            fornecedor.Nome = "Fornecedor";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";
            return fornecedor;
        }
        
        
        [TestMethod]
        public void Nome_nao_deve_ser_nulo()
        {
            //arrange
            
            
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = null;
            medicamento.Lote = "abc-123";
            medicamento.Descricao = "Tomou a dor Sumiu";
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Nome_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "fo";
            medicamento.Lote = "abc-123";
            medicamento.Descricao = "Tomou a dor Sumiu";
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
        [TestMethod]
        public void Nome_nao_pode_ser_vazio()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "";
            medicamento.Lote = "abc-123";
            medicamento.Descricao = "Tomou a dor Sumiu";
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
        [TestMethod]
        public void Descricao_nao_deve_ser_nulo()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Lote = "abc-123";
            medicamento.Descricao = null;
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Descricao_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Lote = "abc-123";
            medicamento.Descricao = "To";
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
        [TestMethod]
        public void Descricao_nao_pode_ser_vazio()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Lote = "abc-123";
            medicamento.Descricao = "";
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
        [TestMethod]
        public void Lote_nao_deve_ser_nulo()
        {
            //arrange


            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Lote = null;
            medicamento.Descricao = "Tomou a dor Sumiu";
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Lote_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Lote = "a";
            medicamento.Descricao = "Tomou a dor Sumiu";
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
        [TestMethod]
        public void Lote_nao_pode_ser_vazio()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Lote = "";
            medicamento.Descricao = "Tomou a dor Sumiu";
            medicamento.Validade = Convert.ToDateTime("25/06/2030");
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
        [TestMethod]
        public void Validade_nao_pode_ser_nulo()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Lote = "abc-123";
            medicamento.Descricao = "Tomou a dor Sumiu";
            medicamento.Validade = DateTime.MinValue;
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
        [TestMethod]
        public void Fornecedor_nao_deve_ser_vazio()
        {
            //arrange
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Lote = "abc-123";
            medicamento.Descricao = "Tomou a dor Sumiu";
           
            medicamento.Validade = new DateTime(2030, 02, 25);
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = null;

            ValidaMedicamento validaMedicamento = new ValidaMedicamento();

            //action
            var resultadoValidacao = validaMedicamento.Validate(medicamento);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
    }
}
