using Microsoft.EntityFrameworkCore;
using Systems = SecuritySystem.Domain.Entities.System;

namespace SecuritySystem.Repositories.Context
{
    public class SSContext : DbContext
    {
        // Injeta as informações do banco para a DbContext
        public SSContext(DbContextOptions<SSContext> options) : base (options) 
        {

        }

        public DbSet<Systems> Systems { get; set; }

        // Realiza as especificações da tabela System no banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Systems>().ToTable("system").HasKey(c => c.Id);
            modelBuilder.Entity<Systems>().Property(c => c.Id).HasColumnName("id");
            modelBuilder.Entity<Systems>().Property(c => c.Description).HasColumnName("description").HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Systems>().Property(c => c.Initials).HasColumnName("initials").HasMaxLength(10).IsRequired();
            modelBuilder.Entity<Systems>().Property(c => c.Email).HasColumnName("email").HasMaxLength(100);
            modelBuilder.Entity<Systems>().Property(c => c.Url).HasColumnName("url").HasMaxLength(50);
            modelBuilder.Entity<Systems>().Property(c => c.Status).HasColumnName("status").IsRequired();
            modelBuilder.Entity<Systems>().Property(c => c.UserResponsibleForLastUpdate).HasColumnName("user_responsible_for_last_update").HasMaxLength(100);
            modelBuilder.Entity<Systems>().Property(c => c.UpdateAt).HasColumnName("update_at").HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<Systems>().Property(c => c.JustificationForTheLastUpdate).HasColumnName("justification_for_the_last_update").HasMaxLength(500);
            modelBuilder.Entity<Systems>().Property(c => c.NewJustification).HasColumnName("new_justification").HasMaxLength(500);

            base.OnModelCreating(modelBuilder);
        }
    }
}
