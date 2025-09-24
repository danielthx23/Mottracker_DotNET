using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Camera;

namespace Mottracker.Presentation.Controllers
{
    public class CameraViewController : Controller
    {
        private readonly ICameraApplicationService _applicationService;

        public CameraViewController(ICameraApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodasCameras();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar as câmeras";
                return View(new List<CameraResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterCameraPorId(id);
            
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
        public IActionResult Create(CameraRequestDto camera)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosCamera(camera);
                    
                    if (result != null)
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
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterCameraPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new CameraRequestDto
            {
                IdCamera = result.IdCamera,
                NomeCamera = result.NomeCamera,
                IpCamera = result.IpCamera,
                Status = result.Status,
                PosX = result.PosX,
                PosY = result.PosY,
                PatioId = result.Patio?.IdPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CameraRequestDto camera)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosCamera(id, camera);
                    
                    if (result != null)
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
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterCameraPorId(id);
            
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
                var result = _applicationService.DeletarDadosCamera(id);
                
                if (result != null)
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
