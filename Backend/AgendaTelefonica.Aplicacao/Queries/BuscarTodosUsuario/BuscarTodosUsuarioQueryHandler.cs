using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Enums;
using AgendaTelefonica.Dominio.Interfaces;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Queries.BuscarTodosUsuario;

public class BuscarTodosUsuarioQueryHandler : IRequestHandler<BuscarTodosUsuarioQuery, List<UsuarioViewModel>?>
{
    #region Propriedades

    private readonly IUsuarioRepositorio _usuarioRepositorio;
    
    #endregion
    
    #region Constructor

    public BuscarTodosUsuarioQueryHandler(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    #endregion
    
    #region Servico
    
    public async Task<List<UsuarioViewModel>?> Handle(BuscarTodosUsuarioQuery request, CancellationToken cancellationToken)
    {
        List<Usuario>? usuarioRecuperados = await _usuarioRepositorio.BuscarPorFiltro(usuario => usuario.Status == StatusPadrao.Ativo);
        if (usuarioRecuperados is null or { Count: 0 })
            return null;
        return usuarioRecuperados.Select(usuario => new UsuarioViewModel()
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Nome = usuario.Nome,
            Telefone = usuario.Telefone
        }).ToList();
    }
    #endregion
}