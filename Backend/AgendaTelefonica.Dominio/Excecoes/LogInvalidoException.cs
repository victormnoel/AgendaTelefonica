namespace AgendaTelefonica.Dominio.Excecoes;

public class LogInvalidoException : Exception
{
    #region Construtor
    public LogInvalidoException(string mensagem) : base(mensagem)
    {}
    
    #endregion
}