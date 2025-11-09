using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.QrCodePonto;

namespace Mottracker.Presentation.Controllers
{
    public class QrCodePontoViewController : Controller
    {
        private readonly IQrCodePontoUseCase _useCase;

        public QrCodePontoViewController(IQrCodePontoUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosQrCodePontosAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar os QR Codes de ponto";
                return View(new List<QrCodePontoResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterQrCodePontoPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return View(result.Value);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QrCodePontoRequestDto qrCodePonto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarDadosQrCodePontoAsync(qrCodePonto);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
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
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterQrCodePontoPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new QrCodePontoRequestDto
            {
                IdQrCodePonto = result.Value.IdQrCodePonto,
                IdentificadorQrCode = result.Value.IdentificadorQrCode,
                PosX = result.Value.PosX,
                PosY = result.Value.PosY,
                LayoutPatioId = result.Value.LayoutPatio?.IdLayoutPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QrCodePontoRequestDto qrCodePonto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarDadosQrCodePontoAsync(id, qrCodePonto);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
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
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterQrCodePontoPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return View(result.Value);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _useCase.DeletarDadosQrCodePontoAsync(id);
                
                if (result != null && result.IsSuccess)
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
