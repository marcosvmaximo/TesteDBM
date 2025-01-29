using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Teste.Domain;

namespace Teste.Infra;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    //
    // modelBuilder.Entity<Produto>(entity =>
    // {
    //     entity.ToTable("produtos");
    //     
    //     entity.HasKey(p => p.Id)
    //         .HasName("id");
    //
    //     entity.Property(p => p.Nome)
    //         .IsRequired()
    //         .HasColumnName("nome")
    //         .HasMaxLength(100);
    //
    //     entity.Property(p => p.Preco)
    //         .IsRequired()
    //         .HasColumnName("preco")
    //         .HasColumnType("decimal(18,2)");
    //
    //     entity.Property(p => p.Descricao)
    //         .IsRequired()
    //         .HasColumnName("descricao")
    //         .HasColumnType("text");
    //
    //     entity.Property(p => p.DataCadastro)
    //         .HasColumnName("data_cadastro")
    //         .HasColumnType("date");
    // });
    } 
}

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;");

        return new DataContext(optionsBuilder.Options);
    }
}