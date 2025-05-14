using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class QrCodePontoApplicationService : IQrCodePontoApplicationService
    {
        private readonly IQrCodePontoRepository _repository;
      
        public QrCodePontoApplicationService(IQrCodePontoRepository repository)
        {
            _repository = repository;
        }
      
        public QrCodePontoEntity? ObterQrCodePontoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<QrCodePontoEntity> ObterTodosQrCodePontos()
        {
            return _repository.ObterTodos();
        }
              
        public QrCodePontoEntity? SalvarDadosQrCodePonto(QrCodePontoEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public QrCodePontoEntity? EditarDadosQrCodePonto(int id, QrCodePontoEntity entity)
        {
            var qrCodePontoExistente = _repository.ObterPorId(id);

            if (qrCodePontoExistente == null)
                return null;
            
            qrCodePontoExistente.IdentificadorQrCode = entity.IdentificadorQrCode;
            qrCodePontoExistente.PosX = entity.PosX;
            qrCodePontoExistente.PosY = entity.PosY;
            qrCodePontoExistente.LayoutPatio = entity.LayoutPatio;

            return _repository.Atualizar(qrCodePontoExistente);
        }
              
        public QrCodePontoEntity? DeletarDadosQrCodePonto(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
