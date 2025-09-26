using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Contrato;

namespace Mottracker.Presentation.Controllers
{
    public class ContratoViewController : Controller
    {
        private readonly IContratoUseCase _useCase;

        public ContratoViewController(IContratoUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodosContratosAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar os contratos";
                return View(new List<ContratoResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterContratoPorIdAsync(id);
            
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
        public async Task<IActionResult> Create(ContratoRequestDto contrato)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarContratoAsync(contrato);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
                    {
                        TempData["Success"] = "Contrato criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar o contrato";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(contrato);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterContratoPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new ContratoRequestDto
            {
                IdContrato = result.Value.IdContrato,
                ClausulasContrato = result.Value.ClausulasContrato,
                DataDeEntradaContrato = result.Value.DataDeEntradaContrato,
                HorarioDeDevolucaoContrato = result.Value.HorarioDeDevolucaoContrato,
                DataDeExpiracaoContrato = result.Value.DataDeExpiracaoContrato,
                RenovacaoAutomaticaContrato = result.Value.RenovacaoAutomaticaContrato,
                DataUltimaRenovacaoContrato = result.Value.DataUltimaRenovacaoContrato,
                NumeroRenovacoesContrato = result.Value.NumeroRenovacoesContrato,
                AtivoContrato = result.Value.AtivoContrato,
                ValorToralContrato = result.Value.ValorToralContrato,
                QuantidadeParcelas = result.Value.QuantidadeParcelas,
                UsuarioContratoId = result.Value.UsuarioContrato?.IdUsuario
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContratoRequestDto contrato)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarContratoAsync(id, contrato);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
                    {
                        TempData["Success"] = "Contrato atualizado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar o contrato";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(contrato);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterContratoPorIdAsync(id);
            
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
                var result = await _useCase.DeletarContratoAsync(id);
                
                if (result != null && result.IsSuccess)
                {
                    TempData["Success"] = "Contrato deletado com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar o contrato";
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
