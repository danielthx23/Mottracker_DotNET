using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Endereco;

namespace Mottracker.Presentation.Controllers
{
    public class EnderecoViewController : Controller
    {
        private readonly IEnderecoApplicationService _applicationService;

        public EnderecoViewController(IEnderecoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosEnderecos();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar os endereços";
                return View(new List<EnderecoResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterEnderecoPorId(id);
            
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
        public IActionResult Create(EnderecoRequestDto endereco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosEndereco(endereco);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Endereço criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar o endereço";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(endereco);
        }
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterEnderecoPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new EnderecoRequestDto
            {
                IdEndereco = result.IdEndereco,
                CepEndereco = result.CepEndereco,
                LogradouroEndereco = result.LogradouroEndereco,
                NumeroEndereco = result.NumeroEndereco,
                ComplementoEndereco = result.ComplementoEndereco,
                BairroEndereco = result.BairroEndereco,
                CidadeEndereco = result.CidadeEndereco,
                EstadoEndereco = result.EstadoEndereco,
                PatioEnderecoId = result.PatioEndereco?.IdPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EnderecoRequestDto endereco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosEndereco(id, endereco);
                    
                    if (result != null)
                    {
                        TempData["Success"] = "Endereço atualizado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar o endereço";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(endereco);
        }
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterEnderecoPorId(id);
            
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
                var result = _applicationService.DeletarDadosEndereco(id);
                
                if (result != null)
                {
                    TempData["Success"] = "Endereço deletado com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar o endereço";
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
