using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Moto;

namespace Mottracker.Presentation.Controllers
{
    public class MotoViewController : Controller
    {
        private readonly IMotoUseCase _useCase;

        public MotoViewController(IMotoUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _useCase.ObterTodasMotosAsync();
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                TempData["Error"] = "Erro ao carregar as motos";
                return View(new List<MotoResponseDto>());
            }

            return View(result.Value);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _useCase.ObterMotoPorIdAsync(id);
            
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
        public async Task<IActionResult> Create(MotoRequestDto moto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.SalvarDadosMotoAsync(moto);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
                    {
                        TempData["Success"] = "Moto criada com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível criar a moto";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(moto);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _useCase.ObterMotoPorIdAsync(id);
            
            if (result == null || !result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            var requestDto = new MotoRequestDto
            {
                IdMoto = result.Value.IdMoto,
                PlacaMoto = result.Value.PlacaMoto,
                ModeloMoto = result.Value.ModeloMoto,
                AnoMoto = result.Value.AnoMoto,
                IdentificadorMoto = result.Value.IdentificadorMoto,
                QuilometragemMoto = result.Value.QuilometragemMoto,
                EstadoMoto = result.Value.EstadoMoto,
                CondicoesMoto = result.Value.CondicoesMoto,
                MotoPatioOrigemId = result.Value.MotoPatioOrigemId,
                ContratoMotoId = result.Value.ContratoMoto?.IdContrato,
                MotoPatioAtualId = result.Value.MotoPatioAtual?.IdPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MotoRequestDto moto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _useCase.EditarDadosMotoAsync(id, moto);
                    
                    if (result != null && result.IsSuccess && result.Value != null)
                    {
                        TempData["Success"] = "Moto atualizada com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Não foi possível atualizar a moto";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(moto);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _useCase.ObterMotoPorIdAsync(id);
            
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
                var result = await _useCase.DeletarDadosMotoAsync(id);
                
                if (result != null && result.IsSuccess)
                {
                    TempData["Success"] = "Moto deletada com sucesso!";
                }
                else
                {
                    TempData["Error"] = "Não foi possível deletar a moto";
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
