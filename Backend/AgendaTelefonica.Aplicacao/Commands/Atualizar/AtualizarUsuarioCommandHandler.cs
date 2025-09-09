using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Commands.Atualizar;

public class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, RetornoDaOperacaoViewModel>
{
    #region Propriedades

    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IUsuarioServico _usuarioServico;

    #endregion

    #region Construtor

    public AtualizarUsuarioCommandHandler(IUsuarioRepositorio usuarioRepositorio, IUsuarioServico usuarioServico)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _usuarioServico = usuarioServico;
    }

    #endregion

    #region Servico

    public async Task<RetornoDaOperacaoViewModel> Handle(AtualizarUsuarioCommand request,
        CancellationToken cancellationToken)
    {
        RetornoDaOperacaoViewModel retornoDaOperacao = new();
        Usuario? usuarioParaAtualizar = await _usuarioRepositorio.BuscarPorId(request.Id);

        if (usuarioParaAtualizar == null)
        {
            retornoDaOperacao.MensagemDeRetorno = "Não foi possível identificar o usuário selecionado! Por favor, tente novamente.";
            return retornoDaOperacao;
        }

        bool existeOutroUsuarioComAsMesmasInformacoes = await _usuarioServico.ExisteUmUsuarioComAsMesmaInformacoes(request.Nome, request.Email, request.Id);

        if (existeOutroUsuarioComAsMesmasInformacoes)
            retornoDaOperacao.MensagemDeRetorno = "Não foi possível atualizar o registro, pois já existe um usuário com as mesmas informações!";
        else
        {
            usuarioParaAtualizar.AtualizarInformacoesDoUsuario(request.Nome, request.Email, request.Telefone);
            bool usuarioAtualizadoComSucesso = await _usuarioRepositorio.Atualizar(usuarioParaAtualizar);
            if (usuarioAtualizadoComSucesso)
            {
                retornoDaOperacao.MensagemDeRetorno = "Usuário atualizado com Sucesso!";
                retornoDaOperacao.OperacaoRealizadaComSucesso = true;
            }
            else
                retornoDaOperacao.MensagemDeRetorno = "Ocorreu um erro durante o processo de atualização! Por favor, tente novamente.";
        }
        
        return retornoDaOperacao;
    }

    #endregion
}