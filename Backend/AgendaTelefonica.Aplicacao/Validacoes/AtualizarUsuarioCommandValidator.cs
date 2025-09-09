using System.Text.RegularExpressions;
using AgendaTelefonica.Aplicacao.Commands.Atualizar;
using AgendaTelefonica.Dominio.Entidades;
using FluentValidation;

namespace AgendaTelefonica.Aplicacao.Validacoes;

public class AtualizarUsuarioCommandValidator : AbstractValidator<AtualizarUsuarioCommand>
{
    public AtualizarUsuarioCommandValidator()
    {
        RuleFor(modelo => modelo.Nome)
            .NotEmpty()
            .WithMessage("O nome deve ser informado!")
            .Must(VerificarQuantidadeDeCaracteres)
            .WithMessage("O nome deve conter no mínimo 5 letras!");
        
        RuleFor(modelo => modelo.Email)
            .NotEmpty()
            .WithMessage("O email deve ser informado!")
            .EmailAddress()
            .WithMessage("O email informado não é válido!");

        RuleFor(modelo => modelo.Telefone)
            .NotEmpty()
            .WithMessage("O número de telefone deve ser informado!")
            .Must(VerificarQuantidadeDeDigitos)
            .WithMessage("O número de telefone deve ter entre 8 e 13 dígitos!");

        
        bool VerificarQuantidadeDeDigitos(string telefone)
        {
            string telefoneFormatado = Regex.Replace(telefone, @"[^\d]", "");
            return telefoneFormatado.Length >= 8 && telefoneFormatado.Length <= 13;
        }

        bool VerificarQuantidadeDeCaracteres(string nome)
        {
            string nomeFormatado = Regex.Replace(nome, @"[^a-zA-Z\s]", "").Trim();
            return nomeFormatado.Length > 5;
        }
    }
}