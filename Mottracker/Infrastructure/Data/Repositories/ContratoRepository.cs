using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class ContratoRepository : IContratoRepository
    {
        private readonly ApplicationContext _context;

        public ContratoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<ContratoEntity> ObterTodos()
        {
            var contratos = _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .ToList();

            return contratos;
        }

        public ContratoEntity? ObterPorId(int id)
        {
            var contrato = _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .FirstOrDefault(c => c.IdContrato == id);

            return contrato;
        }

        public ContratoEntity? Salvar(ContratoEntity entity)
        {
            _context.Contrato.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public ContratoEntity? Atualizar(ContratoEntity entity)
        {
            _context.Contrato.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public ContratoEntity? Deletar(int id)
        {
            var entity = _context.Contrato.Find(id);

            if (entity is not null)
            {
                _context.Contrato.Remove(entity);
                _context.SaveChanges();

                return entity;
            }

            return null;
        }

        public IEnumerable<ContratoEntity> ObterPorAtivoContrato(int ativoContrato)
        {
            return _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.AtivoContrato == ativoContrato)
                .ToList();
        }

        public IEnumerable<ContratoEntity> ObterPorUsuarioId(long usuarioId)
        {
            return _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.UsuarioContrato.IdUsuario == usuarioId)
                .ToList();
        }

        public IEnumerable<ContratoEntity> ObterPorMotoId(long motoId)
        {
            return _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.MotoContrato.IdMoto == motoId)
                .ToList();
        }

        public IEnumerable<ContratoEntity> ObterContratosNaoExpirados(DateTime dataAtual)
        {
            return _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.DataDeExpiracaoContrato > dataAtual)
                .ToList();
        }

        public IEnumerable<ContratoEntity> ObterPorRenovacaoAutomatica(int renovacaoAutomatica)
        {
            return _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.RenovacaoAutomaticaContrato == renovacaoAutomatica)
                .ToList();
        }

        public IEnumerable<ContratoEntity> ObterPorDataEntradaEntre(DateTime dataInicio, DateTime dataFim)
        {
            return _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.DataDeEntradaContrato >= dataInicio && c.DataDeEntradaContrato <= dataFim)
                .ToList();
        }
    }
}
