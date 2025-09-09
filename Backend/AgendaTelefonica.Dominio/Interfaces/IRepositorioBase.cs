using System.Linq.Expressions;

namespace AgendaTelefonica.Dominio.Interfaces;

public interface IRepositorioBase<T> where T : class
{
    Task<T?> BuscarPorId(int id);
    Task<List<T>?> BuscarTodos();
    Task<List<T>?> BuscarPorFiltro(Expression<Func<T, bool>> filtro);
    Task<bool> Cadastrar(T objeto);
    Task<bool> Atualizar(T objeto);
}