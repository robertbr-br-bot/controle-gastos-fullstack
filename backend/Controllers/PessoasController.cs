using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;

namespace backend.Controllers;

// Controller responsável pelas operações da entidade Pessoa.
[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    // Contexto responsável pela comunicação com o banco SQLite.
    private readonly AppDbContext _context;

    // Recebe o contexto do banco através da injeção de dependência.
    public PessoasController(AppDbContext context)
    {
        _context = context;
    }

    // Retorna todas as pessoas cadastradas no banco.
    [HttpGet]
    public List<Pessoa> ListarPessoas()
    {
        return _context.Pessoas.ToList();
    }

    // Cadastra uma nova pessoa.
    [HttpPost]
    public IActionResult CadastrarPessoa(Pessoa pessoa)
    {
        // Adiciona a pessoa no banco.
        _context.Pessoas.Add(pessoa);

        // Salva as alterações.
        _context.SaveChanges();

        return Ok(pessoa);
    }

    // Remove uma pessoa pelo identificador informado.
    [HttpDelete("{id}")]
    public void DeletarPessoa(int id)
    {
        var pessoa = _context.Pessoas.Find(id);

        if (pessoa == null)
        {
            return;
        }

        // Remove a pessoa do banco.
        _context.Pessoas.Remove(pessoa);


        // Salva a alteração no banco de dados.
        _context.SaveChanges();
    }

}
