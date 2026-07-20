namespace backend.Models;

// Modelo que representa o resumo financeiro da aplicação.
public class ResumoFinanceiro
{
    // Soma de todas as receitas.
    public decimal TotalReceitas { get; set; }

    // Soma de todas as despesas.
    public decimal TotalDespesas { get; set; }

    // Resultado das receitas menos despesas.
    public decimal Saldo { get; set; }
}