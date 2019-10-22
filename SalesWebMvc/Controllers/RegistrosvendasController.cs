using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Service;

namespace SalesWebMvc.Controllers
{
    public class RegistrosVendasController : Controller
    {
        private readonly RegistrosVendasService _registrosVendasService;


        public RegistrosVendasController(RegistrosVendasService registrosVendasService)
        {
            _registrosVendasService = registrosVendasService;
        }

        public  IActionResult Index()
        {
            return View();

        }

        public async Task<IActionResult> BuscaSimplesAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = await _registrosVendasService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public IActionResult BuscaAgrupada()
        {
            return View();
        }
    }
}