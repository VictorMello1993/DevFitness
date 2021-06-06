using DevFitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFitness.Infrastructure.Persistence
{
    public class DevFitnessDbContext : DbContext
    {
        public DevFitnessDbContext(DbContextOptions<DevFitnessDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                //PK de usuário
                e.HasKey(u => u.Id);

                //Relacionamento 1 - N => 1 usuário possui 1 ou várias refeições
                e.HasMany(u => u.Meals)
                    .WithOne()
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Restrict);                
            });

            modelBuilder.Entity<Meal>(e =>
            {
                //PK de refeição
                e.HasKey(m => m.Id);
            });
        }
    }
}
