using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Telefone;

namespace Mottracker.Presentation.Controllers
{
    public class TelefoneViewController : Controller
    {
        private readonly ITelefoneUseCase _useCase;

        public TelefoneViewController(ITelefoneUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosTelefonesAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar os telefones";
                return View(new List<TelefoneResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterTelefonePorIdAsync(id);
            
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
        public async Task<IActionResult> Create(TelefoneRequestDto telefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarDadosTelefoneAsync(telefone);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Telefone criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar o telefone";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(telefone);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterTelefonePorIdAsync(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new TelefoneRequestDto
            {
                Numero = result.Value.Numero,
                Tipo = result.Value.Tipo,
                UsuarioId = result.Value.Usuario?.IdUsuario
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TelefoneRequestDto telefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarDadosTelefoneAsync(id, telefone);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Telefone atualizado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar o telefone";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(telefone);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterTelefonePorIdAsync(id);
            
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
                var result = await _useCase.DeletarDadosTelefoneAsync(id);
                
                if (result != null)
                {
                    TempData["Success"] = "Telefone deletado com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar o telefone";
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
