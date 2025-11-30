using Microsoft.EntityFrameworkCore;
using VoeMais.Models;

namespace VoeMais.Data
{
    public class VoeMaisContext : DbContext
    {
        public VoeMaisContext(DbContextOptions<VoeMaisContext> options)
            : base(options)
        {
        }

        public DbSet<EmpresaAerea> EmpresasAereas { get; set; }
        public DbSet<Aviao> Avioes { get; set; }
        public DbSet<Poltrona> Poltronas { get; set; }
        public DbSet<Aeroporto> Aeroportos { get; set; }
        public DbSet<Voo> Voos { get; set; }
        public DbSet<Escala> Escalas { get; set; }
        public DbSet<VooPoltrona> VoosPoltronas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Passagem> Passagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Voo → Aeroportos
            modelBuilder.Entity<Voo>()
                .HasOne(v => v.Origem)
                .WithMany()
                .HasForeignKey(v => v.OrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voo>()
                .HasOne(v => v.Destino)
                .WithMany()
                .HasForeignKey(v => v.DestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Voo → Aviao
            modelBuilder.Entity<Voo>()
                .HasOne(v => v.Aviao)
                .WithMany(a => a.Voos)
                .HasForeignKey(v => v.AviaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // VooPoltrona → Voo
            modelBuilder.Entity<VooPoltrona>()
                .HasOne(vp => vp.Voo)
                .WithMany(v => v.Poltronas)
                .HasForeignKey(vp => vp.VooId)
                .OnDelete(DeleteBehavior.Restrict);

            // VooPoltrona → Poltrona
            modelBuilder.Entity<VooPoltrona>()
                .HasOne(vp => vp.Poltrona)
                .WithMany()
                .HasForeignKey(vp => vp.PoltronaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Escala → Voo
            modelBuilder.Entity<Escala>()
                .HasOne(e => e.Voo)
                .WithMany(v => v.Escalas)
                .HasForeignKey(e => e.VooId)
                .OnDelete(DeleteBehavior.Restrict);

            // Escala → Aeroporto
            modelBuilder.Entity<Escala>()
                .HasOne(e => e.Aeroporto)
                .WithMany()
                .HasForeignKey(e => e.AeroportoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Passagem → Cliente
            modelBuilder.Entity<Passagem>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Passagens)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Passagem → VooPoltrona
            modelBuilder.Entity<Passagem>()
                .HasOne(p => p.VooPoltrona)
                .WithMany()
                .HasForeignKey(p => p.VooPoltronaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Poltrona → Aviao
            modelBuilder.Entity<Poltrona>()
                .HasOne(p => p.Aviao)
                .WithMany(a => a.Poltronas)
                .HasForeignKey(p => p.AviaoId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
