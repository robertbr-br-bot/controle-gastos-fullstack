namespace backend.Models;

// Modelo que representa o resumo financeiro completo da aplicação.
public class ResumoGeral
{
    // Lista com o resumo financeiro de cada pessoa.
    public List<ResumoPessoa> Pessoas { get; set; } = new();

    // Total geral de receitas.
    public decimal TotalReceitas { get; set; }

    // Total geral de despesas.
    public decimal TotalDespesas { get; set; }

    // Saldo geral da aplicação.
    public decimal Saldo { get; set; }
}