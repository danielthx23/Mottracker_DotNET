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
        private readonly IUsuarioPermissaoApplicationService _applicationService;
        private readonly IUsuarioApplicationService _usuarioApplicationService;
        private readonly IPermissaoApplicationService _permissaoApplicationService;

        public UsuarioPermissaoViewController(
            IUsuarioPermissaoApplicationService applicationService,
            IUsuarioApplicationService usuarioApplicationService,
            IPermissaoApplicationService permissaoApplicationService)
        {
            _applicationService = applicationService;
            _usuarioApplicationService = usuarioApplicationService;
            _permissaoApplicationService = permissaoApplicationService;
        }

        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosUsuarioPermissoes();

            if (result == null || !result.Any())
            {
                TempData["Error"] = "Nenhuma permissão de usuário encontrada.";
                return View(new List<UsuarioPermissaoResponseDto>());
            }

            return View(result);
        }

        public IActionResult Details(int usuarioId, int permissaoId)
        {
            var result = _applicationService.ObterUsuarioPermissaoPorId(usuarioId, permissaoId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        public IActionResult Create()
        {
            ViewBag.Usuarios = new SelectList(_usuarioApplicationService.ObterTodosUsuarios(), "IdUsuario", "NomeUsuario");
            ViewBag.Permissoes = new SelectList(_permissaoApplicationService.ObterTodosPermissoes(), "IdPermissao", "NomePermissao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuarioPermissaoRequestDto usuarioPermissao)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.SalvarDadosUsuarioPermissao(usuarioPermissao);

                if (result != null)
                {
                    TempData["Success"] = "Permissão de usuário criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Não foi possível criar a permissão de usuário.";
                }
            }
            ViewBag.Usuarios = new SelectList(_usuarioApplicationService.ObterTodosUsuarios(), "IdUsuario", "NomeUsuario", usuarioPermissao.IdUsuario);
            ViewBag.Permissoes = new SelectList(_permissaoApplicationService.ObterTodosPermissoes(), "IdPermissao", "NomePermissao", usuarioPermissao.IdPermissao);
            return View(usuarioPermissao);
        }

        public IActionResult Edit(int usuarioId, int permissaoId)
        {
            var result = _applicationService.ObterUsuarioPermissaoPorId(usuarioId, permissaoId);

            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new UsuarioPermissaoRequestDto
            {
                IdUsuario = result.IdUsuario,
                IdPermissao = result.IdPermissao
            };

            ViewBag.Usuarios = new SelectList(_usuarioApplicationService.ObterTodosUsuarios(), "IdUsuario", "NomeUsuario", requestDto.IdUsuario);
            ViewBag.Permissoes = new SelectList(_permissaoApplicationService.ObterTodosPermissoes(), "IdPermissao", "NomePermissao", requestDto.IdPermissao);
            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto usuarioPermissao)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.EditarDadosUsuarioPermissao(usuarioId, permissaoId, usuarioPermissao);

                if (result != null)
                {
                    TempData["Success"] = "Permissão de usuário atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Não foi possível atualizar a permissão de usuário.";
                }
            }
            ViewBag.Usuarios = new SelectList(_usuarioApplicationService.ObterTodosUsuarios(), "IdUsuario", "NomeUsuario", usuarioPermissao.IdUsuario);
            ViewBag.Permissoes = new SelectList(_permissaoApplicationService.ObterTodosPermissoes(), "IdPermissao", "NomePermissao", usuarioPermissao.IdPermissao);
            return View(usuarioPermissao);
        }

        public IActionResult Delete(int usuarioId, int permissaoId)
        {
            var result = _applicationService.ObterUsuarioPermissaoPorId(usuarioId, permissaoId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int usuarioId, int permissaoId)
        {
            var result = _applicationService.DeletarDadosUsuarioPermissao(usuarioId, permissaoId);

            if (result != null)
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
