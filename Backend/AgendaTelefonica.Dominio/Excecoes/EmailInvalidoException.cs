namespace AgendaTelefonica.Dominio.Excecoes;

public class EmailInvalidoException : Exception
{
    #region Construtor
    public EmailInvalidoException() : base("Um email válido deve ser informado")
    { }
    
    #endregion
}