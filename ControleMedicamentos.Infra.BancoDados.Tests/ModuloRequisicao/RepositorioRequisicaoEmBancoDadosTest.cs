using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioPacienteEmBancoDadosTest
    {
        private Requisicao requisicao;
        private RepositorioRequisicaoEmBancoDados repositorioRequisicao;
        private Medicamento medicamento;
        private RepositorioMedicamentoEmBancoDados repositorioMedicamento;
        private Fornecedor fornecedor;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;
        private Funcionario funcionario;
        private RepositorioFuncionarioEmBancoDados repositorioFuncionario;
        private Paciente paciente;
        private RepositorioPacienteEmBancoDados repositorioPaciente;

        public RepositorioPacienteEmBancoDadosTest()
        {
            Db.ExecutarSql(@"DELETE FROM TBREQUISICAO;
                  DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)

                  DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)

                  DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)

                  DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");

            medicamento = gerarMedicamento();
            fornecedor = gerarFornecedor();
            paciente = gerarPaciente();
            funcionario = gerarFuncionario();
            requisicao = gerarRequisicao();

            medicamento.Fornecedor = fornecedor;
            requisicao.Medicamento = medicamento;
            requisicao.Funcionario = funcionario;
            requisicao.Paciente = paciente;

            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

        }

        public Requisicao gerarRequisicao()
        {
            Requisicao requisicao = new Requisicao();
            requisicao.Data = new DateTime(2022, 01, 09, 09, 15, 00);
            requisicao.QtdMedicamento = 2;

            return requisicao;
        }

        public Medicamento gerarMedicamento()
        {
            Medicamento medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Descricao = "Tomou doril a dor sumiu.";
            medicamento.Lote = "231AS1";
            medicamento.Validade = new DateTime(2022, 01, 09, 09, 15, 00);
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = gerarFornecedor();
            return medicamento;
        }

        public Fornecedor gerarFornecedor()
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = "Rogerio";
            fornecedor.Email = "RogerinDoYoutube@gmail.com";
            fornecedor.Telefone = "4002-8922";
            fornecedor.Cidade = "Lages";
            fornecedor.Estado = "SC";
            
            return fornecedor;
        }

        public Paciente gerarPaciente()
        {
            Paciente paciente = new Paciente();
            paciente.Nome = "Luan";
            paciente.CartaoSUS = "1322131231";

            return paciente;
        }

        public Funcionario gerarFuncionario()
        {
            Funcionario funcionario = new Funcionario();
            funcionario.Login = "loginteste";
            funcionario.Senha = "senhateste";
            funcionario.Nome = "nometeste";

            return funcionario;
        }

        [TestMethod]
        public void Deve_inserir_nova_requisicao()
        {
            //arrange
            

            Medicamento medicamento = new Medicamento("Dorflex","Voce toma e a dor fica flex", "abc-123",Convert.ToDateTime("24/06/2030"));
            medicamento.QuantidadeDisponivel = 10;

            Fornecedor fornecedor = gerarFornecedor();
            repositorioFornecedor.Inserir(fornecedor);
            medicamento.Fornecedor = fornecedor;
            repositorioMedicamento.Inserir(medicamento);
            Funcionario funcionario = new Funcionario("julio","julio@farmacia.com","julinho123");
            repositorioFuncionario.Inserir(funcionario);
            Paciente paciente = new Paciente("Amanda","2009199624");
            repositorioPaciente.Inserir(paciente);
            Requisicao novaRequisicao = new Requisicao();

            novaRequisicao.Medicamento = repositorioMedicamento.SelecionarPorId(medicamento.Id);
            novaRequisicao.Funcionario = repositorioFuncionario.SelecionarPorId(funcionario.Id);
            novaRequisicao.Paciente = repositorioPaciente.SelecionarPorId(paciente.Id);
            novaRequisicao.QtdMedicamento = 5;
            novaRequisicao.Data = Convert.ToDateTime("24/06/2030");


            //action 
            repositorioRequisicao.Inserir(novaRequisicao);

            //assert
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(novaRequisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(novaRequisicao.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(novaRequisicao.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
            Assert.AreEqual(novaRequisicao.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);
            Assert.AreEqual(novaRequisicao.Paciente.Id, requisicaoEncontrada.Paciente.Id);
            Assert.AreEqual(novaRequisicao.Data, requisicaoEncontrada.Data);
            Assert.AreEqual(novaRequisicao.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
        }

        [TestMethod]
        public void Deve_editar_informacoes_requisicao()
        {
            //arrange                      
            Medicamento medicamento = new Medicamento("Dorflex", "Voce toma e a dor fica flex", "abc-123", Convert.ToDateTime("24/06/2030"));
            medicamento.QuantidadeDisponivel = 10;

            Fornecedor fornecedor = gerarFornecedor();
            repositorioFornecedor.Inserir(fornecedor);
            medicamento.Fornecedor = fornecedor;
            repositorioMedicamento.Inserir(medicamento);
            Funcionario funcionario = new Funcionario("julio", "julio@farmacia.com", "julinho123");
            repositorioFuncionario.Inserir(funcionario);
            Paciente paciente = new Paciente("Amanda", "2009199624");
            repositorioPaciente.Inserir(paciente);
            Requisicao novaRequisicao = new Requisicao();

            novaRequisicao.Medicamento = repositorioMedicamento.SelecionarPorId(medicamento.Id);
            novaRequisicao.Funcionario = repositorioFuncionario.SelecionarPorId(funcionario.Id);
            novaRequisicao.Paciente = repositorioPaciente.SelecionarPorId(paciente.Id);
            novaRequisicao.QtdMedicamento = 5;
            novaRequisicao.Data = Convert.ToDateTime("24/06/2030");
            repositorioRequisicao.Inserir(novaRequisicao);


            //action
            novaRequisicao.QtdMedicamento = 7;
            novaRequisicao.Data = Convert.ToDateTime("26/08/2028");
            repositorioRequisicao.Editar(novaRequisicao);

            //assert
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(novaRequisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(novaRequisicao.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(novaRequisicao.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
            Assert.AreEqual(novaRequisicao.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);
            Assert.AreEqual(novaRequisicao.Paciente.Id, requisicaoEncontrada.Paciente.Id);
            Assert.AreEqual(novaRequisicao.Data, requisicaoEncontrada.Data);
            Assert.AreEqual(novaRequisicao.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
            //arrange           
            Medicamento medicamento = new Medicamento("Dorflex", "Voce toma e a dor fica flex", "abc-123", Convert.ToDateTime("24/06/2030"));
            medicamento.QuantidadeDisponivel = 10;

            Fornecedor fornecedor = gerarFornecedor();
            repositorioFornecedor.Inserir(fornecedor);
            medicamento.Fornecedor = fornecedor;
            repositorioMedicamento.Inserir(medicamento);
            Funcionario funcionario = new Funcionario("julio", "julio@farmacia.com", "julinho123");
            repositorioFuncionario.Inserir(funcionario);
            Paciente paciente = new Paciente("Amanda", "2009199624");
            repositorioPaciente.Inserir(paciente);
            Requisicao novaRequisicao = new Requisicao();

            novaRequisicao.Medicamento = repositorioMedicamento.SelecionarPorId(medicamento.Id);
            novaRequisicao.Funcionario = repositorioFuncionario.SelecionarPorId(funcionario.Id);
            novaRequisicao.Paciente = repositorioPaciente.SelecionarPorId(paciente.Id);
            novaRequisicao.QtdMedicamento = 5;
            novaRequisicao.Data = Convert.ToDateTime("24/06/2030");
            repositorioRequisicao.Inserir(novaRequisicao);

            //action           
            repositorioRequisicao.Excluir(novaRequisicao);

            //assert
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);
            Assert.IsNull(requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_uma_requisicao()
        {
            //arrange          
            Medicamento medicamento = new Medicamento("Dorflex", "Voce toma e a dor fica flex", "abc-123", Convert.ToDateTime("24/06/2030"));
            medicamento.QuantidadeDisponivel = 10;

            Fornecedor fornecedor = gerarFornecedor();
            repositorioFornecedor.Inserir(fornecedor);
            medicamento.Fornecedor = fornecedor;
            repositorioMedicamento.Inserir(medicamento);
            Funcionario funcionario = new Funcionario("julio", "julio@farmacia.com", "julinho123");
            repositorioFuncionario.Inserir(funcionario);
            Paciente paciente = new Paciente("Amanda", "2009199624");
            repositorioPaciente.Inserir(paciente);
            Requisicao novaRequisicao = new Requisicao();

            novaRequisicao.Medicamento = repositorioMedicamento.SelecionarPorId(medicamento.Id);
            novaRequisicao.Funcionario = repositorioFuncionario.SelecionarPorId(funcionario.Id);
            novaRequisicao.Paciente = repositorioPaciente.SelecionarPorId(paciente.Id);
            novaRequisicao.QtdMedicamento = 5;
            novaRequisicao.Data = Convert.ToDateTime("24/06/2030");
            repositorioRequisicao.Inserir(novaRequisicao);

            //action
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(novaRequisicao.Id);

            //assert
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(novaRequisicao.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(novaRequisicao.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
            Assert.AreEqual(novaRequisicao.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);
            Assert.AreEqual(novaRequisicao.Paciente.Id, requisicaoEncontrada.Paciente.Id);
            Assert.AreEqual(novaRequisicao.Data, requisicaoEncontrada.Data);
            Assert.AreEqual(novaRequisicao.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
        }

        [TestMethod]
        public void Deve_selecionar_todos_as_requisicoes()
        {
            //arrange
                //instanciando no repositorio os fornecedores
            var fornecedor1 = new Fornecedor("Julio", "4199852-9870", "julinho@gmail.com", "Curitiba", "Parana");
            var fornecedor2 = new Fornecedor("Carlinhos", "114002-8922", "carlinhos@gmail.com", "Lages", "Santa Catarina");
            var fornecedor3 = new Fornecedor("Baptista", "498999-6523", "baptista@gmail.com", "Porto Alegre", "Rio Grande do Sul");
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

            repositorioMedicamento.Inserir(medicamento1);
            repositorioMedicamento.Inserir(medicamento2);
            repositorioMedicamento.Inserir(medicamento3);

                //instanciando os funcionarios no repositorio
            var funcionario1 = new Funcionario("Amanda", "amanda1996@farmacia.com", "amandinha10*");
            var funcionario2 = new Funcionario("Cleberson", "cleberson2001@farmacia.com", "crebs25*");
            var funcionario3 = new Funcionario("Paulo", "paulo1999@farmacia.com", "paulin99*");
            repositorioFuncionario.Inserir(funcionario1);
            repositorioFuncionario.Inserir(funcionario2);
            repositorioFuncionario.Inserir(funcionario3);
            
                //instanciado os pacientes no repositorio
            var paciente1 = new Paciente("Joselito", "321456987");
            var paciente2 = new Paciente("Crebinho", "456789123");
            var paciente3 = new Paciente("Luizin", "400289222");
            
            repositorioPaciente.Inserir(paciente1);
            repositorioPaciente.Inserir(paciente2);
            repositorioPaciente.Inserir(paciente3);


            var requisicao1 = new Requisicao();
            requisicao1.Medicamento = medicamento1;
            requisicao1.Paciente = paciente1;
            requisicao1.Funcionario = funcionario1;
            requisicao1.QtdMedicamento = 2;
            requisicao1.Data = Convert.ToDateTime("24/06/2030");

            var requisicao2 = new Requisicao();
            requisicao2.Medicamento = medicamento2;
            requisicao2.Paciente = paciente2;
            requisicao2.Funcionario = funcionario2;
            requisicao2.QtdMedicamento = 8;
            requisicao2.Data = Convert.ToDateTime("20/04/2040");

            var requisicao3 = new Requisicao();
            requisicao3.Medicamento = medicamento3;
            requisicao3.Paciente = paciente3;
            requisicao3.Funcionario = funcionario3;
            requisicao3.QtdMedicamento = 22;
            requisicao3.Data = Convert.ToDateTime("20/04/2012");

            repositorioRequisicao.Inserir(requisicao1);
            repositorioRequisicao.Inserir(requisicao2);
            repositorioRequisicao.Inserir(requisicao3);

            //action
            var requisicoes = repositorioRequisicao.SelecionarTodos();

            //assert

            Assert.AreEqual(3, requisicoes.Count);

            Assert.AreEqual(requisicao1.Paciente.Nome, requisicoes[0].Paciente.Nome);
            Assert.AreEqual(requisicao2.Paciente.Nome, requisicoes[1].Paciente.Nome);

            Assert.AreEqual(requisicao1.Id, requisicoes[0].Id);
            Assert.AreEqual(requisicao1.Medicamento.Id, requisicoes[0].Medicamento.Id);
            Assert.AreEqual(requisicao1.Funcionario.Id, requisicoes[0].Funcionario.Id);
            Assert.AreEqual(requisicao1.Paciente.Id, requisicoes[0].Paciente.Id);
            Assert.AreEqual(requisicao1.Data, requisicoes[0].Data);
            Assert.AreEqual(requisicao1.QtdMedicamento, requisicoes[0].QtdMedicamento);

            Assert.AreEqual(requisicao2.Id, requisicoes[1].Id);
            Assert.AreEqual(requisicao2.Medicamento.Id, requisicoes[1].Medicamento.Id);
            Assert.AreEqual(requisicao2.Funcionario.Id, requisicoes[1].Funcionario.Id);
            Assert.AreEqual(requisicao2.Paciente.Id, requisicoes[1].Paciente.Id);
            Assert.AreEqual(requisicao2.Data, requisicoes[1].Data);
            Assert.AreEqual(requisicao2.QtdMedicamento, requisicoes[1].QtdMedicamento);

            Assert.AreEqual(requisicao3.Id, requisicoes[2].Id);
            Assert.AreEqual(requisicao3.Medicamento.Id, requisicoes[2].Medicamento.Id);
            Assert.AreEqual(requisicao3.Funcionario.Id, requisicoes[2].Funcionario.Id);
            Assert.AreEqual(requisicao3.Paciente.Id, requisicoes[2].Paciente.Id);
            Assert.AreEqual(requisicao3.Data, requisicoes[2].Data);
            Assert.AreEqual(requisicao3.QtdMedicamento, requisicoes[2].QtdMedicamento);

        }
    }
}
