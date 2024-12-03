using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoLuzaBlog.Models;
using ProjetoLuzaBlog.Pages.Autenticacao;

namespace ProjetoLuzaBlog.Dal
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Postagem> Postagens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relacionamento entre Postagem e IdentityUser
            builder.Entity<Postagem>()
                .HasOne<IdentityUser>() // A Postagem tem um usuário
                .WithMany() // O usuário tem muitas postagens
                .HasForeignKey(p => p.UserId); // Chave estrangeira na tabela Postagens
        }
    }
}
