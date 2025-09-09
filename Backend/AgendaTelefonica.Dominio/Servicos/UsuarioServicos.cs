using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;

namespace AgendaTelefonica.Dominio.Servicos;

public class UsuarioServicos : IUsuarioServico
{
    #region Propriedades
    
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    
    #endregion
    
    #region Construtor

    public UsuarioServicos(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    #endregion
    
    #region Servicos
    
    public async Task<bool> ExisteUmUsuarioComAsMesmaInformacoes(string nomeDoUsuario, string emailDoUsuario, int? usuarioId = null)
    {
        string nomeDoUsuarioFormatado = nomeDoUsuario.ToLower().Trim();
        string emailDoUsuarioFormatado = emailDoUsuario.ToLower().Trim();
        
        List<Usuario>? usuarioComAsMesmasInformacoes = await _usuarioRepositorio.BuscarPorFiltro(
            usuario => usuario.Nome.ToLower().Trim().Equals(nomeDoUsuarioFormatado) || 
                       usuario.Email.ToLower().Trim().Equals(emailDoUsuarioFormatado));

        if (usuarioId != null && usuarioComAsMesmasInformacoes != null)
        {
            List<Usuario> usuarioComMesmoIdEInformacoes = usuarioComAsMesmasInformacoes.Where(usuarioExistente => usuarioExistente.Id == usuarioId).ToList();
            usuarioComAsMesmasInformacoes = usuarioComMesmoIdEInformacoes.Count == 1
                ? null
                : usuarioComAsMesmasInformacoes;
        }
        return usuarioComAsMesmasInformacoes is { Count: > 0 };
    }
    
    #endregion
}