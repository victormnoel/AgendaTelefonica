using AgendaTelefonica.Aplicacao.Commands.Remover;
using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using AutoFixture;
using Bogus;
using FakeItEasy;

namespace AgendaTelefonica.TesteUnit.Aplicacao.Commands;

public class RemoverUsuarioCommandHandlerTeste
{
    #region Propriedades

    private readonly IUsuarioRepositorio _usuarioRepositorioMock;
    private readonly IFixture _fixture;
    private readonly Faker _faker;

    #endregion

    #region Construtor

    public RemoverUsuarioCommandHandlerTeste()
    {
        _faker = new Faker();
        _fixture = new Fixture();
        _usuarioRepositorioMock = A.Fake<IUsuarioRepositorio>();
    }

    #endregion

    #region Teste
    
    [Fact]
    public async Task DadoQueUsuarioReferenteAoIdInformadoNaoSejaEncontrado_QuandoARemocaoEstiverSendoExecutada_DeveRetornarMensagemDeErro()
    {
        RemoverUsuarioCommand command = _fixture.Build<RemoverUsuarioCommand>()
            .With(c => c.usuarioId, 1)
            .Create();
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(command.usuarioId)).Returns<Usuario?>(null);
        RemoverUsuarioCommandHandler commandHandler = new RemoverUsuarioCommandHandler(_usuarioRepositorioMock);
        RetornoDaOperacaoViewModel retorno = await commandHandler.Handle(command, CancellationToken.None);
        Assert.False(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Não foi possível identificar o usuário selecionado! Por favor, tente novamente.", retorno.MensagemDeRetorno);
    }

    [Fact]
    public async Task DadoQueOcorreUmErroAoAtualizarOStatusDoUsuario_QuandoARemocaoEstiverSendoExecutada_DeveRetornarMensagemDeErroGenerica()
    {
        RemoverUsuarioCommand command = _fixture.Build<RemoverUsuarioCommand>()
            .With(c => c.usuarioId, 2)
            .Create();
        Usuario usuarioParaRemover = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(command.usuarioId)).Returns(usuarioParaRemover);
        A.CallTo(() => _usuarioRepositorioMock.Atualizar(usuarioParaRemover)).Returns(false);
        RemoverUsuarioCommandHandler commandHandler = new RemoverUsuarioCommandHandler(_usuarioRepositorioMock);
        RetornoDaOperacaoViewModel retorno = await commandHandler.Handle(command, CancellationToken.None);
        Assert.False(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Ocorre um erro inesperado durante o processo de remoção! Por favor, tente novamente", retorno.MensagemDeRetorno);
    }

    [Fact]
    public async Task DadoARemocaoOcorraComSucesso_QuandoRemoverForRealizada_DeveRetornarMensagemDeSucesso()
    {
        RemoverUsuarioCommand command = _fixture.Build<RemoverUsuarioCommand>()
            .With(c => c.usuarioId, 3)
            .Create();
        Usuario usuarioParaRemover = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(command.usuarioId)).Returns(usuarioParaRemover);
        A.CallTo(() => _usuarioRepositorioMock.Atualizar(usuarioParaRemover)).Returns(true);
        RemoverUsuarioCommandHandler commandHandler = new RemoverUsuarioCommandHandler(_usuarioRepositorioMock);
        RetornoDaOperacaoViewModel retorno = await commandHandler.Handle(command, CancellationToken.None);
        Assert.True(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Usuário removido com sucesso!", retorno.MensagemDeRetorno);
    }
    
    #endregion
}