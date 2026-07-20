namespace backend.Models;

// Modelo que representa o resumo financeiro de uma pessoa.
public class ResumoPessoa
{
    // Nome da pessoa.
    public string Nome { get; set; } = string.Empty;

    // Total de receitas da pessoa.
    public decimal TotalReceitas { get; set; }

    // Total de despesas da pessoa.
    public decimal TotalDespesas { get; set; }

    // Saldo final da pessoa.
    public decimal Saldo { get; set; }
}