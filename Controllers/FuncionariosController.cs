using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuncionarioAPI;
using FuncionarioAPI.Modelos;

namespace FuncionarioAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly FuncionariosContext _context;

        public FuncionariosController(FuncionariosContext context)
        {
            _context = context;
        }

        // GET: api/Funcionarios
        [HttpGet]
        [Route("list")]

        public async Task<ActionResult<IEnumerable<Funcionario>>> Getfuncionarios()
        {
            return await _context.funcionarios.Include(a => a.enderecos).ToListAsync();
        }

        // GET: api/Funcionarios/5
        [HttpGet("{id}")]
        [Route("show/{id}")]

        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _context.funcionarios.Include(a => a.enderecos).FirstOrDefaultAsync(a => a.id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        // PUT: api/Funcionarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{id}")]
        [Route("update/{id}")]

        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (id != funcionario.id)
            {
                return BadRequest();
            }
            funcionario.dataAlteracao = DateTime.Now;
            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
            }

        // POST: api/Funcionarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("create")]

        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            funcionario.dataCriacao = DateTime.Now;
            _context.funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionario", new { id = funcionario.id }, funcionario);
        }

        [HttpPost]
        [Route("endereco/{id}")]
        public async Task<ActionResult<Funcionario>> AdicionarEndereco(int id, EnderecoFuncionario endereco)
        {
            Funcionario funcionario = _context.funcionarios.Find(id);
            if (funcionario.enderecos == null)
                funcionario.enderecos = new List<EnderecoFuncionario>();
            funcionario.enderecos.Add(endereco);
            funcionario.dataAlteracao = DateTime.Now;
            _context.funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionario", new { id }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        [HttpPost("{id}")]
        [Route("destroy/{id}")]

        public async Task<ActionResult<Funcionario>> DeleteFuncionario(int id)
        {
            var funcionario = await _context.funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return funcionario;
        }

        private bool FuncionarioExists(int id)
        {
            return _context.funcionarios.Any(e => e.id == id);
        }
    }
}
