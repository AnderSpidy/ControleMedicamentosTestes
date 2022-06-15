using ControleMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedorEmBancoDados
    {
        private string enderecoBanco =
             @"Data Source=(LocalDB)\MSSqlLocalDB;
              Initial Catalog=ControleMedicamentosDB;
              Integrated Security=True;
              Pooling=False";

        private const string sqlInserir =
            @"INSERT INTO [TBFORNECEDOR]
                (
                    [NOME],       
                    [TELEFONE], 
                    [EMAIL],
                    [CIDADE],                    
                    [ESTADO]   
                )
            VALUES
                (
                    @NOME,
                    @TELEFONE,
                    @EMAIL,
                    @CIDADE,
                    @ESTADO
                ); SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @" UPDATE [TBFORNECEDOR]
                    SET 
                        [NOME] = @NOME, 
                        [TELEFONE] = @TELEFONE, 
                        [EMAIL] = @EMAIL,
                        [CIDADE] = @CIDADE, 
                        [ESTADO] = @ESTADO

                    WHERE [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBMEDICAMENTO]
                WHERE [FORNECEDOR_ID] = @ID

                DELETE FROM [TBFORNECEDOR] 
                    WHERE [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
                [ID],       
                [NOME],
                [TELEFONE],
                [EMAIL],             
                [CIDADE],                    
                [ESTADO]
            FROM
                [TBFORNECEDOR]";

        private const string sqlSelecionarPorId =
            @"SELECT 
                [ID],       
                [NOME],
                [TELEFONE],
                [EMAIL],             
                [CIDADE],                    
                [ESTADO]
            FROM
                [TBFORNECEDOR]
            WHERE 
                [ID] = @ID";


        public ValidationResult Inserir(Fornecedor novoFornecedor)
        {
            var validador = new ValidaFornecedor();

            var resultadoValidacao = validador.Validate(novoFornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFornecedor(novoFornecedor, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoFornecedor.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Fornecedor registro)
        {
            var validador = new ValidaFornecedor();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var x = SelecionarPorId(registro.Id);

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFornecedor(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public void Excluir(Fornecedor fornecedor)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExcluir = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExcluir.Parameters.AddWithValue("ID", fornecedor.Id);

            conexaoComBanco.Open();
            comandoExcluir.ExecuteNonQuery();
            conexaoComBanco.Close();


        }

        public Fornecedor SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            Fornecedor fornecedor = null;
            if (leitorFornecedor.Read())
                fornecedor = ConverterParaFornecedor(leitorFornecedor);

            conexaoComBanco.Close();

            return fornecedor;
        }

        public List<Fornecedor> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            List<Fornecedor> fornecedores = new List<Fornecedor>();

            while (leitorFornecedor.Read())
            {
                Fornecedor fornecedor = ConverterParaFornecedor(leitorFornecedor);

                fornecedores.Add(fornecedor);
            }

            conexaoComBanco.Close();

            return fornecedores;
        }

        #region Métodos privados

        private void ConfigurarParametrosFornecedor(Fornecedor fornecedor, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", fornecedor.Id);
            comando.Parameters.AddWithValue("NOME", fornecedor.Nome);
            comando.Parameters.AddWithValue("TELEFONE", fornecedor.Telefone);
            comando.Parameters.AddWithValue("EMAIL", fornecedor.Email);
            comando.Parameters.AddWithValue("CIDADE", fornecedor.Cidade);
            comando.Parameters.AddWithValue("ESTADO ", fornecedor.Estado);
        }

        private Fornecedor ConverterParaFornecedor(SqlDataReader leitorFornecedor)
        {
            var id = Convert.ToInt32(leitorFornecedor["ID"]);
            var nome = Convert.ToString(leitorFornecedor["NOME"]);
            var telefone = Convert.ToString(leitorFornecedor["TELEFONE"]);
            var email = Convert.ToString(leitorFornecedor["EMAIL"]);
            var cidade = Convert.ToString(leitorFornecedor["CIDADE"]);
            var estado = Convert.ToString(leitorFornecedor["ESTADO"]);

            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Id = id;
            fornecedor.Nome = nome;
            fornecedor.Telefone = telefone;
            fornecedor.Email = email;
            fornecedor.Cidade = cidade;
            fornecedor.Estado = estado;

            return fornecedor;
        }

        #endregion

    }
}
