using System.Linq.Expressions;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using AgendaTelefonica.Dominio.Servicos;
using AutoFixture;
using Bogus;
using FakeItEasy;
using static FakeItEasy.A;
using Xunit;

namespace AgendaTelefonica.TesteUnit.Dominio.Servicos;

public class UsuarioServicosTeste
{
    #region Propriedades

    private readonly IFixture _fixture;
    private readonly Faker _faker;
    private readonly IUsuarioRepositorio _usuarioRepositorioMock;

    #endregion

    #region Construtor

    public UsuarioServicosTeste()
    {
        _faker = new Faker();
        _fixture = new Fixture();
        _usuarioRepositorioMock = A.Fake<IUsuarioRepositorio>();

    }
    #endregion

    #region Testes
    
    [Fact]
    public async Task CasoExistaOutroUsuarioComMesmoNome_QuandoVerificarExistencia_DeveRetornarTrue()
    {
        Usuario usuarioExistenteComMesmoNome = new Usuario(_faker.Person.FullName,
            _faker.Person.Email,
            _faker.Person.Phone);
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorFiltro(A<Expression<Func<Usuario, bool>>>._))!
            .Returns(Task.FromResult<List<Usuario>>([usuarioExistenteComMesmoNome]));
        UsuarioServicos servico = new UsuarioServicos(_usuarioRepositorioMock);
        bool existeOutroUsuario = await servico.ExisteUmUsuarioComAsMesmaInformacoes(usuarioExistenteComMesmoNome.Nome, _faker.Person.Email);
        Assert.True(existeOutroUsuario);
    }

    [Fact]
    public async Task CasoExistaOutroUsuarioComMesmoEmail_QuandoVerificarExistencia_DeveRetornarTrue()
    {
        Usuario usuarioExistenteComMesmoEmail = new Usuario(_faker.Person.FullName,
            _faker.Person.Email,
            _faker.Person.Phone);
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorFiltro(A<Expression<Func<Usuario, bool>>>._))!
            .Returns(Task.FromResult<List<Usuario>>([usuarioExistenteComMesmoEmail]));
        UsuarioServicos servico = new UsuarioServicos(_usuarioRepositorioMock);
        bool existeUmUsuarioComOmesmoEmail = await servico.ExisteUmUsuarioComAsMesmaInformacoes(_faker.Person.FullName,
            usuarioExistenteComMesmoEmail.Email);
        Assert.True(existeUmUsuarioComOmesmoEmail);
    }

    [Fact]
    public async Task DadoNaoExistaOutroUsuarioComMesmoNomeOuEmail_QuandoVerificarExistencia_DeveRetornarFalse()
    {
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorFiltro(A<Expression<Func<Usuario, bool>>>._))
            .Returns(Task.FromResult<List<Usuario>?>([]));
        UsuarioServicos servico = new UsuarioServicos(_usuarioRepositorioMock);
        bool existeUmUsuarioComAsMesmasInformacoes = await servico.ExisteUmUsuarioComAsMesmaInformacoes(_faker.Person.FullName, _faker.Person.Email);
        Assert.False(existeUmUsuarioComAsMesmasInformacoes);
    }

    [Fact]
    public async Task CasoSoExistaUmUsuarioComAsMesmasInformacoesEId_QuandoVerificarExistenciaDeOutroUsuario_DeveRetornarFalse()
    {
        Usuario usuario = new Usuario(_faker.Person.FullName,
            _faker.Person.Email, _faker.Person.Phone);
        typeof(Usuario).GetProperty("Id")?.SetValue(usuario, 10);
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorFiltro(A<Expression<Func<Usuario, bool>>>._))
            .Returns(Task.FromResult<List<Usuario>?>([usuario]));
        UsuarioServicos servico = new UsuarioServicos(_usuarioRepositorioMock);
        bool existemOutrosUsuariosMesmasInformacoes = await servico.ExisteUmUsuarioComAsMesmaInformacoes(usuario.Nome, usuario.Email, usuario.Id);
        Assert.False(existemOutrosUsuariosMesmasInformacoes);
    }

    [Fact]
    public async Task DadoExistaMaisDeUmUsuarioComMesmoNomeOuEmail_QuandoVerificarExistencia_DeveRetornarTrue()
    {
        Usuario usuario1 = new Usuario(_faker.Person.FullName,
            _faker.Person.Email,
            _faker.Person.Phone);
        typeof(Usuario).GetProperty("Id")?.SetValue(usuario1, 10);
        
        Usuario usuario2 = new Usuario(_faker.Person.FullName,
            _faker.Person.Email,
            _faker.Person.Phone); 
        typeof(Usuario).GetProperty("Id")?.SetValue(usuario1, 11);
        
        A.CallTo(() => _usuarioRepositorioMock.BuscarPorFiltro(A<Expression<Func<Usuario, bool>>>._))
            .Returns(Task.FromResult<List<Usuario>?>(new List<Usuario> { usuario1, usuario2 }));
        UsuarioServicos servico = new UsuarioServicos(_usuarioRepositorioMock);
        bool existemOutrosUsuariosComAsMesmasInformacoes = await servico.ExisteUmUsuarioComAsMesmaInformacoes(usuario1.Nome, usuario1.Email, 10);
        Assert.True(existemOutrosUsuariosComAsMesmasInformacoes);
    }
    
    #endregion
}