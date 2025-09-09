namespace AgendaTelefonica.Dominio.Interfaces;

public interface IUsuarioServico
{
    Task<bool> ExisteUmUsuarioComAsMesmaInformacoes(string nomeDoUsuario, string emailDoUsuario, int? usuarioId = null);
}