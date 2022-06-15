namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {

        public Funcionario(string nome, string login, string senha)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public Funcionario() { } //só para instanciar sem colocar as propriedades

        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
