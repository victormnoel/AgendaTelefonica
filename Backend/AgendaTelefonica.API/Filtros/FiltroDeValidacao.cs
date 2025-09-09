using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgendaTelefonica.API.Filtros;

public class FiltroDeValidacao : IActionFilter
{
    #region Propriedades

    private readonly IServiceProvider _serviceProvider;

    #endregion
    
    #region Construtor
    public FiltroDeValidacao(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    #endregion

    #region Acoes
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var erros = new List<string>();

        foreach (var parametro in context.ActionArguments)
        {
            if (parametro.Value == null) continue;
            var tipoValidador = typeof(IValidator<>).MakeGenericType(parametro.Value.GetType());

            if (_serviceProvider.GetService(tipoValidador) is not IValidator validador) continue;
            var resultado = validador.Validate(new ValidationContext<object>(parametro.Value));

            if (!resultado.IsValid)
            {
                erros.AddRange(resultado.Errors.Select(e => e.ErrorMessage));
            }
        }

        if (erros.Count == 0) return;

        var resposta = new
        {
            Mensagem = "Dados inválidos",
            Erros = erros
        };

        context.Result = new BadRequestObjectResult(resposta);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    { }
    #endregion
}