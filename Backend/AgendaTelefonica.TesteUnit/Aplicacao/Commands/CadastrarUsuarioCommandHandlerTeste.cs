using AgendaTelefonica.Aplicacao.Commands.Cadastrar;
using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using AutoFixture;
using Bogus;
using FakeItEasy;

namespace AgendaTelefonica.TesteUnit.Aplicacao.Commands;

public class CadastrarUsuarioCommandHandlerTeste
{
    #region Propriedades

    private readonly IUsuarioRepositorio _usuarioRepositorioMock;
    private readonly IUsuarioServico _usuarioServicoMock;
    private readonly IFixture _fixture;
    private readonly Faker _faker;

    #endregion

    #region Construtor

    public CadastrarUsuarioCommandHandlerTeste()
    {
        _faker = new Faker();
        _fixture = new Fixture();
        _usuarioRepositorioMock = A.Fake<IUsuarioRepositorio>();
        _usuarioServicoMock = A.Fake<IUsuarioServico>();
    }

    #endregion

    #region Testes

    [Fact]
    public async Task DadoOUsuarioInformadoJaExista_QuandoOCadastroForExecutado_DeveRetornarMensagemDeErro()
    {
        CadastrarUsuarioCommand command = _fixture.Build<CadastrarUsuarioCommand>()
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        A.CallTo(() => _usuarioServicoMock.ExisteUmUsuarioComAsMesmaInformacoes(command.Nome, command.Email, null))
            .Returns(true);
        CadastrarUsuarioCommandHandler commandhandler = new CadastrarUsuarioCommandHandler(_usuarioRepositorioMock, _usuarioServicoMock);
        RetornoDaOperacaoViewModel retorno = await commandhandler.Handle(command, CancellationToken.None);
        Assert.False(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Não foi possível realizar o cadastro, pois já existe um usuário com as mesmas informações!", retorno.MensagemDeRetorno);
    }

    [Fact]
    public async Task CasoOcorraUmErroInesperadoDuranteAPersistencia_QuandoOServicoDeCadastroFalhar_DeveRetornarMensagemDeErroGenerica()
    {
        CadastrarUsuarioCommand command = _fixture.Build<CadastrarUsuarioCommand>()
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        A.CallTo(() => _usuarioServicoMock.ExisteUmUsuarioComAsMesmaInformacoes(command.Nome, command.Email,null))
            .Returns(false);
        A.CallTo(() => _usuarioRepositorioMock.Cadastrar(A<Usuario>._)).Returns(false);
        CadastrarUsuarioCommandHandler commandhandler = new CadastrarUsuarioCommandHandler(_usuarioRepositorioMock, _usuarioServicoMock);
        RetornoDaOperacaoViewModel retorno = await commandhandler.Handle(command, CancellationToken.None);
        Assert.False(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Ocorreu um erro inesperado no processo de cadastro! Por favor tente novamente.", retorno.MensagemDeRetorno);
    }

    [Fact]
    public async Task DadoUsuarioNovoSejaInformado_QuandoCadastroForRealizadoComSucesso_DeveRetornarMensagemDeSucesso()
    {
        CadastrarUsuarioCommand command = _fixture.Build<CadastrarUsuarioCommand>()
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        A.CallTo(() => _usuarioServicoMock.ExisteUmUsuarioComAsMesmaInformacoes(command.Nome, command.Email,null))
            .Returns(false);
        A.CallTo(() => _usuarioRepositorioMock.Cadastrar(A<Usuario>._)).Returns(true);
        CadastrarUsuarioCommandHandler commandhandler = new CadastrarUsuarioCommandHandler(_usuarioRepositorioMock, _usuarioServicoMock);
        RetornoDaOperacaoViewModel retorno = await commandhandler.Handle(command, CancellationToken.None);
        Assert.True(retorno.OperacaoRealizadaComSucesso);
        Assert.Equal("Usuário Cadastro com sucesso!", retorno.MensagemDeRetorno);
    }

    #endregion
}