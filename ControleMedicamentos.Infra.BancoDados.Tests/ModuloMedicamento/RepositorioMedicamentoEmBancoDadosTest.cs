using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        public RepositorioMedicamentoEmBancoDadosTest()
        {
            //exclui todo o banco antes de executar os testes 
            Db.ExecutarSql("DELETE FROM TBMedicamento");

            //reseta as chaves primarias 
            Db.ExecutarSql("DBCC CHECKIDENT (TBMedicamento, RESEED, 0 )");
        }
        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            //arrange
                    //instanciando objeto fornecedor para medicamento
            Fornecedor fornecedor = new Fornecedor("Julio", "41998529870", "julinho@gmail.com", "Curitiba", "Parana");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

                    //intanciando medicamento
            var medicamento = new Medicamento("Doril","Tomou a dor Sumiu","abc-235",System.DateTime.Today);
            medicamento.Fornecedor = repositorioFornecedor.SelecionarPorId(fornecedor.Id);
            var repositorio = new RepositorioMedicamentoEmBancoDados();
            //action
            repositorio.Inserir(medicamento);

            //assert
            var medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);

            Assert.AreEqual("Doril", medicamentoEncontrado.Nome);
            Assert.AreEqual("Tomou a dor Sumiu", medicamentoEncontrado.Descricao);
            Assert.AreEqual("abc-235", medicamento.Lote);
            Assert.AreEqual(System.DateTime.Today, medicamentoEncontrado.Validade);
            Assert.AreEqual(fornecedor.Id, medicamento.Fornecedor.Id);
        }
        [TestMethod]
        public void Deve_editar_medicamento()
        {
            //arrange
            //instanciando objeto fornecedor para medicamento
            Fornecedor fornecedor = new Fornecedor("Julio", "41998529870", "julinho@gmail.com", "Curitiba", "Parana");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            //intanciando medicamento
            var medicamento = new Medicamento("Doril", "Tomou a dor Sumiu", "abc-235", System.DateTime.Today);
            medicamento.Fornecedor = repositorioFornecedor.SelecionarPorId(fornecedor.Id);
            var repositorio = new RepositorioMedicamentoEmBancoDados();
            repositorio.Inserir(medicamento);

            ///tem que terminar isso aqui 
            ///agora vou estudar pra tentar nao tirar um zero em compiladores 

        //    //action
        //    paciente.Nome = "Crebinho";
        //    paciente.CartaoSUS = "987654321";
        //    repositorio.Editar(paciente);

        //    //assert
        //    var pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

        //    Assert.IsNotNull(pacienteEncontrado);

        //    Assert.AreEqual("Crebinho", pacienteEncontrado.Nome);
        //    Assert.AreEqual("987654321", pacienteEncontrado.CartaoSUS);
        //}
        //[TestMethod]
        //public void Deve_excluir_paciente()
        //{
        //    //arrange
        //    var paciente = new Paciente("Joselito", "321456987");
        //    var repositorio = new RepositorioPacienteEmBancoDados();
        //    repositorio.Inserir(paciente);



        //    //action

        //    repositorio.Excluir(paciente);

        //    //assert
        //    var pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

        //    Assert.IsNull(pacienteEncontrado);//paciente precisa ser null



        //}
        //[TestMethod]
        //public void Deve_selecionar_apenas_um_paciente()
        //{
        //    //arrange
        //    var paciente = new Paciente("Joselito", "321456987");
        //    var repositorio = new RepositorioPacienteEmBancoDados();
        //    repositorio.Inserir(paciente);

        //    //action
        //    var pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

        //    //assert

        //    Assert.IsNotNull(pacienteEncontrado);

        //    Assert.AreEqual("Joselito", pacienteEncontrado.Nome);
        //    Assert.AreEqual("321456987", pacienteEncontrado.CartaoSUS);
        //}

        //[TestMethod]
        //public void Deve_selecionar_todos_os_paciente()
        //{
        //    //arrange
        //    var paciente1 = new Paciente("Joselito", "321456987");
        //    var paciente2 = new Paciente("Crebinho", "456789123");
        //    var paciente3 = new Paciente("Luizin", "400289222");
        //    var repositorio = new RepositorioPacienteEmBancoDados();
        //    repositorio.Inserir(paciente1);
        //    repositorio.Inserir(paciente2);
        //    repositorio.Inserir(paciente3);


        //    //action
        //    var pacientesEncontrado = repositorio.SelecionarTodos();

        //    //assert

        //    Assert.AreEqual(3, pacientesEncontrado.Count);

        //    Assert.AreEqual("Joselito", pacientesEncontrado[0].Nome);
        //    Assert.AreEqual("Crebinho", pacientesEncontrado[1].Nome);
        //    Assert.AreEqual("Luizin", pacientesEncontrado[2].Nome);

        //}
    }
}
