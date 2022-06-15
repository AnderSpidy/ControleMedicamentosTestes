using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        public RepositorioFuncionarioEmBancoDadosTest()
        {
            //exclui todo o banco antes de executar os testes 
            Db.ExecutarSql("DELETE FROM TBFuncionario");

            //reseta as chaves primarias 
            Db.ExecutarSql("DBCC CHECKIDENT (TBFuncionario, RESEED, 0 )");
        }


        [TestMethod]
        public void Deve_inserir_funcionario()
        {
            //arrange

            Funcionario novoFuncionario = new Funcionario("Amanda","amanda1996@farmacia.com","amandinha10*");

            var repositorio = new RepositorioFuncionarioEmBancoDados();

            //action
            repositorio.Inserir(novoFuncionario);

            //assert
            Funcionario funcionarioEncontrado = repositorio.SelecionarPorId(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual("Amanda", funcionarioEncontrado.Nome);
            Assert.AreEqual("amanda1996@farmacia.com", funcionarioEncontrado.Login);
            Assert.AreEqual("amandinha10*", funcionarioEncontrado.Senha);
            
        }

        [TestMethod]
        public void Deve_editar_funcionario()
        {
            //arrange
            Funcionario funcionarioEditado = new Funcionario("Amanda", "amanda1996@farmacia.com", "amandinha10*");

            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(funcionarioEditado);


            //action
            funcionarioEditado.Nome = "Cleberson";
            funcionarioEditado.Login = "cleberson2001@farmacia.com";
            funcionarioEditado.Senha = "brabinho25*";
            repositorio.Editar(funcionarioEditado);

            // assert
            Funcionario funcionarioEncontrado = repositorio.SelecionarPorId(funcionarioEditado.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual("Cleberson", funcionarioEncontrado.Nome);
            Assert.AreEqual("cleberson2001@farmacia.com", funcionarioEncontrado.Login);
            Assert.AreEqual("brabinho25*", funcionarioEncontrado.Senha);
   
        }
        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            //arrange
            Funcionario funcionario = new Funcionario("Amanda", "amanda1996@farmacia.com", "amandinha10*");

            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(funcionario);

            //action

            repositorio.Excluir(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNull(funcionarioEncontrado);//paciente precisa ser null

        }
        [TestMethod]
        public void Deve_selecionar_apenas_um_funcionario()
        {
            //arrange
            Funcionario funcionario = new Funcionario("Amanda", "amanda1996@farmacia.com", "amandinha10*");

            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(funcionario);


            //action
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            //assert

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual("Amanda", funcionarioEncontrado.Nome);
            Assert.AreEqual("amanda1996@farmacia.com", funcionarioEncontrado.Login);
            Assert.AreEqual("amandinha10*", funcionarioEncontrado.Senha);
            
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_funcionarios()
        {
            //arrange
            var funcionario1 = new Funcionario("Amanda", "amanda1996@farmacia.com", "amandinha10*");
            var funcionario2 = new Funcionario("Cleberson", "cleberson2001@farmacia.com", "crebs25*");
            var funcionario3 = new Funcionario("Paulo", "paulo1999@farmacia.com", "paulin99*");
            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(funcionario1);
            repositorio.Inserir(funcionario2);
            repositorio.Inserir(funcionario3);


            //action
            var funcionarioEncontrados = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, funcionarioEncontrados.Count);

            Assert.AreEqual("Amanda", funcionarioEncontrados[0].Nome);
            Assert.AreEqual("Cleberson", funcionarioEncontrados[1].Nome);
            Assert.AreEqual("Paulo", funcionarioEncontrados[2].Nome);

        }
    }
}
