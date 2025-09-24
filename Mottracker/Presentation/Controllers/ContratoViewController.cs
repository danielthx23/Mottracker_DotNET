using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Contrato;

namespace Mottracker.Presentation.Controllers
{
    public class ContratoViewController : Controller
    {
        private readonly IContratoApplicationService _applicationService;

        public ContratoViewController(IContratoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodosContratos();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar os contratos";
                return View(new List<ContratoResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterContratoPorId(id);
            
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
        public IActionResult Create(ContratoRequestDto contrato)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosContrato(contrato);
                    
                    if (result != null)
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
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterContratoPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new ContratoRequestDto
            {
                IdContrato = result.IdContrato,
                ClausulasContrato = result.ClausulasContrato,
                DataDeEntradaContrato = result.DataDeEntradaContrato,
                HorarioDeDevolucaoContrato = result.HorarioDeDevolucaoContrato,
                DataDeExpiracaoContrato = result.DataDeExpiracaoContrato,
                RenovacaoAutomaticaContrato = result.RenovacaoAutomaticaContrato,
                DataUltimaRenovacaoContrato = result.DataUltimaRenovacaoContrato,
                NumeroRenovacoesContrato = result.NumeroRenovacoesContrato,
                AtivoContrato = result.AtivoContrato,
                ValorToralContrato = result.ValorToralContrato,
                QuantidadeParcelas = result.QuantidadeParcelas,
                UsuarioContratoId = result.UsuarioContrato?.IdUsuario
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ContratoRequestDto contrato)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosContrato(id, contrato);
                    
                    if (result != null)
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
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterContratoPorId(id);
            
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
                var result = _applicationService.DeletarDadosContrato(id);
                
                if (result != null)
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
