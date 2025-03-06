using Microsoft.EntityFrameworkCore;


namespace AvanlarMetal.Models;

public class Contexts  : DbContext
{
    public Contexts(DbContextOptions<Contexts> options) : base(options)
    {
        
    }
    
    public DbSet<Users> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>()
            .HasKey(x => x.UserId);
    }
}