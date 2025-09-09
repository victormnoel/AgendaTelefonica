using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Queries.BuscarUsuario;

public class BuscarUsuarioQuery : IRequest<UsuarioViewModel?>
{
    public int Id { get; set; }
}