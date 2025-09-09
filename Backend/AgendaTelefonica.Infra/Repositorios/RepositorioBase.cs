using System.Linq.Expressions;
using AgendaTelefonica.Dominio.Interfaces;
using AgendaTelefonica.Infra.Contexto;
using Microsoft.EntityFrameworkCore;

namespace AgendaTelefonica.Infra.Repositorios;

public class RepositorioBase<T> : IRepositorioBase<T> where T : class
{
    #region Propriedades

    private readonly AgendaTelefonicaContexto _contexto;
    
    #endregion
    
    #region Construtor
    
    public RepositorioBase(AgendaTelefonicaContexto contexto)
    {
        _contexto = contexto;
    }
    #endregion
    
    #region Servicos

    public async Task<T?> BuscarPorId(int id) => await _contexto.Set<T>().FindAsync(id);

    public async Task<List<T>?> BuscarTodos() => await _contexto.Set<T>().ToListAsync();

    public async Task<List<T>?> BuscarPorFiltro(Expression<Func<T, bool>> filtro) => await _contexto.Set<T>().Where(filtro).ToListAsync();

    public async Task<bool> Cadastrar(T objetoParaCadastrar)
    { 
        _contexto.Set<T>().Add(objetoParaCadastrar);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Atualizar(T objetoParaAtualizar)
    {
        _contexto.Set<T>().Update(objetoParaAtualizar);
        return await _contexto.SaveChangesAsync() > 0;
    }
    
    #endregion
}