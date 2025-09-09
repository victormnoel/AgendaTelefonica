using AgendaTelefonica.Dominio.Entidades;
using AgendaTelefonica.Dominio.Interfaces;
using AgendaTelefonica.Infra.Contexto;

namespace AgendaTelefonica.Infra.Repositorios;

public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
{
    #region Construtor
    public UsuarioRepositorio(AgendaTelefonicaContexto contexto) : base(contexto)
    { }
    
    #endregion
}