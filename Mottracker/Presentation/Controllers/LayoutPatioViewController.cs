using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.LayoutPatio;

namespace Mottracker.Presentation.Controllers
{
    public class LayoutPatioViewController : Controller
    {
        private readonly ILayoutPatioApplicationService _applicationService;

        public LayoutPatioViewController(ILayoutPatioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosLayoutsPatios();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar os layouts de pátio";
                return View(new List<LayoutPatioResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterLayoutPatioPorId(id);
            
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
        public IActionResult Create(LayoutPatioRequestDto layoutPatio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosLayoutPatio(layoutPatio);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Layout de pátio criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar o layout de pátio";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(layoutPatio);
        }
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterLayoutPatioPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new LayoutPatioRequestDto
            {
                IdLayoutPatio = result.IdLayoutPatio,
                NomeLayoutPatio = result.NomeLayoutPatio,
                DescricaoLayoutPatio = result.DescricaoLayoutPatio,
                DataCriacaoLayoutPatio = result.DataCriacaoLayoutPatio,
                PatioLayoutPatioId = result.PatioLayoutPatio?.IdPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LayoutPatioRequestDto layoutPatio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosLayoutPatio(id, layoutPatio);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Layout de pátio atualizado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar o layout de pátio";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(layoutPatio);
        }
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterLayoutPatioPorId(id);
            
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
                var result = _applicationService.DeletarDadosLayoutPatio(id);
                
                if (result != null)
                {
                    TempData["Success"] = "Layout de pátio deletado com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar o layout de pátio";
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
