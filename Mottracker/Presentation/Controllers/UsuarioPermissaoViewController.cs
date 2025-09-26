using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Application.Dtos.Usuario;
using Mottracker.Application.Dtos.Permissao;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mottracker.Presentation.Controllers
{
    public class UsuarioPermissaoViewController : Controller
    {
        private readonly IUsuarioPermissaoUseCase _useCase;
        private readonly IUsuarioUseCase _usuarioUseCase;
        private readonly IPermissaoUseCase _permissaoUseCase;

        public UsuarioPermissaoViewController(
            IUsuarioPermissaoUseCase useCase,
            IUsuarioUseCase usuarioUseCase,
            IPermissaoUseCase permissaoUseCase)
        {
            _useCase = useCase;
            _usuarioUseCase = usuarioUseCase;
            _permissaoUseCase = permissaoUseCase;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosUsuarioPermissoesAsync();

            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Nenhuma permissão de usuário encontrada.";
                return View(new List<UsuarioPermissaoResponseDto>());
            }

            return View(result.Value);
        }

        public async Task<IActionResult> Details(int usuarioId, int permissaoId)
        {
            var result = await _useCase.ObterUsuarioPermissaoPorIdAsync(usuarioId, permissaoId);

            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return View(result.Value);
        }

        public async Task<IActionResult> Create()
        {
            var usuariosResult = await _usuarioUseCase.ObterTodosUsuariosAsync();
            var permissoesResult = await _permissaoUseCase.ObterTodosPermissoesAsync();
            
            ViewBag.Usuarios = new SelectList(usuariosResult.Value ?? new List<UsuarioResponseDto>(), "IdUsuario", "NomeUsuario");
            ViewBag.Permissoes = new SelectList(permissoesResult.Value ?? new List<PermissaoResponseDto>(), "IdPermissao", "NomePermissao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioPermissaoRequestDto usuarioPermissao)
        {
            if (ModelState.IsValid)
            {
                var result = await _useCase.SalvarDadosUsuarioPermissaoAsync(usuarioPermissao);

                if (result != null && result.IsSuccess && result.Value != null)
                {
                    TempData["Success"] = "Permissão de usuário criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Não foi possível criar a permissão de usuário.";
                }
            }
            var usuariosResult = await _usuarioUseCase.ObterTodosUsuariosAsync();
            var permissoesResult = await _permissaoUseCase.ObterTodosPermissoesAsync();
            
            ViewBag.Usuarios = new SelectList(usuariosResult.Value ?? new List<UsuarioResponseDto>(), "IdUsuario", "NomeUsuario", usuarioPermissao.IdUsuario);
            ViewBag.Permissoes = new SelectList(permissoesResult.Value ?? new List<PermissaoResponseDto>(), "IdPermissao", "NomePermissao", usuarioPermissao.IdPermissao);
            return View(usuarioPermissao);
        }

        public async Task<IActionResult> Edit(int usuarioId, int permissaoId)
        {
            var result = await _useCase.ObterUsuarioPermissaoPorIdAsync(usuarioId, permissaoId);

            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new UsuarioPermissaoRequestDto
            {
                IdUsuario = result.Value.IdUsuario,
                IdPermissao = result.Value.IdPermissao
            };
            var usuariosResult = await _usuarioUseCase.ObterTodosUsuariosAsync();
            var permissoesResult = await _permissaoUseCase.ObterTodosPermissoesAsync();

            ViewBag.Usuarios = new SelectList(usuariosResult.Value ?? new List<UsuarioResponseDto>(), "IdUsuario", "NomeUsuario", requestDto.IdUsuario);
            ViewBag.Permissoes = new SelectList(permissoesResult.Value ?? new List<PermissaoResponseDto>(), "IdPermissao", "NomePermissao", requestDto.IdPermissao);
            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto usuarioPermissao)
        {
            if (ModelState.IsValid)
            {
                var result = await _useCase.EditarDadosUsuarioPermissaoAsync(usuarioId, permissaoId, usuarioPermissao);

                if (result != null && result.IsSuccess && result.Value != null)
                {
                    TempData["Success"] = "Permissão de usuário atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Não foi possível atualizar a permissão de usuário.";
                }
            }
            var usuariosResult = await _usuarioUseCase.ObterTodosUsuariosAsync();
            var permissoesResult = await _permissaoUseCase.ObterTodosPermissoesAsync();
            
            ViewBag.Usuarios = new SelectList(usuariosResult.Value ?? new List<UsuarioResponseDto>(), "IdUsuario", "NomeUsuario", usuarioPermissao.IdUsuario);
            ViewBag.Permissoes = new SelectList(permissoesResult.Value ?? new List<PermissaoResponseDto>(), "IdPermissao", "NomePermissao", usuarioPermissao.IdPermissao);
            return View(usuarioPermissao);
        }

        public async Task<IActionResult> Delete(int usuarioId, int permissaoId)
        {
            var result = await _useCase.ObterUsuarioPermissaoPorIdAsync(usuarioId, permissaoId);

            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return View(result.Value);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int usuarioId, int permissaoId)
        {
            var result = await _useCase.DeletarDadosUsuarioPermissaoAsync(usuarioId, permissaoId);

            if (result != null && result.IsSuccess)
            {
                TempData["Success"] = "Permissão de usuário deletada com sucesso!";
            }
            else
            {
                TempData["Error"] = "Não foi possível deletar a permissão de usuário.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
