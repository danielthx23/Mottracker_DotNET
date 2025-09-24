using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Telefone;

namespace Mottracker.Presentation.Controllers
{
    public class TelefoneViewController : Controller
    {
        private readonly ITelefoneApplicationService _applicationService;

        public TelefoneViewController(ITelefoneApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosTelefones();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar os telefones";
                return View(new List<TelefoneResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterTelefonePorId(id);
            
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
        public IActionResult Create(TelefoneRequestDto telefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosTelefone(telefone);
                    
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
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterTelefonePorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new TelefoneRequestDto
            {
                IdTelefone = result.IdTelefone,
                NumeroTelefone = result.NumeroTelefone,
                TipoTelefone = result.TipoTelefone,
                UsuarioTelefoneId = result.UsuarioTelefone?.IdUsuario
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TelefoneRequestDto telefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosTelefone(id, telefone);
                    
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
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterTelefonePorId(id);
            
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
                var result = _applicationService.DeletarDadosTelefone(id);
                
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
