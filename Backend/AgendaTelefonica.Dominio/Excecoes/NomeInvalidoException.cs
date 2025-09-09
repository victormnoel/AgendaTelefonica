namespace AgendaTelefonica.Dominio.Excecoes;

public class NomeInvalidoException : Exception
{
    #region Construtor
    public NomeInvalidoException() : base("Um nome válido deve ser informado")
    {}
    #endregion
}