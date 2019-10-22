using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Vendedor> Vendedor { get; set; } = new List<Vendedor>();

        public Departamento()
        {
        }

        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }


        public void AddVendedor(Vendedor vendedor) // Add vendedor
        {
            Vendedor.Add(vendedor);
        }

        public double TotalVendas(DateTime inicial, DateTime final) // Faz a soma do total de vendas do departamento.
        {
            return Vendedor.Sum(vendedor => vendedor.TotalVendas(inicial, final));
        }





    }
}
