using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Camera;

namespace Mottracker.Presentation.Controllers
{
    public class CameraViewController : Controller
    {
        private readonly ICameraUseCase _useCase;

        public CameraViewController(ICameraUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodasCamerasAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar as câmeras";
                return View(new List<CameraResponseDto>());
            }

            return View(result.Value.Data);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterCameraPorIdAsync(id);
            
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
        public async Task<IActionResult> Create(CameraRequestDto camera)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarCameraAsync(camera);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
                    {
                        TempData["Success"] = "Câmera criada com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar a câmera";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(camera);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterCameraPorIdAsync(id);
            
            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new CameraRequestDto
            {
                IdCamera = result.Value.IdCamera,
                NomeCamera = result.Value.NomeCamera,
                IpCamera = result.Value.IpCamera,
                Status = result.Value.Status,
                PosX = result.Value.PosX,
                PosY = result.Value.PosY,
                PatioId = result.Value.Patio?.IdPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CameraRequestDto camera)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarCameraAsync(id, camera);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
                    {
                        TempData["Success"] = "Câmera atualizada com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar a câmera";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(camera);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterCameraPorIdAsync(id);
            
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
                var result = await _useCase.DeletarCameraAsync(id);
                
                if (result != null && result.IsSuccess)
                {
                    TempData["Success"] = "Câmera deletada com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar a câmera";
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
