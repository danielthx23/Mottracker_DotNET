using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Patio;

namespace Mottracker.Presentation.Controllers
{
    public class PatioViewController : Controller
    {
        private readonly IPatioUseCase _useCase;

        public PatioViewController(IPatioUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosPatiosAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar os pátios";
                return View(new List<PatioResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterPatioPorIdAsync(id);
            
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
        public async Task<IActionResult> Create(PatioRequestDto patio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarDadosPatioAsync(patio);
                    
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
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterPatioPorIdAsync(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new PatioRequestDto
            {
                IdPatio = result.Value.IdPatio,
                NomePatio = result.Value.NomePatio,
                MotosTotaisPatio = result.Value.MotosTotaisPatio,
                MotosDisponiveisPatio = result.Value.MotosDisponiveisPatio,
                DataPatio = result.Value.DataPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatioRequestDto patio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarDadosPatioAsync(id, patio);
                    
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
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterPatioPorIdAsync(id);
            
            if (result == null)
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
                var result = await _useCase.DeletarDadosPatioAsync(id);
                
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
