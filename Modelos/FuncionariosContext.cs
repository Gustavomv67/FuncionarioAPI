using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuncionarioAPI.Modelos
{
    public class FuncionariosContext : DbContext
    {
        public FuncionariosContext(DbContextOptions<FuncionariosContext> options)
                   : base(options)
        {
        }

        public DbSet<Funcionario> funcionarios { get; set; }
        public DbSet<EnderecoFuncionario> enderecos { get; set; }
    }
}
