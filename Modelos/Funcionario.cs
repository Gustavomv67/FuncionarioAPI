using FuncionarioAPI.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuncionarioAPI
{
    public class Funcionario
    {
        [Key]
        [Required]
        public int id { get; set; }

        public DateTime dataCriacao { get; set; }

        public DateTime dataAlteracao { get; set; }

        [Required]
        public string nome { get; set; }

        [Required]
        public string cpf { get; set; }

        public string rg { get; set; }

        [Required]
        public string cargo { get; set; }

        public List<EnderecoFuncionario> enderecos { get; set; }
    }
}
