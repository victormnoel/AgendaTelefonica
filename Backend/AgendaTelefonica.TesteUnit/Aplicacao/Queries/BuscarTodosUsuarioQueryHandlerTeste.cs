using System.Linq.Expressions;
using AgendaTelefonica.Aplicacao.Queries.BuscarTodosUsuario;
using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Enums;
using AgendaTelefonica.Dominio.Interfaces;
using Bogus;
using FakeItEasy;

namespace AgendaTelefonica.TesteUnit.Aplicacao.Queries;

public class BuscarTodosUsuarioQueryHandlerTeste
{
    #region Propriedades

    private readonly IUsuarioRepositorio _usuarioRepositorioMock;
    private readonly Faker _faker;

    #endregion

    #region Construtor

    public BuscarTodosUsuarioQueryHandlerTeste()
    {
        _faker = new Faker();
        _usuarioRepositorioMock = A.Fake<IUsuarioRepositorio>();
    }
    
    #endregion

    #region Teste
    
    [Fact]
    public async Task DadoNenhumUsuarioAtivoSejaEncontrado_QuandoBuscarTodos_DeveRetornarNull()
    {
        BuscarTodosUsuarioQuery query = new BuscarTodosUsuarioQuery();
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorFiltro(A<Expression<Func<Usuario, bool>>>._))
            .Returns<List<Usuario>?>(null);
        BuscarTodosUsuarioQueryHandler queryHandler = new BuscarTodosUsuarioQueryHandler(_usuarioRepositorioMock);
        List<UsuarioViewModel>? usuariosRetornados = await queryHandler.Handle(query, CancellationToken.None);
        Assert.Null(usuariosRetornados);
    }

    [Fact]
    public async Task DadoListaVaziaDeUsuariosAtivosForRecuperada_QuandoBuscarTodos_DeveRetornarNull()
    {
        BuscarTodosUsuarioQuery query = new BuscarTodosUsuarioQuery();
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorFiltro(A<Expression<Func<Usuario, bool>>>._))
            .Returns(new List<Usuario>());
        BuscarTodosUsuarioQueryHandler queryHandler = new BuscarTodosUsuarioQueryHandler(_usuarioRepositorioMock);
        List<UsuarioViewModel>? usuariosRetornados = await queryHandler.Handle(query, CancellationToken.None);
        Assert.Null(usuariosRetornados);
    }

    [Fact]
    public async Task DadoExistamUsuariosAtivos_QuandoBuscarTodos_DeveRetornarListaDeUsuarioViewModel()
    {
        BuscarTodosUsuarioQuery query = new BuscarTodosUsuarioQuery();
        Usuario usuario1 = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        typeof(Usuario).GetProperty("Id")?.SetValue(usuario1, 1);
        typeof(Usuario).GetProperty("Status")?.SetValue(usuario1, StatusPadrao.Ativo);
        
        Usuario usuario2 = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        typeof(Usuario).GetProperty("Id")?.SetValue(usuario2, 2);
        typeof(Usuario).GetProperty("Status")?.SetValue(usuario2, StatusPadrao.Ativo);
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorFiltro(A<System.Linq.Expressions.Expression<System.Func<Usuario, bool>>>._))
            .Returns(new List<Usuario> { usuario1, usuario2 });
        BuscarTodosUsuarioQueryHandler queryHandler = new BuscarTodosUsuarioQueryHandler(_usuarioRepositorioMock);
        List<UsuarioViewModel>? usuarioRetornados = await queryHandler.Handle(query, System.Threading.CancellationToken.None);
        
        Assert.NotNull(usuarioRetornados);
        Assert.Contains(usuarioRetornados, viewModel =>
            viewModel.Id == usuario1.Id &&
            viewModel.Nome == usuario1.Nome &&
            viewModel.Email == usuario1.Email &&
            viewModel.Telefone == usuario1.Telefone);
        
        Assert.Contains(usuarioRetornados, viewModel =>
            viewModel.Id == usuario2.Id &&
            viewModel.Nome == usuario2.Nome &&
            viewModel.Email == usuario2.Email &&
            viewModel.Telefone == usuario2.Telefone);
    }
    
    #endregion
}