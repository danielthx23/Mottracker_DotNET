using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Presentation.Controllers
{
    public class UsuarioViewController : Controller
    {
        private readonly IUsuarioUseCase _useCase;

        public UsuarioViewController(IUsuarioUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosUsuariosAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar os usuários";
                return View(new List<UsuarioResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterUsuarioPorIdAsync(id);
            
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
        public async Task<IActionResult> Create(UsuarioRequestDto usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarDadosUsuarioAsync(usuario);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
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
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterUsuarioPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new UsuarioRequestDto
            {
                IdUsuario = result.Value.IdUsuario,
                NomeUsuario = result.Value.NomeUsuario,
                CPFUsuario = result.Value.CPFUsuario,
                SenhaUsuario = result.Value.SenhaUsuario,
                CNHUsuario = result.Value.CNHUsuario,
                EmailUsuario = result.Value.EmailUsuario,
                TokenUsuario = result.Value.TokenUsuario,
                DataNascimentoUsuario = result.Value.DataNascimentoUsuario,
                CriadoEmUsuario = result.Value.CriadoEmUsuario
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioRequestDto usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarDadosUsuarioAsync(id, usuario);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
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
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterUsuarioPorIdAsync(id);
            
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
                var result = await _useCase.DeletarDadosUsuarioAsync(id);
                
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
