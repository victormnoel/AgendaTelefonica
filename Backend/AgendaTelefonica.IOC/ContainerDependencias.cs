using AgendaTelefonica.Aplicacao.Validacoes;
using AgendaTelefonica.Dominio.Interfaces;
using AgendaTelefonica.Dominio.Servicos;
using AgendaTelefonica.Infra.Contexto;
using AgendaTelefonica.Infra.Repositorios;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaTelefonica.IOC;

public static class ContainerDependencias
{
    public static IServiceCollection AdicionarServicos(this IServiceCollection servicos)
    {
        #region Conexao Com banco de dados
        
        const string STRING_CONEXAO = "server=localhost;Port=3306;database=agendaTelefonica;user=root;password=root";
        
        servicos.AddDbContextPool<AgendaTelefonicaContexto>(opcoes =>
            opcoes.UseMySql(STRING_CONEXAO, ServerVersion.AutoDetect(STRING_CONEXAO)));
        
        #endregion
        
        #region Repositorios
        servicos.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
        servicos.AddScoped<ILogRepositorio, LogRepositorio>();
        servicos.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        #endregion
        
        #region FluentValidation
        
        servicos.AddValidatorsFromAssemblyContaining<CadastrarUsuarioCommandValidator>();
        
        #endregion
        
        #region Servicos Do Dominio
        
        servicos.AddScoped<IUsuarioServico, UsuarioServicos>();
        
        #endregion
        
        return servicos;
    }
}