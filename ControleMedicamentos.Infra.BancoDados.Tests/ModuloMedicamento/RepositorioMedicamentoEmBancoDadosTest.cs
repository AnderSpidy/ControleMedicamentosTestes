using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            var medicamento = new Medicamento("Doril", "Tomou a dor Sumiu", "abc-235", System.DateTime.Today);
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



            //action
            medicamento.Nome = "Dorflex";
            medicamento.Descricao = "deixa a dor mais flexivel";
            medicamento.Lote = "bvc-987";
            medicamento.Validade = Convert.ToDateTime("24/06/2030");

            repositorio.Editar(medicamento);

            //assert
            var medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);

            Assert.AreEqual("Dorflex", medicamentoEncontrado.Nome);
            Assert.AreEqual("deixa a dor mais flexivel", medicamentoEncontrado.Descricao);
            Assert.AreEqual("bvc-987", medicamentoEncontrado.Lote);
            Assert.AreEqual(Convert.ToDateTime("24/06/2030"), medicamentoEncontrado.Validade);
        }
        [TestMethod]
        public void Deve_excluir_medicamento()
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

            //action

            repositorio.Excluir(medicamento);

            //assert
            var medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNull(medicamentoEncontrado);//paciente precisa ser null



        }
        [TestMethod]
        public void Deve_selecionar_apenas_um_paciente()
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

            //action
            var medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            //assert

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual("Doril", medicamentoEncontrado.Nome);
            Assert.AreEqual("Tomou a dor Sumiu", medicamentoEncontrado.Descricao);
            Assert.AreEqual("abc-235", medicamento.Lote);
            Assert.AreEqual(System.DateTime.Today, medicamentoEncontrado.Validade);
            Assert.AreEqual(fornecedor.Id, medicamento.Fornecedor.Id);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_fornecedores()
        {
            //arrange
                //instanciando no repositorio os fornecedores
            var fornecedor1 = new Fornecedor("Julio", "41998529870", "julinho@gmail.com", "Curitiba", "Parana");
            var fornecedor2 = new Fornecedor("Carlinhos", "40028922", "carlinhos@gmail.com", "Lages", "Santa Catarina");
            var fornecedor3 = new Fornecedor("Baptista", "4989996523", "baptista@gmail.com", "Porto Alegre", "Rio Grande do Sul");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor1);
            repositorioFornecedor.Inserir(fornecedor2);
            repositorioFornecedor.Inserir(fornecedor3);

            //instanciando os medicamentos no repositorio
            var medicamento1 = new Medicamento("Doril", "Tomou a dor Sumiu", "abc-235", Convert.ToDateTime("24/06/2030"));
            medicamento1.Fornecedor = repositorioFornecedor.SelecionarPorId(fornecedor1.Id);
            var medicamento2 = new Medicamento("Dorflex", "Tomou a dor fica mais flexivel", "hji-321", Convert.ToDateTime("28/12/2036"));
            medicamento2.Fornecedor = repositorioFornecedor.SelecionarPorId(fornecedor2.Id);
            var medicamento3 = new Medicamento("Benegripe", "Tomou voce fica bem da gripe", "qwe-957", Convert.ToDateTime("02/03/2006"));
            medicamento3.Fornecedor = repositorioFornecedor.SelecionarPorId(fornecedor3.Id);
            var repositorio = new RepositorioMedicamentoEmBancoDados();
            
            repositorio.Inserir(medicamento1);
            repositorio.Inserir(medicamento2);
            repositorio.Inserir(medicamento3);


            //action
            var medicamentosEncontrados = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, medicamentosEncontrados.Count);

            Assert.AreEqual("Doril", medicamentosEncontrados[0].Nome);
            Assert.AreEqual("Dorflex", medicamentosEncontrados[1].Nome);
            Assert.AreEqual("Benegripe", medicamentosEncontrados[2].Nome);

        }
    }
    
}
