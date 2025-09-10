using System.Text.Json;
using AgendaTelefonica.API.Auxiliares.Modelos;
using AgendaTelefonica.Aplicacao.Commands.Atualizar;
using AgendaTelefonica.Aplicacao.Commands.Cadastrar;
using AgendaTelefonica.Aplicacao.Commands.Remover;
using AgendaTelefonica.Aplicacao.Modelos.ViewModels;
using AgendaTelefonica.Aplicacao.Queries.BuscarTodosUsuario;
using AgendaTelefonica.Aplicacao.Queries.BuscarUsuario;
using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTelefonica.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    #region Propriedades

    private readonly IMediator _mediator;
    private readonly ILogRepositorio _logRepositorio;
    
    const string MENSAGEM_ERRO = "Ocorreu um erro inesperado! Por favor, tente novamente.";
    #endregion
    
    #region Construtor

    public UsuarioController(IMediator mediator, ILogRepositorio logRepositorio)
    {
        _mediator = mediator;
        _logRepositorio = logRepositorio;
    }
    #endregion
    
    #region Endpoints

    [HttpGet("buscar/{id}")]
    public async Task<IActionResult> BuscarUsuario(int id)
    {
        try
        {
           UsuarioViewModel? usuarioRetornado = await _mediator.Send(new BuscarUsuarioQuery() { Id = id });
           return usuarioRetornado != null ? Ok(usuarioRetornado) : NotFound("Não foi possível encontrar o registro selecionado!");
        }
        catch (Exception excecao)
        {
            Log logParaSalvar = new Log(nameof(BuscarUsuario),
                JsonSerializer.Serialize(new BuscarUsuarioQuery() { Id = id }),
                MENSAGEM_ERRO);
            logParaSalvar.RegistrarExcecao(JsonSerializer.Serialize(new ModeloDeExcecao(excecao)));
            await _logRepositorio.CadadastrarLog(logParaSalvar);
            return BadRequest(MENSAGEM_ERRO);
        }
    }

    [HttpGet("buscarTodos")]
    public async Task<IActionResult> BuscarTodosUsuario()
    {
        try
        {
            List<UsuarioViewModel>? usuarioRetornados = await _mediator.Send(new BuscarTodosUsuarioQuery());
            return usuarioRetornados?.Count > 0 ? Ok(usuarioRetornados) : NotFound("Nenhum registro foi encontrado!");
        }
        catch (Exception excecao)
        {
            Log logParaSalvar = new Log(nameof(BuscarUsuario),
                nameof(BuscarTodosUsuarioQuery),
                MENSAGEM_ERRO);
            logParaSalvar.RegistrarExcecao(JsonSerializer.Serialize(new ModeloDeExcecao(excecao)));
            await _logRepositorio.CadadastrarLog(logParaSalvar);
            return BadRequest(MENSAGEM_ERRO);
        }
    }

    [HttpPost("Cadastrar")]
    public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioCommand dadosDoUsuario)
    {
        Log logParaSalvar = new Log();

        try
        {
            RetornoDaOperacaoViewModel retornoDaOperacao = await _mediator.Send(dadosDoUsuario);
            logParaSalvar.RetornoDaAcao = retornoDaOperacao.MensagemDeRetorno;
            return retornoDaOperacao.OperacaoRealizadaComSucesso
                ? Ok(retornoDaOperacao.MensagemDeRetorno)
                : BadRequest(retornoDaOperacao.MensagemDeRetorno);
        }
        catch (Exception excecao)
        {
            logParaSalvar.RegistrarExcecao(JsonSerializer.Serialize(new ModeloDeExcecao(excecao)));
            return BadRequest(MENSAGEM_ERRO);
        }
        finally
        {
            logParaSalvar.InserirInformacoes(
                nameof(CadastrarUsuario),
                JsonSerializer.Serialize(dadosDoUsuario));
            await _logRepositorio.CadadastrarLog(logParaSalvar);
        }
    }

    [HttpPut("Atualizar/{id}")]
    public async Task<IActionResult> AtualizarUsuario(int id, AtualizarUsuarioCommand dadosAtualizados)
    {
        Log logDaAcao = new Log();

        try
        {
            dadosAtualizados.Id = id;
            RetornoDaOperacaoViewModel retornoDaOperacao = await _mediator.Send(dadosAtualizados);
            logDaAcao.RetornoDaAcao = retornoDaOperacao.MensagemDeRetorno;
            return retornoDaOperacao.OperacaoRealizadaComSucesso
                ? NoContent()
                : BadRequest(retornoDaOperacao.MensagemDeRetorno);
        }
        catch (Exception excecao)
        {
            logDaAcao.RegistrarExcecao(JsonSerializer.Serialize(new ModeloDeExcecao(excecao)));
            return BadRequest(MENSAGEM_ERRO);
        }
        finally
        {
            logDaAcao.InserirInformacoes(nameof(AtualizarUsuario), JsonSerializer.Serialize(dadosAtualizados));
            await _logRepositorio.CadadastrarLog(logDaAcao);
        }
    }

    [HttpPatch("Remover/{id}")]
    public async Task<IActionResult> RemoverUsuario(int id)
    {
        Log logDaAcao = new Log();

        try
        {
            RetornoDaOperacaoViewModel retornoDaOperacao =
                await _mediator.Send(new RemoverUsuarioCommand() { usuarioId = id });
            logDaAcao.RetornoDaAcao = retornoDaOperacao.MensagemDeRetorno;
            return retornoDaOperacao.OperacaoRealizadaComSucesso
                ? Ok(retornoDaOperacao.MensagemDeRetorno)
                : BadRequest(retornoDaOperacao.MensagemDeRetorno);
        }
        catch (Exception excecao)
        {
            logDaAcao.RegistrarExcecao(JsonSerializer.Serialize(new ModeloDeExcecao(excecao)));
            return BadRequest(MENSAGEM_ERRO);
        }
        finally
        {
            logDaAcao.InserirInformacoes(nameof(AtualizarUsuario),
                JsonSerializer.Serialize(new RemoverUsuarioCommand() { usuarioId = id }));
            await _logRepositorio.CadadastrarLog(logDaAcao);
        }
    }
    
    #endregion
}