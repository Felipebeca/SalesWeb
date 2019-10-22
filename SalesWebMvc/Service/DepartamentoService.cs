using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Service
{
    public class DepartamentoService
    {
        private readonly SalesWebMvcContext _context;



        public DepartamentoService(SalesWebMvcContext context)
        {
            _context = context;
        }


        public async Task<List<Departamento>> FinAllAsync()
        {
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync(); // Retona a lista de departamento em ordem alfabetica
        }
    }
}
