using System;
using System.Collections.Generic;
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
          @"INSERT INTO [TBTESTE]
                (
                    [TITULO],                    
                    [DATAGERACAO],
                    [QUANTIDADEQUESTOES],
                    [PROVAO],
                    [MATERIA_NUMERO],
                    [DISCIPLINA_NUMERO]
	            )
	            VALUES
                (
                    @TITULO,
                    @DATAGERACAO,
                    @QUANTIDADEQUESTOES,
                    @PROVAO,
                    @MATERIA_NUMERO,
                    @DISCIPLINA_NUMERO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlAdicionarQuestao =
          @"INSERT INTO [TBTESTE_TBQUESTAO]
                (
                    [TESTE_NUMERO],                    
                    [QUESTAO_NUMERO]
	            )
	            VALUES
                (
                    @TESTE_NUMERO,
                    @QUESTAO_NUMERO
                );";

        private const string sqlSelecionarTodos =
             @"SELECT        
	                T.NUMERO,
	                T.TITULO, 
	                T.DATAGERACAO, 
	                T.PROVAO, 	
 	                T.QUANTIDADEQUESTOES,
	                D.NUMERO DISCIPLINA_NUMERO, 
	                D.NOME DISCIPLINA_NOME,
	
	                M.NUMERO MATERIA_NUMERO, 
	                M.NOME MATERIA_NOME,
	                M.SERIE MATERIA_SERIE 	
                FROM  
	                TBTESTE T INNER JOIN TBDISCIPLINA D 
                ON 
	                T.DISCIPLINA_NUMERO = D.NUMERO LEFT JOIN TBMATERIA M 
                ON 
	                T.MATERIA_NUMERO = M.NUMERO";

        private const string sqlSelecionarPorNumero =
            @"SELECT        
	                T.NUMERO,
	                T.TITULO, 
	                T.DATAGERACAO, 
	                T.PROVAO, 	
 	                T.QUANTIDADEQUESTOES,
	                D.NUMERO DISCIPLINA_NUMERO, 
	                D.NOME DISCIPLINA_NOME,
	
	                M.NUMERO MATERIA_NUMERO, 
	                M.NOME MATERIA_NOME,
	                M.SERIE MATERIA_SERIE 	
                FROM  
	                TBTESTE T INNER JOIN TBDISCIPLINA D 
                ON 
	                T.DISCIPLINA_NUMERO = D.NUMERO LEFT JOIN TBMATERIA M 
                ON 
	                T.MATERIA_NUMERO = M.NUMERO
                WHERE 
	                T.[NUMERO] = @NUMERO";

        private const string sqlSelecionarQuestoesDoTeste =
            @"SELECT 
	                Q.NUMERO, 
	                Q.ENUNCIADO
                FROM 
	                TBQUESTAO AS Q INNER JOIN TBTESTE_TBQUESTAO AS TQ
                ON 
	                Q.NUMERO = TQ.QUESTAO_NUMERO
                WHERE 
	                TQ.TESTE_NUMERO = @TESTE_NUMERO";

        private const string sqlExcluir =
           @"DELETE FROM [TBTESTE_TBQUESTAO]
		            WHERE
			            [TESTE_NUMERO] = @NUMERO;
            DELETE FROM [TBTESTE]
		            WHERE
			            [NUMERO] = @NUMERO";

        //public ValidationResult Inserir(Teste teste)
        //{
        //    ValidadorTeste validador = new ValidadorTeste();

        //    var resultadoValidacao = validador.Validate(teste);

        //    if (resultadoValidacao.IsValid == false)
        //        return resultadoValidacao;

        //    SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

        //    SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

        //    ConfigurarParametrosTeste(teste, comandoInsercao);

        //    conexaoComBanco.Open();
        //    var id = comandoInsercao.ExecuteScalar();
        //    teste.Numero = Convert.ToInt32(id);

        //    conexaoComBanco.Close();

        //    AdicionarQuestoesNoTeste(teste);

        //    return resultadoValidacao;
        //}

        //public ValidationResult Excluir(Teste registro)
        //{
        //    SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

        //    SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

        //    comandoExclusao.Parameters.AddWithValue("NUMERO", registro.Numero);

        //    conexaoComBanco.Open();
        //    int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

        //    var resultadoValidacao = new ValidationResult();

        //    if (numeroRegistrosExcluidos == 0)
        //        resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

        //    conexaoComBanco.Close();

        //    return resultadoValidacao;
        //}

        //public List<Teste> SelecionarTodos()
        //{
        //    SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

        //    SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

        //    conexaoComBanco.Open();
        //    SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

        //    List<Teste> testes = new List<Teste>();

        //    while (leitorTeste.Read())
        //    {
        //        Teste teste = ConverterParaTeste(leitorTeste);

        //        testes.Add(teste);
        //    }

        //    conexaoComBanco.Close();

        //    return testes;
        //}

        //public Teste SelecionarPorNumero(int numero)
        //{
        //    SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

        //    SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

        //    comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

        //    conexaoComBanco.Open();
        //    SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

        //    Teste teste = null;

        //    if (leitorTeste.Read())
        //    {
        //        teste = ConverterParaTeste(leitorTeste);
        //    }

        //    conexaoComBanco.Close();

        //    CarregarQuestoes(teste);

        //    return teste;
        //}

        //private void AdicionarQuestoesNoTeste(Teste teste)
        //{
        //    SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
        //    conexaoComBanco.Open();

        //    foreach (var questao in teste.Questoes)
        //    {
        //        SqlCommand comandoInsercao = new SqlCommand(sqlAdicionarQuestao, conexaoComBanco);

        //        comandoInsercao.Parameters.AddWithValue("TESTE_NUMERO", teste.Numero);
        //        comandoInsercao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

        //        comandoInsercao.ExecuteNonQuery();
        //    }

        //    conexaoComBanco.Close();
        //}

        //private void ConfigurarParametrosTeste(Teste teste, SqlCommand comando)
        //{
        //    comando.Parameters.AddWithValue("NUMERO", teste.Numero);
        //    comando.Parameters.AddWithValue("TITULO", teste.Titulo);
        //    comando.Parameters.AddWithValue("DATAGERACAO", teste.DataGeracao);
        //    comando.Parameters.AddWithValue("QUANTIDADEQUESTOES", teste.QuantidadeQuestoes);
        //    comando.Parameters.AddWithValue("DISCIPLINA_NUMERO", teste.Disciplina.Numero);

        //    if (teste.Materia != null)
        //        comando.Parameters.AddWithValue("MATERIA_NUMERO", teste.Materia.Numero);
        //    else
        //        comando.Parameters.AddWithValue("MATERIA_NUMERO", DBNull.Value);

        //    comando.Parameters.AddWithValue("PROVAO", teste.Provao);
        //}

        //private void CarregarQuestoes(Teste teste)
        //{
        //    SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

        //    SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarQuestoesDoTeste, conexaoComBanco);

        //    comandoSelecao.Parameters.AddWithValue("TESTE_NUMERO", teste.Numero);

        //    conexaoComBanco.Open();
        //    SqlDataReader leitorQuestoes = comandoSelecao.ExecuteReader();

        //    while (leitorQuestoes.Read())
        //    {
        //        Questao questao = ConverterParaQuestao(leitorQuestoes);

        //        teste.Questoes.Add(questao);
        //    }

        //    conexaoComBanco.Close();
        //}

        //private Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        //{
        //    int numero = Convert.ToInt32(leitorQuestao["NUMERO"]);
        //    string enunciado = Convert.ToString(leitorQuestao["ENUNCIADO"]);

        //    return new Questao
        //    {
        //        Numero = numero,
        //        Enunciado = enunciado
        //    };
        //}

        //private Teste ConverterParaTeste(SqlDataReader leitorTeste)
        //{
        //    var numero = Convert.ToInt32(leitorTeste["NUMERO"]);
        //    string titulo = Convert.ToString(leitorTeste["TITULO"]);
        //    var provao = Convert.ToBoolean(leitorTeste["PROVAO"]);
        //    var dataGeracao = Convert.ToDateTime(leitorTeste["DATAGERACAO"]);
        //    var qtdQuestoes = Convert.ToInt32(leitorTeste["QUANTIDADEQUESTOES"]);

        //    int numeroDisciplina = Convert.ToInt32(leitorTeste["DISCIPLINA_NUMERO"]);
        //    string nomeDisciplina = Convert.ToString(leitorTeste["DISCIPLINA_NOME"]);

        //    var disciplina = new Disciplina
        //    {
        //        Numero = numeroDisciplina,
        //        Nome = nomeDisciplina
        //    };

        //    var teste = new Teste
        //    {
        //        Numero = numero,
        //        Titulo = titulo,
        //        Provao = provao,
        //        DataGeracao = dataGeracao,
        //        QuantidadeQuestoes = qtdQuestoes
        //    };

        //    Materia materia;

        //    if (provao == false)
        //    {
        //        var numeroMateria = Convert.ToInt32(leitorTeste["MATERIA_NUMERO"]);
        //        var nomeMateria = Convert.ToString(leitorTeste["MATERIA_NOME"]);
        //        var serieMateria = (SerieMateriaEnum)leitorTeste["MATERIA_SERIE"];

        //        materia = new Materia
        //        {
        //            Numero = numeroMateria,
        //            Nome = nomeMateria,
        //            Serie = serieMateria
        //        };

        //        materia.ConfigurarDisciplina(disciplina);
        //        teste.ConfigurarMateria(materia);
        //    }

        //    teste.ConfigurarDisciplina(disciplina);

        //    return teste;
        //}

        //#region métodos não implementados
        //public ValidationResult Editar(Teste registro)
        //{
        //    return new ValidationResult();
        //}
        //#endregion
    }
}
