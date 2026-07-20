using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data;

// Contexto responsável pela comunicação entre a aplicação e o banco.
// O Entity Framework usa essa classe para criar tabelas,
// consultar dados e salvar alterações no SQLite.
public class AppDbContext : DbContext
{
    // Recebe as configurações do banco definidas no Program.cs.
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Representa a tabela de pessoas no banco.
    public DbSet<Pessoa> Pessoas { get; set; }

    // Representa a tabela de transações no banco.
    public DbSet<Transacao> Transacoes { get; set; }
}