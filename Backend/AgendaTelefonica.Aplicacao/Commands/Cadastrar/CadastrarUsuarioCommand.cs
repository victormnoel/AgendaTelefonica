using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Commands.Cadastrar;

public class CadastrarUsuarioCommand : IRequest<RetornoDaOperacaoViewModel>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
}