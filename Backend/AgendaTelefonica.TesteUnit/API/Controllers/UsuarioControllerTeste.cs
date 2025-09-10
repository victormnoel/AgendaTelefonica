using AgendaTelefonica.API.Controllers;
using AgendaTelefonica.Aplicacao.Commands.Atualizar;
using AgendaTelefonica.Aplicacao.Commands.Cadastrar;
using AgendaTelefonica.Aplicacao.Commands.Remover;
using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Aplicacao.Queries.BuscarTodosUsuario;
using AgendaTelefonica.Aplicacao.Queries.BuscarUsuario;
using AgendaTelefonica.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using AutoFixture;
using Bogus;

namespace AgendaTelefonica.TesteUnit.API.Controllers;

public class UsuarioControllerTeste
{
    #region Propriedades
    
    private readonly IMediator _mediatorMock;
    private readonly ILogRepositorio _logRepositorioMock;
    private readonly IFixture _fixture;
    private readonly Faker _faker;
    private readonly UsuarioController _controller;

    #endregion

    #region Construtor
    
    public UsuarioControllerTeste()
    {
        _faker = new Faker();
        _fixture = new Fixture();
        _mediatorMock = A.Fake<IMediator>();
        _logRepositorioMock = A.Fake<ILogRepositorio>();
        _controller = new UsuarioController(_mediatorMock, _logRepositorioMock);
    }
    #endregion

    #region Teste
    
    [Fact]
    public async Task BuscarUsuarioGet_QuandoUsuarioEncontrado_DeveRetornarOkObjectResult()
    {
        UsuarioViewModel usuarioViewModel = _fixture.Build<UsuarioViewModel>()
            .With(u => u.Id, 1)
            .With(u => u.Nome, _faker.Person.FullName)
            .With(u => u.Email, _faker.Person.Email)
            .With(u => u.Telefone, _faker.Person.Phone)
            .Create();
        
        A.CallTo(() => _mediatorMock.Send(A<BuscarUsuarioQuery>.That.Matches(q => q.Id == 1), A<CancellationToken>._))
            .Returns(usuarioViewModel);
        
        IActionResult retornoDaAcao = await _controller.BuscarUsuario(1);
        var statusCodeRetornado = Assert.IsType<OkObjectResult>(retornoDaAcao);
        Assert.Equal(usuarioViewModel, statusCodeRetornado.Value);
    }

    [Fact]
    public async Task BuscarUsuarioGet_QuandoUsuarioNaoEncontrado_DeveRetornarNotFound_()
    {
        A.CallTo(() => _mediatorMock.Send(A<BuscarUsuarioQuery>.That.Matches(q => q.Id == 2), A<CancellationToken>._))
            .Returns<UsuarioViewModel?>(null);
        IActionResult retornorDaAcao = await _controller.BuscarUsuario(2);
        var statusCodeRetornado = Assert.IsType<NotFoundObjectResult>(retornorDaAcao);
        Assert.Equal("Não foi possível encontrar o registro selecionado!", statusCodeRetornado.Value);
    }

    [Fact]
    public async Task BuscarTodosUsuarioGet__QuandoForEncontradosUsuarios_DeveRetornarOk()
    {
        List<UsuarioViewModel> usuariosRetornados = _fixture.CreateMany<UsuarioViewModel>(2).ToList();
        A.CallTo(() => _mediatorMock.Send(A<BuscarTodosUsuarioQuery>._, A<CancellationToken>._))
            .Returns(usuariosRetornados);
        IActionResult retornoDaAcao = await _controller.BuscarTodosUsuario();
        var statusCodeRetornado = Assert.IsType<OkObjectResult>(retornoDaAcao);
        Assert.Equal(usuariosRetornados, statusCodeRetornado.Value);
    }

    [Fact]
    public async Task BuscarTodosUsuarioGet_QuandoNenhumUsuarioEncontrado_DeveRetornarNotFound()
    {
        A.CallTo(() => _mediatorMock.Send(A<BuscarTodosUsuarioQuery>._, A<CancellationToken>._))
            .Returns((List<UsuarioViewModel>?)null);
        IActionResult retornoDaAcao = await _controller.BuscarTodosUsuario();
        var statusCodeRetornado = Assert.IsType<NotFoundObjectResult>(retornoDaAcao);
        Assert.Equal("Nenhum registro foi encontrado!", statusCodeRetornado.Value);
    }

    [Fact]
    public async Task DadoQueSejaSolicitadoOCadastroDoUsuario_QuandoOCadastroForRealizadoComSucesso_DeveRetornarOk()
    {
        CadastrarUsuarioCommand command = _fixture.Build<CadastrarUsuarioCommand>()
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        RetornoDaOperacaoViewModel retorno = new RetornoDaOperacaoViewModel { OperacaoRealizadaComSucesso = true, MensagemDeRetorno = "Sucesso!" };
        A.CallTo(() => _mediatorMock.Send(command, A<CancellationToken>._)).Returns(retorno);
        IActionResult retornoDaAcao = await _controller.CadastrarUsuario(command);
        var statusCodeRetornado = Assert.IsType<OkObjectResult>(retornoDaAcao);
        Assert.Equal(retorno.MensagemDeRetorno, statusCodeRetornado.Value);
    }

    [Fact]
    public async Task DadoQueSejaSolicitadoOCadastroDoUsuario_QuandoOcorreUmaFalharNoCadastro_DeveRetornarBadRequest()
    {
        CadastrarUsuarioCommand command = _fixture.Build<CadastrarUsuarioCommand>()
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        RetornoDaOperacaoViewModel retorno = new RetornoDaOperacaoViewModel { OperacaoRealizadaComSucesso = false, MensagemDeRetorno = "Erro!" };
        A.CallTo(() => _mediatorMock.Send(command, A<CancellationToken>._)).Returns(retorno);
        IActionResult retornoDaAcao = await _controller.CadastrarUsuario(command);
        var statusCodeRetornado = Assert.IsType<BadRequestObjectResult>(retornoDaAcao);
        Assert.Equal(retorno.MensagemDeRetorno, statusCodeRetornado.Value);
    }

    [Fact]
    public async Task DadoQueSejaSolicitadoAtualizacaoDoUsuario_QuandoAtualizacaoComSucesso_DeveRetornarNoContent()
    {
        var command = _fixture.Build<AtualizarUsuarioCommand>()
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        
        RetornoDaOperacaoViewModel retorno = new RetornoDaOperacaoViewModel { OperacaoRealizadaComSucesso = true, MensagemDeRetorno = "Atualizado!" };
        A.CallTo(() => _mediatorMock.Send(command, A<CancellationToken>._)).Returns(retorno);
        IActionResult retornoDaAcao = await _controller.AtualizarUsuario(1, command);
        Assert.IsType<NoContentResult>(retornoDaAcao);
    }

    [Fact]
    public async Task DadoQueSejaSolicitadoAtualizacaoDoUsuario_QuandoOcorreUmaFalha_DeveRetornarBadRequest()
    {
        AtualizarUsuarioCommand command = _fixture.Build<AtualizarUsuarioCommand>()
            .With(c => c.Nome, _faker.Person.FullName)
            .With(c => c.Email, _faker.Person.Email)
            .With(c => c.Telefone, _faker.Person.Phone)
            .Create();
        RetornoDaOperacaoViewModel retorno = new RetornoDaOperacaoViewModel { OperacaoRealizadaComSucesso = false, MensagemDeRetorno = "Erro!" };
        A.CallTo(() => _mediatorMock.Send(command, A<CancellationToken>._)).Returns(retorno);
        IActionResult retornoDaAcao = await _controller.AtualizarUsuario(1, command);
        var statusCodeRetornado = Assert.IsType<BadRequestObjectResult>(retornoDaAcao);
        Assert.Equal(retorno.MensagemDeRetorno, statusCodeRetornado.Value);
    }

    [Fact]
    public async Task DadoQueSejaSolicitadoARemocaoDoUsuario_QuandoARemocaoERealizadaComSucesso_DeveRetornarNoContent()
    {
        RetornoDaOperacaoViewModel retorno = new RetornoDaOperacaoViewModel { OperacaoRealizadaComSucesso = true, MensagemDeRetorno = "Removido!" };
        A.CallTo(() => _mediatorMock.Send(A<RemoverUsuarioCommand>.That.Matches(c => c.usuarioId == 1), A<CancellationToken>._)).Returns(retorno);
        IActionResult retornoDaAcao = await _controller.RemoverUsuario(1);
        Assert.IsType<NoContentResult>(retornoDaAcao);
    }

    [Fact]
    public async Task DadoQueSejaSolicitadoARemocaoDoUsuario_QuandoRemocaoFalhar_DeveRetornarBadRequest()
    {
        RetornoDaOperacaoViewModel retorno = new RetornoDaOperacaoViewModel { OperacaoRealizadaComSucesso = false, MensagemDeRetorno = "Erro!" };
        A.CallTo(() => _mediatorMock.Send(A<RemoverUsuarioCommand>.That.Matches(c => c.usuarioId == 2), A<CancellationToken>._)).Returns(retorno);
        IActionResult retornoDaAcao = await _controller.RemoverUsuario(2);
        var statusCodeRetornado = Assert.IsType<BadRequestObjectResult>(retornoDaAcao);
        Assert.Equal(retorno.MensagemDeRetorno, statusCodeRetornado.Value);
    }
    #endregion
}