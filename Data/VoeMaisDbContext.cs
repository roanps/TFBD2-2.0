using Microsoft.EntityFrameworkCore;
using VoeMais.Models;

namespace VoeMais.Data
{
    public class VoeMaisDbContext : DbContext
    {
        public VoeMaisDbContext(DbContextOptions<VoeMaisDbContext> options)
            : base(options)
        {
        }
        // Tabelas (DbSet)
        public DbSet<EmpresaAerea> EmpresasAereas { get; set; }
        public DbSet<Aeroporto> Aeroportos { get; set; }
        public DbSet<Aeronave> Aeronaves { get; set; }
        public DbSet<Voo> Voos { get; set; }
        public DbSet<Escala> Escalas { get; set; }
        public DbSet<VooEscala> VooEscalas { get; set; }
        public DbSet<Passageiro> Passageiros { get; set; }
        public DbSet<Poltrona> Poltronas { get; set; }
        public DbSet<Passagem> Passagens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // EMPRESA AÉREA
            modelBuilder.Entity<EmpresaAerea>()
                .ToTable("EMPRESA_AEREA")
                .HasKey(e => e.IdEmpresa);

            modelBuilder.Entity<EmpresaAerea>()
                .Property(e => e.IdEmpresa)
                .HasColumnName("ID_EMPRESA");

            modelBuilder.Entity<EmpresaAerea>()
                .Property(e => e.Nome)
                .HasColumnName("NOME");

            modelBuilder.Entity<EmpresaAerea>()
                .Property(e => e.Telefone)
                .HasColumnName("TELEFONE");

            modelBuilder.Entity<EmpresaAerea>()
                .Property(e => e.Cnpj)
                .HasColumnName("CNPJ");

            // AEROPORTO
            modelBuilder.Entity<Aeroporto>()
                .ToTable("AEROPORTO")
                .HasKey(a => a.IdAeroporto);

            modelBuilder.Entity<Aeroporto>()
                .Property(a => a.IdAeroporto)
                .HasColumnName("ID_AEROPORTO");

            modelBuilder.Entity<Aeroporto>()
                .Property(a => a.Nome)
                .HasColumnName("NOME");

            modelBuilder.Entity<Aeroporto>()
                .Property(a => a.Cidade)
                .HasColumnName("CIDADE");

            modelBuilder.Entity<Aeroporto>()
                .Property(a => a.Estado)
                .HasColumnName("ESTADO");

            modelBuilder.Entity<Aeroporto>()
                .Property(a => a.CodigoIATA)
                .HasColumnName("CODIGO_IATA");

            // AERONAVE
            modelBuilder.Entity<Aeronave>()
                .ToTable("AERONAVE")
                .HasKey(a => a.IdAeronave);

            modelBuilder.Entity<Aeronave>()
                .Property(a => a.IdAeronave)
                .HasColumnName("ID_AERONAVE");

            modelBuilder.Entity<Aeronave>()
                .Property(a => a.IdEmpresa)
                .HasColumnName("ID_EMPRESA");

            modelBuilder.Entity<Aeronave>()
                .Property(a => a.ModeloAeronave)
                .HasColumnName("MODELO_AERONAVE");

            modelBuilder.Entity<Aeronave>()
                .Property(a => a.DataFabricacao)
                .HasColumnName("DATA_FABRICACAO");

            modelBuilder.Entity<Aeronave>()
                .Property(a => a.NumeroPoltronas)
                .HasColumnName("NUMERO_POLTRONAS");

            modelBuilder.Entity<Aeronave>()
                .Property(a => a.NumeroMaximoTripulantes)
                .HasColumnName("NUMERO_MAXIMO_TRIPULANTES");

            modelBuilder.Entity<Aeronave>()
                .Property(a => a.CapacidadeMaximaCombustivel)
                .HasColumnName("CAPACIDADE_MAXIMA_COMBUSTIVEL");

            modelBuilder.Entity<Aeronave>()
                .Property(a => a.CapacidadeMaximaVoo)
                .HasColumnName("CAPACIDADE_MAXIMA_VOO");

            modelBuilder.Entity<Aeronave>()
                .HasOne(a => a.EmpresaAerea)
                .WithMany(e => e.Aeronaves)
                .HasForeignKey(a => a.IdEmpresa)
                .OnDelete(DeleteBehavior.Restrict);

            // VOO
            modelBuilder.Entity<Voo>()
                .ToTable("VOO")
                .HasKey(v => v.IdVoo);

            modelBuilder.Entity<Voo>()
                .Property(v => v.IdVoo)
                .HasColumnName("ID_VOO");

            modelBuilder.Entity<Voo>()
                .Property(v => v.IdAeronave)
                .HasColumnName("ID_AERONAVE");

            modelBuilder.Entity<Voo>()
                .Property(v => v.IdAeroportoOrigem)
                .HasColumnName("ID_AEROPORTO_ORIGEM");

            modelBuilder.Entity<Voo>()
                .Property(v => v.IdAeroportoDestino)
                .HasColumnName("ID_AEROPORTO_DESTINO");

            modelBuilder.Entity<Voo>()
                .Property(v => v.Partida)
                .HasColumnName("PARTIDA");

            modelBuilder.Entity<Voo>()
                .Property(v => v.Chegada)
                .HasColumnName("CHEGADA");

            modelBuilder.Entity<Voo>()
                .HasOne(v => v.Aeronave)
                .WithMany(a => a.Voos)
                .HasForeignKey(v => v.IdAeronave)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voo>()
                .HasOne(v => v.Origem)
                .WithMany()
                .HasForeignKey(v => v.IdAeroportoOrigem)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voo>()
                .HasOne(v => v.Destino)
                .WithMany()
                .HasForeignKey(v => v.IdAeroportoDestino)
                .OnDelete(DeleteBehavior.Restrict);

            // ESCALA
            modelBuilder.Entity<Escala>()
                .ToTable("ESCALA")
                .HasKey(e => e.IdEscala);

            modelBuilder.Entity<Escala>()
                .Property(e => e.IdEscala)
                .HasColumnName("ID_ESCALA");

            modelBuilder.Entity<Escala>()
                .Property(e => e.IdAeroportoEscala)
                .HasColumnName("ID_AEROPORTO_ESCALA");

            modelBuilder.Entity<Escala>()
                .Property(e => e.ChegadaEscala)
                .HasColumnName("CHEGADA_ESCALA");

            modelBuilder.Entity<Escala>()
                .Property(e => e.SaidaEscala)
                .HasColumnName("SAIDA_ESCALA");

            modelBuilder.Entity<Escala>()
                .HasOne(e => e.AeroportoEscala)
                .WithMany()
                .HasForeignKey(e => e.IdAeroportoEscala)
                .OnDelete(DeleteBehavior.Restrict);

            // RELAÇÃO VOO_ESCALA
            modelBuilder.Entity<VooEscala>()
                .ToTable("VOO_ESCALA")
                .HasKey(ve => new { ve.IdVoo, ve.IdEscala });

            modelBuilder.Entity<VooEscala>()
                .Property(ve => ve.IdVoo)
                .HasColumnName("ID_VOO");

            modelBuilder.Entity<VooEscala>()
                .Property(ve => ve.IdEscala)
                .HasColumnName("ID_ESCALA");

            modelBuilder.Entity<VooEscala>()
                .HasOne(ve => ve.Voo)
                .WithMany(v => v.VoosEscalas)
                .HasForeignKey(ve => ve.IdVoo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VooEscala>()
                .HasOne(ve => ve.Escala)
                .WithMany(e => e.VoosEscalas)
                .HasForeignKey(ve => ve.IdEscala)
                .OnDelete(DeleteBehavior.Restrict);

            // PASSAGEIRO
            modelBuilder.Entity<Passageiro>()
                .ToTable("PASSAGEIRO")
                .HasKey(p => p.IdPassageiro);

            // POLTRONA
            modelBuilder.Entity<Poltrona>()
                .ToTable("POLTRONA")
                .HasKey(p => new { p.IdVoo, p.NumeroPoltrona });

            // PASSAGEM
            modelBuilder.Entity<Passagem>()
                .ToTable("PASSAGEM")
                .HasKey(p => new { p.IdPassageiro, p.IdVoo });
        }
    }
}
