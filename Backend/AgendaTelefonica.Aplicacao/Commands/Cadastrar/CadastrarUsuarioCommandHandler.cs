using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using MediatR;

namespace AgendaTelefonica.Aplicacao.Commands.Cadastrar;

public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, RetornoDaOperacaoViewModel>
{
    #region Propriedades
    
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IUsuarioServico _usuarioServico;
    
    #endregion
    
    #region Construtor

    public CadastrarUsuarioCommandHandler(IUsuarioRepositorio usuarioRepositorio, IUsuarioServico usuarioServico)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _usuarioServico = usuarioServico;
    }
    #endregion
    
    #region Servico
    
    public async Task<RetornoDaOperacaoViewModel> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        RetornoDaOperacaoViewModel retornoDaOperacao = new();
        bool usuarioJaExiste = await _usuarioServico.ExisteUmUsuarioComAsMesmaInformacoes(request.Nome, request.Email);

        if (usuarioJaExiste)
        {
             retornoDaOperacao.MensagemDeRetorno = "Não foi possível realizar o cadastro, pois já existe um usuário com as mesmas informações!";
             return retornoDaOperacao;
        }

        Usuario novoUsuario = new Usuario(request.Nome, request.Email, request.Telefone);
        bool usuarioCadastradoComSucesso = await _usuarioRepositorio.Cadastrar(novoUsuario);

        if (usuarioCadastradoComSucesso)
        {
            retornoDaOperacao.MensagemDeRetorno = "Usuário Cadastro com sucesso!";
            retornoDaOperacao.OperacaoRealizadaComSucesso = true;
        }
        else
            retornoDaOperacao.MensagemDeRetorno = "Ocorreu um erro inesperado no processo de cadastro! Por favor tente novamente.";

        return retornoDaOperacao;
    }
    
    #endregion
}