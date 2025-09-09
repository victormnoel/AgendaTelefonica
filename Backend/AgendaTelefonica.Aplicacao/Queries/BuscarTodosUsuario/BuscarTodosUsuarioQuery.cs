using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Queries.BuscarTodosUsuario;

public class BuscarTodosUsuarioQuery : IRequest<List<UsuarioViewModel>?>
{ }