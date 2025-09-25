using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Permissao;

namespace Mottracker.Presentation.Controllers
{
    public class PermissaoViewController : Controller
    {
        private readonly IPermissaoUseCase _useCase;

        public PermissaoViewController(IPermissaoUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosPermissoesAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar as permissões";
                return View(new List<PermissaoResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterPermissaoPorIdAsync(id);
            
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
        public async Task<IActionResult> Create(PermissaoRequestDto permissao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarDadosPermissaoAsync(permissao);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
                    {
                        TempData["Success"] = "Permissão criada com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar a permissão";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(permissao);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterPermissaoPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new PermissaoRequestDto
            {
                IdPermissao = result.Value.IdPermissao,
                NomePermissao = result.Value.NomePermissao,
                Descricao = result.Value.Descricao
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PermissaoRequestDto permissao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarDadosPermissaoAsync(id, permissao);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
                    {
                        TempData["Success"] = "Permissão atualizada com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar a permissão";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(permissao);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterPermissaoPorIdAsync(id);
            
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
                var result = await _useCase.DeletarDadosPermissaoAsync(id);
                
                if (result != null && result.IsSuccess)
                {
                    TempData["Success"] = "Permissão deletada com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar a permissão";
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
