using Microsoft.EntityFrameworkCore;
using Store.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Catalogo.Domain.Data
{
    class CatalogoContext : DbContext, IUnityOfWork
    {
        // Construtor usado pelo startup project
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setando todas as properties strings oara Varchar(100)
            var stringProperties = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
            foreach (var prop in stringProperties)
                prop.SetColumnType("varchar(100)");

            // Busca os mappings criados por reflection
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            // ChangeTracker é o mapeador de alterações do entity
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                // Se estiver adicionando seta a data, caso contrário não deixa setar
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
