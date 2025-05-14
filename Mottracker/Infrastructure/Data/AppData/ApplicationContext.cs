using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;

namespace Mottracker.Infrastructure.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

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

    }   
}