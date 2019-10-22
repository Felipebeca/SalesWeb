using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} Obrigatório!")]// Indica q este campo é obrigatório.
        [StringLength (50, MinimumLength = 3, ErrorMessage = "tamanho do {0} deve ser entre {2} e {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório!")]// Indica q este campo é obrigatório.
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Salario Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double SalarioBase { get; set; }

        public Departamento Departamento { get; set; }
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }
        public ICollection<RegistroVendas> Registro { get; set; } = new List<RegistroVendas>();



        public Vendedor()
        {
        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void AddVendas(RegistroVendas rv) // Add um vendedor 
        {
            Registro.Add(rv);

        }

        public void RemoveVndas(RegistroVendas rv) // Remove um vendedor
        {
            Registro.Add(rv);
        }

        public double TotalVendas(DateTime inicial, DateTime final) // Calcula o total de vendas do vendedor no período destas datas.
        {
            return Registro.Where(rv => rv.Data >= inicial && rv.Data <= final).Sum(rv => rv.Montante);
        }





    }
}
