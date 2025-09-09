using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Commands.Remover;

public class RemoverUsuarioCommandHandler : IRequestHandler<RemoverUsuarioCommand, RetornoDaOperacaoViewModel>
{
    #region Propriedades
    
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    
    #endregion
    
    #region Construtor

    public RemoverUsuarioCommandHandler(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    #endregion
    
    #region Servico
    
    public async Task<RetornoDaOperacaoViewModel> Handle(RemoverUsuarioCommand request, CancellationToken cancellationToken)
    {
        RetornoDaOperacaoViewModel retornoDaOperacao = new();
        Usuario? usuarioParaRemover = await _usuarioRepositorio.BuscarPorId(request.usuarioId);

        if (usuarioParaRemover == null)
        {
            retornoDaOperacao.MensagemDeRetorno = "Não foi possível identificar o usuário selecionado! Por favor, tente novamente.";
            return retornoDaOperacao;
        }
        
        usuarioParaRemover.RemoverUsuario();
        bool usuarioRemovidoComSucesso = await _usuarioRepositorio.Atualizar(usuarioParaRemover);
        if (!usuarioRemovidoComSucesso)
            retornoDaOperacao.MensagemDeRetorno = "Ocorre um erro inesperado durante o processo de remoção! Por favor, tente novamente";
        else
        {
            retornoDaOperacao.MensagemDeRetorno = "Usuário removido com sucesso!";
            retornoDaOperacao.OperacaoRealizadaComSucesso = true;
        }
        return retornoDaOperacao;
    }
    #endregion
}