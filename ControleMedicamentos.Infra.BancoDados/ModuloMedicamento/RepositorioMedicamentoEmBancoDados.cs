﻿using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados
    {
        private const string enderecoBanco =
           "Data Source=(LocalDB)\\MSSQLLocalDB;" +
           "Initial Catalog=ControleMedicamentosDB;" +
           "Integrated Security=True;" +
           "Pooling=False";

        #region Sql Queries de Medicamento

        private const string sqlInserir =
            @"INSERT INTO [TBMEDICAMENTO]
                (
                    [NOME],       
                    [DESCRICAO], 
                    [LOTE],
                    [VALIDADE],                    
                    [QUANTIDADEDISPONIVEL],                                                           
                    [FORNECEDOR_ID]       
                )
            VALUES
                (
                    @NOME,
                    @DESCRICAO,
                    @LOTE,
                    @VALIDADE,
                    @QUANTIDADEDISPONIVEL,
                    @FORNECEDOR_ID
                ); SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @" UPDATE [TBMEDICAMENTO]
                    SET 
                        [NOME] = @NOME, 
                        [DESCRICAO] = @DESCRICAO, 
                        [LOTE] = @LOTE,
                        [VALIDADE] = @VALIDADE, 
                        [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL,
                        [FORNECEDOR_ID] = @FORNECEDOR_ID

                    WHERE [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBMEDICAMENTO] 
                WHERE [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
                MEDICAMENTO.[ID],       
                MEDICAMENTO.[NOME],
                MEDICAMENTO.[DESCRICAO],
                MEDICAMENTO.[LOTE],             
                MEDICAMENTO.[VALIDADE],                    
                MEDICAMENTO.[QUANTIDADEDISPONIVEL],                                
                MEDICAMENTO.[FORNECEDOR_ID],
                FORNECEDOR.[NOME],              
                FORNECEDOR.[TELEFONE],                    
                FORNECEDOR.[EMAIL], 
                FORNECEDOR.[CIDADE],
                FORNECEDOR.[ESTADO]
            FROM
                [TBMEDICAMENTO] AS MEDICAMENTO LEFT JOIN 
                [TBFORNECEDOR] AS FORNECEDOR
            ON
                FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
                MED.[ID],       
                MED.[NOME],
                MED.[DESCRICAO],
                MED.[LOTE],             
                MED.[VALIDADE],                    
                MED.[QUANTIDADEDISPONIVEL],                                
                MED.[FORNECEDOR_ID],
                FORNE.[NOME],              
                FORNE.[TELEFONE],                    
                FORNE.[EMAIL], 
                FORNE.[CIDADE],
                FORNE.[ESTADO]
            FROM
                [TBMEDICAMENTO] AS MED LEFT JOIN 
                [TBFORNECEDOR] AS FORNE
            ON
                FORNE.ID = MED.FORNECEDOR_ID
            WHERE 
                MED.[ID] = @ID";

        #endregion
        public ValidationResult Inserir(Medicamento novoMedicamento)
        {
            var validador = new ValidaMedicamento();

            var resultadoValidacao = validador.Validate(novoMedicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMedicamento(novoMedicamento, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoMedicamento.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Medicamento registro)
        {
            var validador = new ValidaMedicamento();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var x = SelecionarPorId(registro.Id);

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMedicamento(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Medicamento registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Medicamento SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            Medicamento medicamento = null;
            if (leitorMedicamento.Read())
                medicamento = ConverterParaMedicamento(leitorMedicamento);

            conexaoComBanco.Close();

            return medicamento;
        }

        public List<Medicamento> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();

            while (leitorMedicamento.Read())
            {
                Medicamento medicamento = ConverterParaMedicamento(leitorMedicamento);

                medicamentos.Add(medicamento);
            }

            conexaoComBanco.Close();

            return medicamentos;
        }

        #region Métodos privados

        private void ConfigurarParametrosMedicamento(Medicamento medicamento, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", medicamento.Id);
            comando.Parameters.AddWithValue("NOME", medicamento.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", medicamento.Descricao);
            comando.Parameters.AddWithValue("LOTE", medicamento.Lote);
            comando.Parameters.AddWithValue("VALIDADE", medicamento.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", medicamento.QuantidadeDisponivel);

            comando.Parameters.AddWithValue("FORNECEDOR_ID", medicamento.Fornecedor.Id);
        }

        private Medicamento ConverterParaMedicamento(SqlDataReader leitorMedicamento)
        {
            var id = Convert.ToInt32(leitorMedicamento["ID"]);
            var nome = Convert.ToString(leitorMedicamento["NOME"]);
            var descricao = Convert.ToString(leitorMedicamento["DESCRICAO"]);
            var lote = Convert.ToString(leitorMedicamento["LOTE"]);
            var validade = Convert.ToDateTime(leitorMedicamento["VALIDADE"]);
            var quantidadeDisponivel = Convert.ToInt32(leitorMedicamento["QUANTIDADEDISPONIVEL"]);

            Medicamento medicamento = new Medicamento();
            medicamento.Id = id;
            medicamento.Nome = nome;
            medicamento.Descricao = descricao;
            medicamento.Lote = lote;
            medicamento.Validade = validade;
            medicamento.QuantidadeDisponivel = quantidadeDisponivel;

            var idFornecedor = Convert.ToInt32(leitorMedicamento["FORNECEDOR_ID"]);
            var nomeFornecedor = Convert.ToString(leitorMedicamento["NOME"]);
            var telefoneFornecedor = Convert.ToString(leitorMedicamento["TELEFONE"]);
            var emailFornecedor = Convert.ToString(leitorMedicamento["EMAIL"]);
            var cidadeFornecedor = Convert.ToString(leitorMedicamento["CIDADE"]);
            var estadoFornecedor = Convert.ToString(leitorMedicamento["ESTADO"]);

            medicamento.Fornecedor = new Fornecedor
            {
                Id = idFornecedor,
                Nome = nomeFornecedor,
                Telefone = telefoneFornecedor,
                Email = emailFornecedor,
                Cidade = cidadeFornecedor,
                Estado = estadoFornecedor
            };


            return medicamento;
        }

        #endregion
    }
}
