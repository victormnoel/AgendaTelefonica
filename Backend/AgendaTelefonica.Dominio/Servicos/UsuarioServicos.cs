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
    
    public async Task<bool> ExisteUmUsuarioComAsMesmaInformacoes(string nomeDoUsuario, string emailDoUsuario)
    {
        string nomeDoUsuarioFormatado = nomeDoUsuario.ToLower().Trim();
        string emailDoUsuarioFormatado = emailDoUsuario.ToLower().Trim();
        
        List<Usuario>? usuarioComOMesmoNome = await _usuarioRepositorio.BuscarPorFiltro(
            usuario => usuario.Nome.ToLower().Trim().Equals(nomeDoUsuarioFormatado) || 
                       usuario.Email.ToLower().Trim().Equals(emailDoUsuarioFormatado));
        
        return usuarioComOMesmoNome is { Count: > 0 };
    }
    
    #endregion
}