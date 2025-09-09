using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Enums;
using AgendaTelefonica.Dominio.Interfaces;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Queries.BuscarUsuario;

public class BuscarUsuarioQueryHandler : IRequestHandler<BuscarUsuarioQuery, UsuarioViewModel?>
{
    #region Propriedades

    private readonly IUsuarioRepositorio _usuarioRepositorio;
    #endregion
    
    #region Construtor

    public BuscarUsuarioQueryHandler(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    #endregion
    
    #region Servico
    
    public async Task<UsuarioViewModel?> Handle(BuscarUsuarioQuery request, CancellationToken cancellationToken)
    {
        Usuario? usuarioComOIdInformado = await _usuarioRepositorio.BuscarPorId(request.Id);
        if (usuarioComOIdInformado is not { Status: StatusPadrao.Ativo })
            return null;
        return new UsuarioViewModel()
        {
            Id = usuarioComOIdInformado.Id,
            Email = usuarioComOIdInformado.Email,
            Nome = usuarioComOIdInformado.Nome,
            Telefone = usuarioComOIdInformado.Telefone
        };
    }
    #endregion
    
    
}