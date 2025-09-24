using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Presentation.Controllers
{
    public class UsuarioViewController : Controller
    {
        private readonly IUsuarioApplicationService _applicationService;

        public UsuarioViewController(IUsuarioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosUsuarios();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar os usuários";
                return View(new List<UsuarioResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterUsuarioPorId(id);
            
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
        public IActionResult Create(UsuarioRequestDto usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosUsuario(usuario);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Usuário criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar o usuário";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(usuario);
        }
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterUsuarioPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new UsuarioRequestDto
            {
                IdUsuario = result.IdUsuario,
                NomeUsuario = result.NomeUsuario,
                CPFUsuario = result.CPFUsuario,
                SenhaUsuario = result.SenhaUsuario,
                CNHUsuario = result.CNHUsuario,
                EmailUsuario = result.EmailUsuario,
                TokenUsuario = result.TokenUsuario,
                DataNascimentoUsuario = result.DataNascimentoUsuario,
                CriadoEmUsuario = result.CriadoEmUsuario
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UsuarioRequestDto usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosUsuario(id, usuario);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Usuário atualizado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar o usuário";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(usuario);
        }
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterUsuarioPorId(id);
            
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
                var result = _applicationService.DeletarDadosUsuario(id);
                
                if (result != null)
                {
                    TempData["Success"] = "Usuário deletado com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar o usuário";
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
