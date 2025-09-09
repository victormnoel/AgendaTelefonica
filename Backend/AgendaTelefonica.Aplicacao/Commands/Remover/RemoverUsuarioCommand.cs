using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Commands.Remover;

public class RemoverUsuarioCommand : IRequest<RetornoDaOperacaoViewModel>
{
    public int usuarioId { get; set; }
}