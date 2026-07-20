using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;

namespace backend.Controllers;

// Controller responsável pelas operações da entidade Transação.
[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    // Contexto responsável pela comunicação com o banco de dados.
    private readonly AppDbContext _context;

    // Recebe o contexto através da injeção de dependência.
    public TransacoesController(AppDbContext context)
    {
        _context = context;
    }

    // Retorna todas as transações cadastradas.
    [HttpGet]
    public List<Transacao> ListarTransacoes()
    {
        return _context.Transacoes.ToList();
    }

    // Cadastra uma nova transação.
    [HttpPost]
    public IActionResult CadastrarTransacao(Transacao transacao)
    {
        // Busca a pessoa relacionada à transação pelo ID informado.
        var pessoa = _context.Pessoas.Find(transacao.PessoaId);

        // Verifica se a pessoa existe.
        if (pessoa == null)
        {
            return NotFound("Pessoa não encontrada.");
        }

        // Regra de negócio:
        // Pessoas menores de 18 anos só podem cadastrar despesas.
        if (pessoa.Idade < 18 && transacao.Tipo == "Receita")
        {
            return BadRequest("Menores de 18 anos não podem cadastrar receitas.");
        }

        // Adiciona a transação no banco.
        _context.Transacoes.Add(transacao);

        // Salva as alterações.
        _context.SaveChanges();

        // Retorna a transação criada.
        return Ok(transacao);
    }

    // Retorna o resumo financeiro da aplicação.
[HttpGet("totais")]
public ResumoGeral ConsultarTotais()
{
    // Busca todas as pessoas cadastradas.
    var pessoas = _context.Pessoas.ToList();

    // Cria a lista de resumo individual.
    var resumoPessoas = pessoas.Select(pessoa => new ResumoPessoa
    {
        Nome = pessoa.Nome,

        TotalReceitas = _context.Transacoes
            .Where(t => t.PessoaId == pessoa.Id && t.Tipo == "Receita")
            .AsEnumerable()
            .Sum(t => t.Valor),

        TotalDespesas = _context.Transacoes
            .Where(t => t.PessoaId == pessoa.Id && t.Tipo == "Despesa")
            .AsEnumerable()
            .Sum(t => t.Valor)
    }).ToList();


    // Calcula o saldo de cada pessoa.
    foreach (var pessoa in resumoPessoas)
    {
        pessoa.Saldo = pessoa.TotalReceitas - pessoa.TotalDespesas;
    }


    // Retorna o resumo completo.
    return new ResumoGeral
    {
        Pessoas = resumoPessoas,

        TotalReceitas = resumoPessoas.Sum(p => p.TotalReceitas),

        TotalDespesas = resumoPessoas.Sum(p => p.TotalDespesas),

        Saldo = resumoPessoas.Sum(p => p.Saldo)
    };
}
}

