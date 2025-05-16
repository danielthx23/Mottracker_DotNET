using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;

namespace Mottracker.Infrastructure.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<CameraEntity> Camera { get; set; }
        public DbSet<ContratoEntity> Contrato { get; set; }
        public DbSet<EnderecoEntity> Endereco { get; set; }
        public DbSet<LayoutPatioEntity> LayoutPatio { get; set; }
        public DbSet<MotoEntity> Moto { get; set; }
        public DbSet<PatioEntity> Patio { get; set; }
        public DbSet<PermissaoEntity> Permissao { get; set; }
        public DbSet<QrCodePontoEntity> QrCodePonto { get; set; }
        public DbSet<TelefoneEntity> Telefone { get; set; }
        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<UsuarioPermissaoEntity> UsuarioPermissao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CAMERA - muitos para um com PATIO
            modelBuilder.Entity<CameraEntity>()
                .HasOne(c => c.Patio)
                .WithMany(p => p.CamerasPatio)
                .HasForeignKey(c => c.PatioId)
                .OnDelete(DeleteBehavior.Restrict);

            // CONTRATO - um para um com USUARIO
            modelBuilder.Entity<ContratoEntity>()
                .HasOne(c => c.UsuarioContrato)
                .WithOne(u => u.ContratoUsuario)
                .HasForeignKey<ContratoEntity>(c => c.UsuarioContratoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ENDERECO - um para um com PATIO
            modelBuilder.Entity<EnderecoEntity>()
                .HasOne(e => e.PatioEndereco)
                .WithOne(p => p.EnderecoPatio)
                .HasForeignKey<EnderecoEntity>(e => e.PatioEnderecoId)
                .OnDelete(DeleteBehavior.Restrict);

            // LAYOUT_PATIO - um para um com PATIO
            modelBuilder.Entity<LayoutPatioEntity>()
                .HasOne(lp => lp.PatioLayoutPatio)
                .WithOne(p => p.LayoutPatio)
                .HasForeignKey<LayoutPatioEntity>(lp => lp.PatioLayoutPatioId)
                .OnDelete(DeleteBehavior.Restrict);

            // MOTO - muitos para um com PATIO, um para um com CONTRATO
            modelBuilder.Entity<MotoEntity>()
                .HasOne(m => m.ContratoMoto)
                .WithOne(c => c.MotoContrato)   // supondo que ContratoEntity tenha essa propriedade
                .HasForeignKey<MotoEntity>(m => m.ContratoMotoId);

            modelBuilder.Entity<MotoEntity>()
                .HasOne(m => m.MotoPatioAtual)
                .WithMany(p => p.MotosPatioAtual) // supondo que PatioEntity tenha essa coleção
                .HasForeignKey(m => m.MotoPatioAtualId)
                .OnDelete(DeleteBehavior.Restrict);

            // patio

            // PERMISSAO - tabela independente

            // QR_CODE_PONTO - muitos para um com LAYOUT_PATIO
            modelBuilder.Entity<QrCodePontoEntity>()
                .HasOne(qr => qr.LayoutPatio)
                .WithMany(lp => lp.QrCodesLayoutPatio)
                .HasForeignKey(qr => qr.LayoutPatioId)
                .OnDelete(DeleteBehavior.Cascade);

            // TELEFONE - muitos para um com USUARIO
            modelBuilder.Entity<TelefoneEntity>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Telefones)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // USUARIO - tabela independente com sequência e trigger no banco

            // USUARIO_PERMISSAO - relacionamento muitos-para-muitos com entidade de junção
            modelBuilder.Entity<UsuarioPermissaoEntity>()
                .HasKey(up => new { up.UsuarioId, up.PermissaoId });

            modelBuilder.Entity<UsuarioPermissaoEntity>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuarioPermissoes)
                .HasForeignKey(up => up.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioPermissaoEntity>()
                .HasOne(up => up.Permissao)
                .WithMany(p => p.UsuarioPermissoes)
                .HasForeignKey(up => up.PermissaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
