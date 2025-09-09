namespace AgendaTelefonica.Dominio.Excecoes;

public class TelefoneInvalidoException : Exception
{
    #region Construtor
    public TelefoneInvalidoException() : base("Um telefone válido deve ser informado")
    { }
    #endregion
}