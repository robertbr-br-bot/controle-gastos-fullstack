namespace backend.Models;

// Modelo que representa uma transação financeira.
public class Transacao
{
    // Identificador único da transação.
    public int Id { get; set; }

    // Descrição da transação.
    public string Descricao { get; set; } = string.Empty;

    // Valor da transação.
    public decimal Valor { get; set; }

    // Tipo da transação: Receita ou Despesa.
    public string Tipo { get; set; } = string.Empty;

    // Identificador da pessoa responsável pela transação.
    public int PessoaId { get; set; }

    // Pessoa responsável por esta transação.
    public Pessoa? Pessoa { get; set; }
}