using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest
    {
        public RepositorioFornecedorEmBancoDadosTest()
        {
            //exclui todo o banco antes de executar os testes 
            Db.ExecutarSql("DELETE FROM TBFornecedor");

            //reseta as chaves primarias 
            Db.ExecutarSql("DBCC CHECKIDENT (TBFornecedor, RESEED, 0 )");
        }


        [TestMethod]
        public void Deve_inserir_fornecedor()
        {
            //arrange
            Fornecedor novoFornecedor = new Fornecedor("Julio","41998529870","julinho@gmail.com","Curitiba","Parana");

            var repositorio = new RepositorioFornecedorEmBancoDados();

            //action
            repositorio.Inserir(novoFornecedor);

            //assert
            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorId(novoFornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual("Julio", fornecedorEncontrado.Nome);
            Assert.AreEqual("Parana", fornecedorEncontrado.Estado);
            Assert.AreEqual("Curitiba", fornecedorEncontrado.Cidade);
            Assert.AreEqual("julinho@gmail.com", fornecedorEncontrado.Email);
            Assert.AreEqual("41998529870", fornecedorEncontrado.Telefone);
        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            //arrange
            Fornecedor fornecedorEditado = new Fornecedor("Julio", "41998529870", "julinho@gmail.com", "Curitiba", "Parana");

            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(fornecedorEditado);


            //action
            fornecedorEditado.Nome = "Crebinho";
            fornecedorEditado.Telefone = "40028922";
            fornecedorEditado.Email = "crebs@gmail.com";
            fornecedorEditado.Cidade = "Lages";
            fornecedorEditado.Estado = "Santa Catarina";
            repositorio.Editar(fornecedorEditado);

            // assert
            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorId(fornecedorEditado.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual("Crebinho", fornecedorEncontrado.Nome);
            Assert.AreEqual("Santa Catarina", fornecedorEncontrado.Estado);
            Assert.AreEqual("Lages", fornecedorEncontrado.Cidade);
            Assert.AreEqual("crebs@gmail.com", fornecedorEncontrado.Email);
            Assert.AreEqual("40028922", fornecedorEncontrado.Telefone);
        }
        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor("Julio", "41998529870", "julinho@gmail.com", "Curitiba", "Parana");

            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(fornecedor);



            //action

            repositorio.Excluir(fornecedor);

            //assert
            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            Assert.IsNull(fornecedorEncontrado);//paciente precisa ser null



        }
        [TestMethod]
        public void Deve_selecionar_apenas_um_fornecedor()
        {
            //arrange
            Fornecedor fornecedor = new Fornecedor("Julio", "41998529870", "julinho@gmail.com", "Curitiba", "Parana");

            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(fornecedor);


            //action
            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            //assert

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual("Julio", fornecedorEncontrado.Nome);
            Assert.AreEqual("Parana", fornecedorEncontrado.Estado);
            Assert.AreEqual("Curitiba", fornecedorEncontrado.Cidade);
            Assert.AreEqual("julinho@gmail.com", fornecedorEncontrado.Email);
            Assert.AreEqual("41998529870", fornecedorEncontrado.Telefone);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_fornecedores()
        {
            //arrange
            var paciente1 = new Fornecedor("Julio", "41998529870", "julinho@gmail.com", "Curitiba", "Parana");
            var paciente2 = new Fornecedor("Carlinhos", "40028922", "carlinhos@gmail.com", "Lages", "Santa Catarina");
            var paciente3 = new Fornecedor("Baptista", "4989996523", "baptista@gmail.com", "Porto Alegre", "Rio Grande do Sul");
            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(paciente1);
            repositorio.Inserir(paciente2);
            repositorio.Inserir(paciente3);


            //action
            var pacientesEncontrado = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, pacientesEncontrado.Count);

            Assert.AreEqual("Julio", pacientesEncontrado[0].Nome);
            Assert.AreEqual("Carlinhos", pacientesEncontrado[1].Nome);
            Assert.AreEqual("Baptista", pacientesEncontrado[2].Nome);

        }
    }
}
