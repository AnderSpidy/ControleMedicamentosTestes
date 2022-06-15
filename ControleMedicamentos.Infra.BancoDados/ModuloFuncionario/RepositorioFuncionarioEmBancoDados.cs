using ControleMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class RepositorioFuncionarioEmBancoDados
    {
        private string enderecoBanco =
            @"Data Source=(LocalDB)\MSSqlLocalDB;
              Initial Catalog=ControleMedicamentosDB;
              Integrated Security=True;
              Pooling=False";

        private const string sqlInserir =
          @"INSERT INTO [TBFUNCIONARIO] 
                (
                    [NOME],
                    [LOGIN],
                    [SENHA]
	            )
	            VALUES
                (
                    @NOME,
                    @LOGIN,
                    @SENHA
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @"UPDATE [TBFUNCIONARIO]	
		        SET
			        [NOME] = @NOME,
			        [LOGIN] = @LOGIN,
                    [SENHA] = @SENHA
		        WHERE
			        [ID] = @ID";


        private const string sqlExcluir =
           @"DELETE FROM [TBFUNCIONARIO]			        
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarPorId =
          @"SELECT 
		            [ID], 
		            [NOME], 
		            [LOGIN],
                    [SENHA]
	            FROM 
		            [TBFUNCIONARIO]
		        WHERE
                    [ID] = @ID";


        private const string sqlSelecionarTodos =
          @"SELECT 
		            [ID], 
		            [NOME], 
		            [LOGIN],
                    [SENHA]
	            FROM 
		            [TBFUNCIONARIO]";


        public ValidationResult Inserir(Funcionario novoFuncionario)
        {
            var validador = new ValidaFuncionario();

            var resultadoValidacao = validador.Validate(novoFuncionario);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFuncionario(novoFuncionario, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoFuncionario.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }



        public ValidationResult Editar(Funcionario funcionario)
        {
            var validador = new ValidaFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFuncionario(funcionario, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public void Excluir(Funcionario funcionario)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExcluir = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExcluir.Parameters.AddWithValue("ID", funcionario.Id);

            conexaoComBanco.Open();
            comandoExcluir.ExecuteNonQuery();
            conexaoComBanco.Close();


        }

        public List<Funcionario> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            //nao tem parametro
            conexaoComBanco.Open();
            SqlDataReader leitorFuncionario = comandoSelecao.ExecuteReader();

            List<Funcionario> pacientes = new List<Funcionario>();
            while (leitorFuncionario.Read())
                pacientes.Add(ConverterParaFuncionario(leitorFuncionario));

            conexaoComBanco.Close();

            return pacientes;
        }

        public Funcionario SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorFuncionario = comandoSelecao.ExecuteReader();

            Funcionario paciente = null;
            if (leitorFuncionario.Read())
                paciente = ConverterParaFuncionario(leitorFuncionario);

            conexaoComBanco.Close();

            return paciente;
        }

        private void ConfigurarParametrosFuncionario(Funcionario funcionario, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", funcionario.Id);
            comando.Parameters.AddWithValue("NOME", funcionario.Nome);
            comando.Parameters.AddWithValue("LOGIN", funcionario.Login);
            comando.Parameters.AddWithValue("SENHA", funcionario.Senha);
        }

        private Funcionario ConverterParaFuncionario(SqlDataReader leitorFuncionario)
        {
            int id = Convert.ToInt32(leitorFuncionario["ID"]);
            string nome = Convert.ToString(leitorFuncionario["NOME"]);
            string login = Convert.ToString(leitorFuncionario["LOGIN"]);
            string senha = Convert.ToString(leitorFuncionario["SENHA"]);


            return new Funcionario()
            {
                Id = id,
                Nome = nome,
                Login = login,
                Senha = senha
            };

        }
    }
}
