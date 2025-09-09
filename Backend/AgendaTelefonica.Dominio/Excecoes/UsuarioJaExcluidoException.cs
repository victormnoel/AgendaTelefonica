namespace AgendaTelefonica.Dominio.Excecoes;

public class UsuarioJaExcluidoException : Exception
{
    #region Construtor
    public UsuarioJaExcluidoException() : base("Não é possível excluir o usuário, pois o mesmo já foi excluído!")
    { }
    #endregion
}