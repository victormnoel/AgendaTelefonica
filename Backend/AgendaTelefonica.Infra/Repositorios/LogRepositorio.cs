using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using AgendaTelefonica.Infra.Contexto;

namespace AgendaTelefonica.Infra.Repositorios;

public class LogRepositorio : ILogRepositorio
{
    #region Propriedades

    private readonly AgendaTelefonicaContexto _contexto;
    #endregion
    
    #region Construtor

    public LogRepositorio(AgendaTelefonicaContexto contexto)
    {
        _contexto = contexto;
    }
    #endregion
    
    #region Servicos
    
    public async Task CadadastrarLog(Log logParaCadastrar)
    {
        _contexto.Add(logParaCadastrar);
         await _contexto.SaveChangesAsync();
    }
    
    #endregion
}