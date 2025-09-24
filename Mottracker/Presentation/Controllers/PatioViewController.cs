using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Patio;

namespace Mottracker.Presentation.Controllers
{
    public class PatioViewController : Controller
    {
        private readonly IPatioApplicationService _applicationService;

        public PatioViewController(IPatioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosPatios();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar os pátios";
                return View(new List<PatioResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterPatioPorId(id);
            
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
        public IActionResult Create(PatioRequestDto patio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosPatio(patio);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Pátio criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar o pátio";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(patio);
        }
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterPatioPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new PatioRequestDto
            {
                IdPatio = result.IdPatio,
                NomePatio = result.NomePatio,
                MotosTotaisPatio = result.MotosTotaisPatio,
                MotosDisponiveisPatio = result.MotosDisponiveisPatio,
                DataPatio = result.DataPatio,
                EnderecoPatioId = result.EnderecoPatio?.IdEndereco
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PatioRequestDto patio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosPatio(id, patio);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Pátio atualizado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar o pátio";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(patio);
        }
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterPatioPorId(id);
            
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
                var result = _applicationService.DeletarDadosPatio(id);
                
                if (result != null)
                {
                    TempData["Success"] = "Pátio deletado com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar o pátio";
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
