using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Commands.Atualizar;

public class AtualizarUsuarioCommand : IRequest<RetornoDaOperacaoViewModel>
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
}