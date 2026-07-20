namespace backend.Models;

// Modelo que representa uma pessoa no sistema.
public class Pessoa
{
    // Identificador único da pessoa.
    public int Id { get; set; }

    // Nome da pessoa.
    public string Nome { get; set; } = string.Empty;

    // Idade será utilizada nas regras de negócio,
    // como impedir receitas para menores de idade.
    public int Idade { get; set; }
}
