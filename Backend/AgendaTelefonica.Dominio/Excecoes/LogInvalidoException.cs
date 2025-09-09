namespace AgendaTelefonica.Dominio.Excecoes;

public class LogInvalidoException : Exception
{
    public LogInvalidoException(string mensagem) : base(mensagem)
    {}
}