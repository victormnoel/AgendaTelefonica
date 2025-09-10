using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Enums;
using AgendaTelefonica.Dominio.Excecoes;
using AgendaTelefonica.Dominio.Interfaces;
using AutoFixture;
using Bogus;

namespace AgendaTelefonica.TesteUnit.Dominio.Entidades;

public class UsuarioTeste
{
    #region Propriedades

    private readonly IFixture _fixture;
    private readonly Faker _faker;
    
    #endregion

    #region Construtor

    public UsuarioTeste()
    {
        _fixture = new Fixture();
        _faker = new Faker();
    }
    #endregion

    #region Testes

    [Fact]
    public void SendoAsInformacoesCorretas_QuandoUmUsuarioForCriado_DeveRetornarUmNovoUsuarioAtivo()
    {
        string nome = _faker.Person.FullName;
        string email = _faker.Person.Email;
        string telefone = _faker.Person.Phone;
        var usuarioCriado = new Usuario(nome, email, telefone);
        
        Assert.NotNull(usuarioCriado);
        Assert.Equal(usuarioCriado.Email, email);
        Assert.Equal(usuarioCriado.Nome, nome);
        Assert.Equal(usuarioCriado.Telefone, telefone);
        Assert.IsType<Usuario>(usuarioCriado);
        Assert.True(usuarioCriado.Status == StatusPadrao.Ativo);
    }

    [Fact]
    public void SendoONomeInformadoInvalido_QuandoUmUsuarioForCriado_DeveRetornarNomeInvalidoException()
    {
        string nome = string.Empty;
        string email = _faker.Person.Email;
        string telefone = _faker.Person.Phone;
        Assert.Throws<NomeInvalidoException>(() => new Usuario(nome, email, telefone));
    }
    
    [Fact]
    public void SendoOEmailInformadoInvalido_QuandoUmUsuarioForCriado_DeveRetornarEmailInvalidoException()
    {
        string nome = _faker.Person.FullName;
        string email = string.Empty;
        string telefone = _faker.Person.Phone;
        Assert.Throws<EmailInvalidoException>(() => new Usuario(nome, email, telefone));
    }

    [Fact] 
    public void SendoOTelefoneInformadoInvalido_QuandoUmUsuarioForCriado_DeveRetornarTelefoneInvalidoException()
    {
        string nome = _faker.Person.FullName;
        string email = _faker.Person.Email;
        string telefone = string.Empty;
        Assert.Throws<TelefoneInvalidoException>(() => new Usuario(nome, email, telefone));
    }

    [Fact]
    public void DadasInformacoesCorretas_QuandoUmUsuarioForAtualizado_DeveAtualizarAsInformacoesDoUsuario()
    {
        Usuario usuarioParaAtualizar = new Usuario(_faker.Person.FullName,
            _faker.Person.Email, 
            _faker.Person.Phone);
        string nomeAtualizado = _faker.Person.UserName;
        string emailAtualizado = _faker.Person.Email;
        string telefoneAtualizado = _faker.Person.Phone;
        usuarioParaAtualizar.AtualizarInformacoesDoUsuario(nomeAtualizado, emailAtualizado, telefoneAtualizado);
        
        Assert.Equal(usuarioParaAtualizar.Nome, nomeAtualizado);
        Assert.Equal(usuarioParaAtualizar.Telefone, telefoneAtualizado);
        Assert.Equal(usuarioParaAtualizar.Email, emailAtualizado);
    }

    [Fact]
    public void TendoOStatusDiferenteDeAtivo_QuandoForExecutadaOMetodoParaRemoverOUsuario_DeveRetornarUsuarioJaExcluidoException()
    {
        Usuario usuarioParaRemover = new Usuario(_faker.Person.FullName,
            _faker.Person.Email,
            _faker.Person.Phone);
        usuarioParaRemover.RemoverUsuario(); // trocando status do usuario
        Assert.Throws<UsuarioJaExcluidoException>(() => usuarioParaRemover.RemoverUsuario());
    }

    [Fact]
    public void TendoAsInformacoesEspacos_QuandoUmUsuarioForCriado_DeveRemoverOsEspacosDesnecessarios()
    {
        string nomeDo = "  " + _faker.Person.FullName + "  ";
        string email = "  " + _faker.Person.Email + "  ";
        string telefone = "  " + _faker.Person.Phone + "  ";
        Usuario usuario = new Usuario(nomeDo, email, telefone);
        Assert.Equal(_faker.Person.FullName.Trim().Length, usuario.Nome.Trim().Length);
        Assert.False(usuario.Nome.StartsWith(" ") || usuario.Nome.EndsWith(" "));
        Assert.False(usuario.Email.StartsWith(" ") || usuario.Email.EndsWith(" "));
        Assert.False(usuario.Telefone.StartsWith(" ") || usuario.Telefone.EndsWith(" "));
    }

    [Fact]
    public void DadoOUsuarioForAtualizado_QuandoRealizarAtualizacao_StatusDevePermanecerAtivo()
    {
        Usuario usuario = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        usuario.AtualizarInformacoesDoUsuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        Assert.Equal(StatusPadrao.Ativo, usuario.Status);
    }

    [Fact]
    public void DadoUsuarioForRemovido_QuandoARemocaoForRealizada_StatusDeveSerAtualizadoParaExcluido()
    {
        Usuario usuario = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        usuario.RemoverUsuario();
        Assert.Equal(StatusPadrao.Excluido, usuario.Status);
    }

    [Fact]
    public void TendoOsDadosAtualizadosEspacoesDesnecessario_QuandoAtualizarOUsuario_DeveRemoverOsEspacosDesnecessariosDosDados()
    {
        Usuario usuario = new Usuario(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone);
        string nomeDo = "  " + _faker.Person.FullName + "  ";
        string email = "  " + _faker.Person.Email + "  ";
        string telefone = "  " + _faker.Person.Phone + "  ";
        usuario.AtualizarInformacoesDoUsuario(nomeDo, email, telefone);
        Assert.False(usuario.Nome.StartsWith(" ") || usuario.Nome.EndsWith(" "));
        Assert.False(usuario.Email.StartsWith(" ") || usuario.Email.EndsWith(" "));
        Assert.False(usuario.Telefone.StartsWith(" ") || usuario.Telefone.EndsWith(" "));
    }

    [Fact]
    public void DadoUmUsuarioSejaInstanciadoPeloConstrutorPadrao_QuandoOUsuarioForCriado_OsCamposDeveTerOValorPadrao()
    {
        Usuario usuario = new Usuario();
        
        Assert.Null(usuario.Nome);
        Assert.Null(usuario.Telefone);
        Assert.Null(usuario.Email);
        Assert.Equal(default, usuario.DataDaAtualizacao);
        Assert.Equal(default, usuario.DataDaAtualizacao);
    }
    #endregion
}