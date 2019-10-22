using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Service;
using SalesWebMvc.Service.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class VendedoresController : Controller
    {

        private readonly VendedorService _vendedorSevice;
        private readonly DepartamentoService _departamentoService;


        public VendedoresController(VendedorService vendedorSevice, DepartamentoService departamentoService)
        {
            _vendedorSevice = vendedorSevice;
            _departamentoService = departamentoService;
        }



        public async Task<IActionResult> Index()
        {
            var list = await _vendedorSevice.FindAllAsync(); // Retorna a lista de vendedores para a View
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoService.FinAllAsync(); // Chamando a função q busca todos os departamentos, p/ mostrar na tela.
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos }; // Criando um novo form de vendedor e passando como argumento a lista do metodo acima   
            return View(viewModel);
        }

        [HttpPost] // Anotação p/ indicar q será um post.
        [ValidateAntiForgeryToken] // Prevenção de ataques maliciosos
        public async Task<IActionResult> Create(Vendedor vendedor) // Add novo vendedor
        {
            if (!ModelState.IsValid)// Verifica se todos os campos foram digitados corretamente.
            {
                return View(vendedor);
            }
            await _vendedorSevice.InsertAsync(vendedor);
            return RedirectToAction(nameof(Index)); // Redireciona p/ a index.
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var obj = await _vendedorSevice.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id inexistente!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vendedorSevice.RemoveAsync(id); // Removendo 
                return RedirectToAction(nameof(Index)); // Redirecionando para index.
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var obj = await _vendedorSevice.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id inexistente!" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null) // Testanto se o id existe
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido!" });
            }

            var obj = await _vendedorSevice.FindByIdAsync(id.Value); // Testando se o id é null
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não inexistente!" });
            }

            List<Departamento> departamentos = await _departamentoService.FinAllAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)// Verifica se todos os campos foram digitados corretamente.
            {
                return View(vendedor);
            }

            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não correspodem!" });
            }
            try
            {
               await _vendedorSevice.UpdateAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }

        public IActionResult Error (string message)
        {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}