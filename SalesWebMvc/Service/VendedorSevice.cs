using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Service.Exceptions;

namespace SalesWebMvc.Service
{
    public class VendedorService
    {
        private readonly SalesWebMvcContext _context;



        public VendedorService(SalesWebMvcContext context)
        {
            _context = context;
        }



        public async Task<List<Vendedor>> FindAllAsync()
        {
            return await _context.Vendedor.ToListAsync(); // Retorna a lista de todos o vendedores.
        }

        public async Task InsertAsync (Vendedor obj) // Add novo vendedor no banco.
        {
            _context.Add(obj);
           await _context.SaveChangesAsync();
        }

        public async Task<Vendedor> FindByIdAsync(int id)
        {
              return await _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefaultAsync(obj => obj.Id == id); // Retorna o id do vendedor e o departamento do banco de dados
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
 
            }
        }

        public async Task UpdateAsync (Vendedor obj)
        {
            bool hasAny = await _context.Vendedor.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) // Testando antes de atualizar 
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
            
    }
}
