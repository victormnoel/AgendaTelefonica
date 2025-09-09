namespace AgendaTelefonica.Dominio.Excecoes;

public class UsuarioJaExcluidoException : Exception
{
    public UsuarioJaExcluidoException() : base("Não é possível excluir o usuário, pois o mesmo já foi excluído!")
    { }
}