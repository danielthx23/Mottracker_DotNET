using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class QrCodePontoRepository : IQrCodePontoRepository
    {
        private readonly ApplicationContext _context;

        public QrCodePontoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<QrCodePontoEntity> ObterTodos()
        {
            return _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .ToList();
        }

        public QrCodePontoEntity? ObterPorId(int id)
        {
            return _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .FirstOrDefault(q => q.IdQrCodePonto == id);
        }

        public QrCodePontoEntity? Salvar(QrCodePontoEntity entity)
        {
            _context.QrCodePonto.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public QrCodePontoEntity? Atualizar(QrCodePontoEntity entity)
        {
            _context.QrCodePonto.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public QrCodePontoEntity? Deletar(int id)
        {
            var entity = _context.QrCodePonto.Find(id);

            if (entity is not null)
            {
                _context.QrCodePonto.Remove(entity);
                _context.SaveChanges();

                return entity;
            }

            return null;
        }

        public QrCodePontoEntity? ObterPorIdentificador(string identificadorQrCode)
        {
            return _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .FirstOrDefault(q => q.IdentificadorQrCode == identificadorQrCode);
        }

        public IEnumerable<QrCodePontoEntity> ObterPorIdLayoutPatio(long layoutPatioId)
        {
            return _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .Where(q => q.LayoutPatioId == layoutPatioId)
                .ToList();
        }

        public IEnumerable<QrCodePontoEntity> ObterPorPosicaoXEntre(float posXInicial, float posXFinal)
        {
            return _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .Where(q => q.PosX >= posXInicial && q.PosX <= posXFinal)
                .ToList();
        }

        public IEnumerable<QrCodePontoEntity> ObterPorPosicaoYEntre(float posYInicial, float posYFinal)
        {
            return _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .Where(q => q.PosY >= posYInicial && q.PosY <= posYFinal)
                .ToList();
        }

    }
}