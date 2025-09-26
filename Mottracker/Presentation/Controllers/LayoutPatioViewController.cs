using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.LayoutPatio;

namespace Mottracker.Presentation.Controllers
{
    public class LayoutPatioViewController : Controller
    {
        private readonly ILayoutPatioUseCase _useCase;

        public LayoutPatioViewController(ILayoutPatioUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosLayoutsPatiosAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar os layouts de pátio";
                return View(new List<LayoutPatioResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterLayoutPatioPorIdAsync(id);
            
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
        public async Task<IActionResult> Create(LayoutPatioRequestDto layoutPatio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarDadosLayoutPatioAsync(layoutPatio);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
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
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterLayoutPatioPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new LayoutPatioRequestDto
            {
                IdLayoutPatio = result.Value.IdLayoutPatio,
                Descricao = result.Value.Descricao,
                DataCriacao = result.Value.DataCriacao,
                Largura = result.Value.Largura,
                Comprimento = result.Value.Comprimento,
                Altura = result.Value.Altura,
                PatioLayoutPatioId = result.Value.PatioLayoutPatio?.IdPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LayoutPatioRequestDto layoutPatio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarDadosLayoutPatioAsync(id, layoutPatio);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
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
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterLayoutPatioPorIdAsync(id);
            
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
                var result = await _useCase.DeletarDadosLayoutPatioAsync(id);
                
                if (result != null && result.IsSuccess)
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
