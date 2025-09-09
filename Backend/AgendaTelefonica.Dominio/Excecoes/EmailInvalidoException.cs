namespace AgendaTelefonica.Dominio.Excecoes;

public class EmailInvalidoException : Exception
{
    public EmailInvalidoException() : base("Um email válido deve ser informado")
    { }
}