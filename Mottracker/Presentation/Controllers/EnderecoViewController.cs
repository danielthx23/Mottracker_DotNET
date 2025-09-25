using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Endereco;

namespace Mottracker.Presentation.Controllers
{
    public class EnderecoViewController : Controller
    {
        private readonly IEnderecoUseCase _useCase;

        public EnderecoViewController(IEnderecoUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosEnderecosAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar os endereços";
                return View(new List<EnderecoResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterEnderecoPorIdAsync(id);
            
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
        public async Task<IActionResult> Create(EnderecoRequestDto endereco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarDadosEnderecoAsync(endereco);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
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
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterEnderecoPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new EnderecoRequestDto
            {
                IdEndereco = result.Value.IdEndereco,
                CEP = result.Value.CEP,
                Logradouro = result.Value.Logradouro,
                Numero = result.Value.Numero,
                Complemento = result.Value.Complemento,
                Bairro = result.Value.Bairro,
                Cidade = result.Value.Cidade,
                Estado = result.Value.Estado,
                PatioEnderecoId = result.Value.PatioEndereco?.IdPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EnderecoRequestDto endereco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarDadosEnderecoAsync(id, endereco);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
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
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterEnderecoPorIdAsync(id);
            
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
                var result = await _useCase.DeletarDadosEnderecoAsync(id);
                
                if (result != null && result.IsSuccess)
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
