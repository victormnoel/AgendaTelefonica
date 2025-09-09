namespace AgendaTelefonica.Dominio.Excecoes;

public class NomeInvalidoException : Exception
{
    public NomeInvalidoException() : base("Um nome válido deve ser informado")
    {}
}