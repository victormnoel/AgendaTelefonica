namespace AgendaTelefonica.Dominio.Excecoes;

public class TelefoneInvalidoException : Exception
{
    public TelefoneInvalidoException() : base("Um telefone válido deve ser informado")
    { }
}