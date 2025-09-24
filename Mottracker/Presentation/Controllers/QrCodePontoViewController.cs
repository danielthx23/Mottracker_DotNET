using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.QrCodePonto;

namespace Mottracker.Presentation.Controllers
{
    public class QrCodePontoViewController : Controller
    {
        private readonly IQrCodePontoApplicationService _applicationService;

        public QrCodePontoViewController(IQrCodePontoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosQrCodePontos();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar os QR Codes de ponto";
                return View(new List<QrCodePontoResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterQrCodePontoPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QrCodePontoRequestDto qrCodePonto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosQrCodePonto(qrCodePonto);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "QR Code de ponto criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar o QR Code de ponto";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(qrCodePonto);
        }
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterQrCodePontoPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new QrCodePontoRequestDto
            {
                IdQrCodePonto = result.IdQrCodePonto,
                IdentificadorQrCodePonto = result.IdentificadorQrCodePonto,
                PosXQrCodePonto = result.PosXQrCodePonto,
                PosYQrCodePonto = result.PosYQrCodePonto,
                LayoutPatioQrCodePontoId = result.LayoutPatioQrCodePonto?.IdLayoutPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, QrCodePontoRequestDto qrCodePonto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosQrCodePonto(id, qrCodePonto);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "QR Code de ponto atualizado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar o QR Code de ponto";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(qrCodePonto);
        }
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterQrCodePontoPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var result = _applicationService.DeletarDadosQrCodePonto(id);
                
                if (result != null)
                {
                    TempData["Success"] = "QR Code de ponto deletado com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar o QR Code de ponto";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
