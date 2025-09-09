using AgendaTelefonica.Dominio.Entidades;

namespace AgendaTelefonica.Dominio.Interfaces;

public interface ILogRepositorio
{
    Task CadadastrarLog(Log logParaCadastrar);
}