using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class ValidaFornecedorTest
    {
        [TestMethod]
        public void Nome_nao_deve_ser_nulo()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = null;
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Nome_nao_deve_ser_vazio()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Nome_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "No";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Telefone_nao_deve_ser_nulo()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Anderson";
            fornecedor.Telefone = null;
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Telefone_nao_deve_ser_vazio()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Fornecedor";
            fornecedor.Telefone = "";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Telefone_deve_ter_no_minimo_9_caracteres()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Fornecedor";
            fornecedor.Telefone = "41";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

       

        [TestMethod]
        public void Email_nao_deve_ser_nulo()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Fornecedor";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = null;
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Email_nao_deve_ser_vazio()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Fornecedor";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

       
        [TestMethod]
        public void Cidade_nao_deve_ser_nulo()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Fornecedor";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = null;
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Cidade_nao_deve_ser_vazio()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Fornecedor";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "";
            fornecedor.Estado = "PR";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Cidade_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Anderson";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "C";
            fornecedor.Estado = "PR";
            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Estado_nao_deve_ser_nulo()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Fornecedor";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = null;

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Estado_nao_deve_ser_vazio()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Nome";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }

        [TestMethod]
        public void Nome_deve_ter_no_minimo_2_caracteres()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Nome";
            fornecedor.Telefone = "41998529870";
            fornecedor.Email = "fornecedor@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "S";

            ValidaFornecedor validadorFornecedor = new ValidaFornecedor();

            //action
            var resultadoValidacao = validadorFornecedor.Validate(fornecedor);

            //assert
            Assert.AreEqual(false, resultadoValidacao.IsValid);
        }
    }
}
