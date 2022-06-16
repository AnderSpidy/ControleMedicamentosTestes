using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados
    {
        private const string enderecoBanco =
            "Data Source=(LocalDb)\\MSSQLLocalDB;" +
            "Initial Catalog=ControleMedicamentosDB;" +
            "Integrated Security=True;" +
            "Pooling=False";

        private const string sqlInserir =
          @"INSERT INTO [TBREQUISICAO]
                (
                    [FUNCIONARIO_ID],                    
                    [PACIENTE_ID],
                    [MEDICAMENTO_ID],
                    [QUANTIDADEMEDICAMENTO],
                    [DATA]
                    
	            )
	            VALUES
                (
                    @FUNCIONARIO_ID,
                    @PACIENTE_ID,
                    @MEDICAMENTO_ID,
                    @QUANTIDADEMEDICAMENTO,
                    @DATA
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @" UPDATE [TBREQUISICAO]
                    SET 
                        [FUNCIONARIO_ID] = @FUNCIONARIO_ID, 
                        [PACIENTE_ID] = @PACIENTE_ID, 
                        [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                        [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO, 
                        [DATA] = @DATA


                    WHERE [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBREQUISICAO] 


                WHERE [ID] = @ID";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
                REQUISICAO.[ID],       
                REQUISICAO.[FUNCIONARIO_ID],
                REQUISICAO.[PACIENTE_ID],
                REQUISICAO.[MEDICAMENTO_ID],             
                REQUISICAO.[QUANTIDADEMEDICAMENTO],      
                REQUISICAO.[DATA],   
                FUNCIONARIO.[NOME] AS FUNCIONARIO_NOME,              
                FUNCIONARIO.[LOGIN],                    
                FUNCIONARIO.[SENHA], 
                PACIENTE.[NOME] AS PACIENTE_NOME,
                PACIENTE.[CARTAOSUS],
                MEDICAMENTO.[NOME] AS MEDICAMENTO_NOME,
                MEDICAMENTO.[DESCRICAO],
                MEDICAMENTO.[LOTE],
                MEDICAMENTO.[VALIDADE],
                MEDICAMENTO.[QUANTIDADEDISPONIVEL]
            FROM
                [TBREQUISICAO] AS REQUISICAO INNER JOIN [TBFUNCIONARIO] AS FUNCIONARIO
                    ON REQUISICAO.FUNCIONARIO_ID = FUNCIONARIO.ID
                INNER JOIN [TBPACIENTE] AS PACIENTE 
                    ON REQUISICAO.PACIENTE_ID = PACIENTE.ID
                INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                    ON REQUISICAO.MEDICAMENTO_ID = MEDICAMENTO.ID
            WHERE
                REQUISICAO.[ID] = @ID
                ";


        private const string sqlSelecionarTodos =
             @"SELECT        
	                REQUI.ID,
	                REQUI.FUNCIONARIO_ID, 
	                REQUI.PACIENTE_ID, 
	                REQUI.MEDICAMENTO_ID, 	
 	                REQUI.QUANTIDADEMEDICAMENTO,
	                REQUI.DATA, 
	                FUNC.NOME AS FUNCIONARIO_NOME,
	                FUNC.LOGIN, 
	                FUNC.SENHA,
	                PACIENTE.NOME AS PACIENTE_NOME,
                    PACIENTE.CARTAOSUS,
                    MEDIC.NOME AS MEDICAMENTO_NOME,
                    MEDIC.DESCRICAO,
                    MEDIC.LOTE,
                    MEDIC.VALIDADE,
                    MEDIC.QUANTIDADEDISPONIVEL,
                    
                FROM  
	                TBREQUISICAO AS REQUI

                INNER JOIN TBFUNCIONARIO AS FUNC
                ON 
                    REQUI.FUNCIONARIO_ID = FUNCIONARIO.ID

                INNER JOIN TBPACIENTE AS PACIENTE
                ON 
                    REQUI.PACIENTE_ID = PACIENTE.ID

                INNER JOIN TBMEDICAMENTO AS MEDIC
                ON
                    REQUI.MEDICAMENTO_ID = MEDICAMENTO.ID";


        public ValidationResult Inserir(Requisicao requisicao)
        {
            var validador = new ValidaRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            requisicao.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            var validador = new ValidaRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }


        public void Excluir(Requisicao requisicao)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExcluir = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExcluir.Parameters.AddWithValue("ID", requisicao.Id);

            conexaoComBanco.Open();
            comandoExcluir.ExecuteNonQuery();
            conexaoComBanco.Close();

        }

        public Requisicao SelecionarPorNumero(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao requisicao = null;
            if (leitorRequisicao.Read())
                requisicao = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return requisicao;
        }

        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao requisicao = ConverterParaRequisicao(leitorRequisicao);

                requisicoes.Add(requisicao);
            }

            conexaoComBanco.Close();

            return requisicoes;
        }

        #region Métodos privados

        private void ConfigurarParametrosRequisicao(Requisicao requisicao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", requisicao.Id);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", requisicao.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", requisicao.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", requisicao.Medicamento.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", requisicao.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", requisicao.Data);
        }

        private Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            var id = Convert.ToInt32(leitorRequisicao["ID"]);
            var quantidadeMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            var data = Convert.ToDateTime(leitorRequisicao["DATA"]);

            var funcionarioId = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);
            string funcionarioNome = Convert.ToString(leitorRequisicao["FUNCIONARIO_NOME"]);
            string funcionarioLogin = Convert.ToString(leitorRequisicao["LOGIN"]);
            string funcionarioSenha = Convert.ToString(leitorRequisicao["SENHA"]);

            var pacienteId = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            string pacienteNome = Convert.ToString(leitorRequisicao["PACIENTE_NOME"]);
            string cartaoSus = Convert.ToString(leitorRequisicao["CARTAOSUS"]);

            var medicamentoId = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_ID"]);
            var medicamentoNome = Convert.ToString(leitorRequisicao["MEDICAMENTO_NOME"]);
            var medicamentoDescricao = Convert.ToString(leitorRequisicao["DESCRICAO"]);
            var medicamentoLote = Convert.ToString(leitorRequisicao["LOTE"]);
            var medicamentoValidade = Convert.ToDateTime(leitorRequisicao["VALIDADE"]);
            var medicamentoQuantidadeDisponivel = Convert.ToInt32(leitorRequisicao["QUANTIDADEDISPONIVEL"]);

            Requisicao requisicao = new Requisicao();
            requisicao.Id = id;
            requisicao.QtdMedicamento = quantidadeMedicamento;
            requisicao.Data = data;

            requisicao.Funcionario = new Funcionario
            {
                Id = funcionarioId,
                Nome = funcionarioNome,
                Login = funcionarioLogin,
                Senha = funcionarioSenha
            };

            requisicao.Paciente = new Paciente
            {
                Id = pacienteId,
                Nome = pacienteNome,
                CartaoSUS = cartaoSus
            };

            requisicao.Medicamento = new Medicamento
            {
                Id = medicamentoId,
                Nome = medicamentoNome,
                Descricao = medicamentoDescricao,
                Lote = medicamentoLote,
                Validade = medicamentoValidade,
                QuantidadeDisponivel = medicamentoQuantidadeDisponivel
            };
            return requisicao;
        }
        #endregion
    }
}
