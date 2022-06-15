using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteEmBancoDadosTest
    {
        public RepositorioPacienteEmBancoDadosTest()
        {
            //exclui todo o banco antes de executar os testes 
            Db.ExecutarSql("DELETE FROM TBPaciente");

            //reseta as chaves primarias 
            Db.ExecutarSql("DBCC CHECKIDENT (TBPaciente, RESEED, 0 )");
        }
        [TestMethod]
        public void Deve_inserir_paciente()
        {
            //arrange
            var paciente = new Paciente("Joselito", "321456987");
            var repositorio = new RepositorioPacienteEmBancoDados();

            //action
            repositorio.Inserir(paciente);

            //assert
            var pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);

            Assert.AreEqual("Joselito", pacienteEncontrado.Nome);
            Assert.AreEqual("321456987", pacienteEncontrado.CartaoSUS);
        }
        [TestMethod]
        public void Deve_editar_paciente()
        {
            //arrange
            var paciente = new Paciente("Joselito", "321456987");
            var repositorio = new RepositorioPacienteEmBancoDados();
            repositorio.Inserir(paciente);



            //action
            paciente.Nome = "Crebinho";
            paciente.CartaoSUS = "987654321";
            repositorio.Editar(paciente);

            //assert
            var pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);

            Assert.AreEqual("Crebinho", pacienteEncontrado.Nome);
            Assert.AreEqual("987654321", pacienteEncontrado.CartaoSUS);
        }
        [TestMethod]
        public void Deve_excluir_paciente()
        {
            //arrange
            var paciente = new Paciente("Joselito", "321456987");
            var repositorio = new RepositorioPacienteEmBancoDados();
            repositorio.Inserir(paciente);



            //action
            
            repositorio.Excluir(paciente);

            //assert
            var pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

            Assert.IsNull(pacienteEncontrado);//paciente precisa ser null

        

        }
        [TestMethod]
        public void Deve_selecionar_apenas_um_paciente()
        {
            //arrange
            var paciente = new Paciente("Joselito", "321456987");
            var repositorio = new RepositorioPacienteEmBancoDados();
            repositorio.Inserir(paciente);

            //action
            var pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

            //assert

            Assert.IsNotNull(pacienteEncontrado);

            Assert.AreEqual("Joselito", pacienteEncontrado.Nome);
            Assert.AreEqual("321456987", pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_paciente()
        {
            //arrange
            var paciente1 = new Paciente("Joselito", "321456987");
            var paciente2 = new Paciente("Crebinho", "456789123");
            var paciente3 = new Paciente("Luizin",   "400289222");
            var repositorio = new RepositorioPacienteEmBancoDados();
            repositorio.Inserir(paciente1);
            repositorio.Inserir(paciente2);
            repositorio.Inserir(paciente3);


            //action
            var pacientesEncontrado = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3,pacientesEncontrado.Count);

            Assert.AreEqual("Joselito", pacientesEncontrado[0].Nome);
            Assert.AreEqual("Crebinho", pacientesEncontrado[1].Nome);
            Assert.AreEqual("Luizin", pacientesEncontrado[2].Nome);

        }
    }
}
