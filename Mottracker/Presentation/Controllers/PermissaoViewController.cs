using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Permissao;

namespace Mottracker.Presentation.Controllers
{
    public class PermissaoViewController : Controller
    {
        private readonly IPermissaoApplicationService _applicationService;

        public PermissaoViewController(IPermissaoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosPermissoes();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar as permissões";
                return View(new List<PermissaoResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterPermissaoPorId(id);
            
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
        public IActionResult Create(PermissaoRequestDto permissao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosPermissao(permissao);
                    
                    if (result != null)
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
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterPermissaoPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new PermissaoRequestDto
            {
                IdPermissao = result.IdPermissao,
                NomePermissao = result.NomePermissao,
                DescricaoPermissao = result.DescricaoPermissao
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PermissaoRequestDto permissao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosPermissao(id, permissao);
                    
                    if (result != null)
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
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterPermissaoPorId(id);
            
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
                var result = _applicationService.DeletarDadosPermissao(id);
                
                if (result != null)
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
