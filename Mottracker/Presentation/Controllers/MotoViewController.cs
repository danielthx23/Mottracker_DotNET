using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Moto;

namespace Mottracker.Presentation.Controllers
{
    public class MotoViewController : Controller
    {
        private readonly IMotoApplicationService _applicationService;

        public MotoViewController(IMotoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var result = _applicationService.ObterTodasMotos();
            
            if (result == null)
            {
                TempData["Error"] = "Erro ao carregar as motos";
                return View(new List<MotoResponseDto>());
            }

            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var result = _applicationService.ObterMotoPorId(id);
            
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
        public IActionResult Create(MotoRequestDto moto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.SalvarDadosMoto(moto);
                    
                    if (result != null)
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
        
        public IActionResult Edit(int id)
        {
            var result = _applicationService.ObterMotoPorId(id);
            
            if (result == null)
            {
                return NotFound();
            }

            var requestDto = new MotoRequestDto
            {
                IdMoto = result.IdMoto,
                PlacaMoto = result.PlacaMoto,
                ModeloMoto = result.ModeloMoto,
                AnoMoto = result.AnoMoto,
                IdentificadorMoto = result.IdentificadorMoto,
                QuilometragemMoto = result.QuilometragemMoto,
                EstadoMoto = result.EstadoMoto,
                CondicoesMoto = result.CondicoesMoto,
                MotoPatioOrigemId = result.MotoPatioOrigemId,
                ContratoMotoId = result.ContratoMoto?.IdContrato,
                MotoPatioAtualId = result.MotoPatioAtual?.IdPatio
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MotoRequestDto moto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _applicationService.EditarDadosMoto(id, moto);
                    
                    if (result != null)
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
        
        public IActionResult Delete(int id)
        {
            var result = _applicationService.ObterMotoPorId(id);
            
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
                var result = _applicationService.DeletarDadosMoto(id);
                
                if (result != null)
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
