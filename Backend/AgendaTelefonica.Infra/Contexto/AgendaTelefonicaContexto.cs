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
        foreach (EntityEntry entrada in ChangeTracker.Entries().Where(entrada => entrada.Entity.GetType().GetProperty("DataDaCriacao") != null))
        {
            if (entrada.State == EntityState.Added)
            {
                if (entrada.Entity.GetType().GetProperty("DataDaCriacao") != null)
                {
                    var currentValue = entrada.Property("DataDaCriacao").CurrentValue;
                    if (currentValue == null || currentValue.Equals(default(DateTime)))
                        entrada.Property("DataDaCriacao").CurrentValue = DateTime.Now;
                }
    
    
                if (entrada.Entity.GetType().GetProperty("DataDaAtualizacao") != null)
                    entrada.Property("DataDaAtualizacao").IsModified = false;
            }

            if (entrada.State != EntityState.Modified) continue;
            
            if (entrada.Entity.GetType().GetProperty("DataDaCriacao") != null)
                entrada.Property("DataDaCriacao").IsModified = false;
    
            if (entrada.Entity.GetType().GetProperty("DataDaAtualizacao") != null)
                entrada.Property("DataDaAtualizacao").CurrentValue = DateTime.Now;
        }
    
        return base.SaveChangesAsync(tokenDeCancelamento);
    }
}