using ApiCore.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options) : base(options){ }

   
    // Construtor sem parâmetro
    public DbSet<User> Users { get; set; }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var bancoDeDados = "Server=WP062692\\SQLEXPRESS,1433;Database=APICOREMAIN;Trusted_Connection=True;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(bancoDeDados);

        return new AppDbContext(optionsBuilder.Options);
    }
}