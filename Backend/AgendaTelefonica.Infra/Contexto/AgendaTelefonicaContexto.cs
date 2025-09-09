using System.Reflection;
using AgendaTelefonica.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AgendaTelefonica.Infra.Contexto;

public class AgendaTelefonicaContexto : DbContext
{
    public AgendaTelefonicaContexto(DbContextOptions<AgendaTelefonicaContexto> opcoes) : base(opcoes)
    {}
    
    #region Tabelas

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Log> Log { get; set; }
    
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    
    public override Task<int> SaveChangesAsync(CancellationToken tokenDeCancelamento = new())
    {
        foreach (EntityEntry entrada in ChangeTracker.Entries().Where(entrada => entrada.Entity.GetType().GetProperty("DataDeCriacao") != null))
        {
            if (entrada.State == EntityState.Added)
            {
                if (entrada.Entity.GetType().GetProperty("DataDeCriacao") != null)
                {
                    if (entrada.Property("DataDeCriacao").CurrentValue == null)
                        entrada.Property("DataDeCriacao").CurrentValue = DateTime.Now;
                }
    
    
                if (entrada.Entity.GetType().GetProperty("DataDeAtualizacao") != null)
                    entrada.Property("DataDeAtualizacao").IsModified = false;
            }
    
            if (entrada.State == EntityState.Modified)
            {
                if (entrada.Entity.GetType().GetProperty("DataDeCriacao") != null)
                    entrada.Property("DataDeCriacao").IsModified = false;
    
                if (entrada.Entity.GetType().GetProperty("DataDeAtualizacao") != null)
                    entrada.Property("DataDeAtualizacao").CurrentValue = DateTime.Now;
            }
        }
    
        return base.SaveChangesAsync(tokenDeCancelamento);
    }
}