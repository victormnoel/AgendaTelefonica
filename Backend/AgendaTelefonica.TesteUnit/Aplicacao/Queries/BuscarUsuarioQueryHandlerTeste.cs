using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Aplicacao.Queries.BuscarUsuario;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Enums;
using AgendaTelefonica.Dominio.Interfaces;
using AutoFixture;
using Bogus;
using FakeItEasy;

namespace AgendaTelefonica.TesteUnit.Aplicacao.Queries;

public class BuscarUsuarioQueryHandlerTeste
{
    #region Propriedades

    private readonly IUsuarioRepositorio _usuarioRepositorioMock;
    private readonly IFixture _fixture;
    private readonly Faker _faker;

    #endregion

    #region Construtor

    public BuscarUsuarioQueryHandlerTeste()
    {
        _faker = new Faker();
        _fixture = new Fixture();
        _usuarioRepositorioMock = A.Fake<IUsuarioRepositorio>();
    }

    #endregion

    #region Testes
    
    [Fact]
    public async Task DadoQueUsuarioReferenteAoIdInformadoNaoSejaEncontrado_QuandoBuscar_DeveRetornarNull()
    {
        BuscarUsuarioQuery query = _fixture.Build<BuscarUsuarioQuery>()
            .With(q => q.Id, 1)
            .Create();
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(query.Id)).Returns<Usuario?>(null);
        BuscarUsuarioQueryHandler queryHandler = new BuscarUsuarioQueryHandler(_usuarioRepositorioMock);
        UsuarioViewModel? usuarioRetornado = await queryHandler.Handle(query, CancellationToken.None);
        Assert.Null(usuarioRetornado);
    }

    [Fact]
    public async Task DadoQueUsuarioReferenteAoIdInformadoSejaEncontradoComStatusDiferenteDeAtivo_QuandoBuscar_DeveRetornarNull()
    {
        BuscarUsuarioQuery query = _fixture.Build<BuscarUsuarioQuery>()
            .With(q => q.Id, 2)
            .Create();
        Usuario usuario = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        typeof(Usuario).GetProperty("Id")?.SetValue(usuario, query.Id);
        typeof(Usuario).GetProperty("Status")?.SetValue(usuario, StatusPadrao.Excluido);
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(query.Id)).Returns(usuario);
        BuscarUsuarioQueryHandler queryHandler = new BuscarUsuarioQueryHandler(_usuarioRepositorioMock);
        UsuarioViewModel? usuarioRetornado = await queryHandler.Handle(query, CancellationToken.None);
        Assert.Null(usuarioRetornado);
    }

    [Fact]
    public async Task DadoQueUsuarioReferenteAoIdInformadoSejaEncontradoComStatusAtivo_QuandoBuscar_DeveRetornarUsuarioViewModelCorreto()
    {
        BuscarUsuarioQuery query = _fixture.Build<BuscarUsuarioQuery>()
            .With(q => q.Id, 3)
            .Create();
        var usuario = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        typeof(Usuario).GetProperty("Id")?.SetValue(usuario, query.Id);
        typeof(Usuario).GetProperty("Status")?.SetValue(usuario, StatusPadrao.Ativo);
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorId(query.Id)).Returns(usuario);
        BuscarUsuarioQueryHandler queryHandlerhandler = new BuscarUsuarioQueryHandler(_usuarioRepositorioMock);
        UsuarioViewModel? usuarioRetornado = await queryHandlerhandler.Handle(query, CancellationToken.None);
        Assert.NotNull(usuarioRetornado);
        Assert.Equal(usuario.Id, usuarioRetornado.Id);
        Assert.Equal(usuario.Nome, usuarioRetornado.Nome);
        Assert.Equal(usuario.Email, usuarioRetornado.Email);
        Assert.Equal(usuario.Telefone, usuarioRetornado.Telefone);
    }
    
    #endregion
}