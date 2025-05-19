using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class CameraRepository : ICameraRepository
    {
        private readonly ApplicationContext _context;

        public CameraRepository(ApplicationContext context)
        {
            _context = context;
        }
        
        public IEnumerable<CameraEntity> ObterTodos()
        {
            var cameras = _context.Camera
                    .Include(c => c.Patio) 
                    .ToList();

            return cameras;
        }

        public CameraEntity? ObterPorId(int id)
        {
            var camera = _context.Camera
                .Include(c => c.Patio)
                .FirstOrDefault(c => c.IdCamera == id);

            return camera;
        }
        
        public List<CameraEntity> ObterPorIds(List<int> ids)
        {
            return _context.Camera
                .Where(c => ids.Contains(c.IdCamera))
                .ToList();
        }

        public CameraEntity? Salvar(CameraEntity entity)
        {
            _context.Camera.Add(entity);
            _context.SaveChanges();

            return entity;
        }
        
        public CameraEntity? Atualizar(CameraEntity entity)
        {
            _context.Camera.Update(entity);
            _context.SaveChanges();

            return entity;
        }
        
        public CameraEntity? Deletar(int id)
        {
            var entity = _context.Camera.Find(id);

            if (entity is not null)
            {
                _context.Camera.Remove(entity);
                _context.SaveChanges();

                return entity;
            }

            return null;
        }

        public IEnumerable<CameraEntity> ObterPorNome(string nomeCamera)
        {
            return _context.Camera
                .Include(c => c.Patio)
                .Where(c => EF.Functions.Like(c.NomeCamera, $"%{nomeCamera}%"))
                .ToList();
        }

        public IEnumerable<CameraEntity> ObterPorStatus(CameraStatus status)
        {
            return _context.Camera
                .Include(c => c.Patio)
                .Where(c => c.Status == status)
                .ToList();
        }
    }
}
