using AgendaTelefonica.Aplicacao.Commands.Atualizar;
using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using AutoFixture;
using Bogus;
using FakeItEasy;

namespace AgendaTelefonica.TesteUnit.Aplicacao.Commands;

public class AtualizarUsuarioCommandHandlerTeste
{
    #region Propriedades

    private readonly IUsuarioRepositorio _usuarioRepositorioMock;
    private readonly IUsuarioServico _usuarioServicoMock;
    private readonly IFixture _fixture;
    private readonly Faker _faker;

    #endregion

    #region Construtor

    public AtualizarUsuarioCommandHandlerTeste()
    {
        _faker = new Faker();
        _fixture = new Fixture();
        _usuarioRepositorioMock = A.Fake<IUsuarioRepositorio>();
        _usuarioServicoMock = A.Fake<IUsuarioServico>();
    }

    #endregion

    #region Testes
    
    [Fact]
    public async Task CasoOUsuarioInformadoNaoEncontrado_QuandoAOperacaoDeAtualizacaoEstiverEmExecucao_DeveRetornarMensagemDeErro()
    {
        AtualizarUsuarioCommand command = _fixture.Build<AtualizarUsuarioCommand>()
            .With(c => c.Id, 1)
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(command.Id)).Returns<Usuario?>(null);
        AtualizarUsuarioCommandHandler commadHandler = new AtualizarUsuarioCommandHandler(_usuarioRepositorioMock, _usuarioServicoMock);
        RetornoDaOperacaoViewModel retorno = await commadHandler.Handle(command, CancellationToken.None);
        Assert.False(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Não foi possível identificar o usuário selecionado! Por favor, tente novamente.", retorno.MensagemDeRetorno);
    }

    [Fact]
    public async Task DadoNomeOuEmailInformadosIguaisADeOutroUsuario_QuandoAOperacaoDeAtualizacaoEstiverEmExecucao_DeveRetornarMensagemDeErro()
    {
        AtualizarUsuarioCommand command = _fixture.Build<AtualizarUsuarioCommand>()
            .With(c => c.Id, 2)
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        
        Usuario usuarioParaAtualizar = new Usuario(command.Nome, command.Email, command.Telefone);
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(command.Id)).Returns(usuarioParaAtualizar);
        A.CallTo(() => _usuarioServicoMock.ExisteUmUsuarioComAsMesmaInformacoes(command.Nome, command.Email, command.Id)).Returns(true);
        
        AtualizarUsuarioCommandHandler commadHandler = new AtualizarUsuarioCommandHandler(_usuarioRepositorioMock, _usuarioServicoMock);
        RetornoDaOperacaoViewModel retorno = await commadHandler.Handle(command, CancellationToken.None);
        Assert.False(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Não foi possível atualizar o registro, pois já existe um usuário com as mesmas informações!", retorno.MensagemDeRetorno);
    }

    [Fact]
    public async Task OcorrerUmErroDuranteAPersistencia_QuandoAOperacaoDeAtualizacaoEstiverEmExecucao_DeveRetornarMensagemDeErroGenerica()
    {
        AtualizarUsuarioCommand command = _fixture.Build<AtualizarUsuarioCommand>()
            .With(c => c.Id, 3)
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        Usuario usuarioParaAtualizar = new Usuario(command.Nome, command.Email, command.Telefone);
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(command.Id)).Returns(usuarioParaAtualizar);
        A.CallTo(() => _usuarioServicoMock.ExisteUmUsuarioComAsMesmaInformacoes(command.Nome, command.Email, command.Id)).Returns(false);
        A.CallTo(() => _usuarioRepositorioMock.Atualizar(usuarioParaAtualizar)).Returns(false);
        AtualizarUsuarioCommandHandler commadHandler = new AtualizarUsuarioCommandHandler(_usuarioRepositorioMock, _usuarioServicoMock);
        RetornoDaOperacaoViewModel retorno = await commadHandler.Handle(command, CancellationToken.None);
        Assert.False(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Ocorreu um erro durante o processo de atualização! Por favor, tente novamente.", retorno.MensagemDeRetorno);
    }

    [Fact]
    public async Task DadoUsuarioAtualizadoComSucesso_QuandoAtualizar_DeveRetornarMensagemDeSucesso()
    {
        AtualizarUsuarioCommand command = _fixture.Build<AtualizarUsuarioCommand>()
            .With(c => c.Id, 4)
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        
        Usuario usuarioParaAtualizar = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(command.Id)).Returns(usuarioParaAtualizar);
        A.CallTo(() => _usuarioServicoMock.ExisteUmUsuarioComAsMesmaInformacoes(A<string>._, A<string>._, A<int>._)).Returns(false);
        usuarioParaAtualizar.AtualizarInformacoesDoUsuario(command.Nome, command.Email, command.Telefone);
        A.CallTo(() => _usuarioRepositorioMock.Atualizar(usuarioParaAtualizar)).Returns(true);
        
        AtualizarUsuarioCommandHandler commadHandler = new AtualizarUsuarioCommandHandler(_usuarioRepositorioMock, _usuarioServicoMock);
        RetornoDaOperacaoViewModel retorno = await commadHandler.Handle(command, CancellationToken.None);
        Assert.True(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Usuário atualizado com Sucesso!", retorno.MensagemDeRetorno);
    }
    
    #endregion
}