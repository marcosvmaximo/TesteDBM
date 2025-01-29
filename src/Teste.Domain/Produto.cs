using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teste.Core;

namespace Teste.Domain;

public class Produto : Entity
{
    public Produto(string nome, string descricao, decimal preco)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
    }
    
    protected Produto() {}

    public string Nome { get; private set; }

    public string Descricao { get; private set; }

    public decimal Preco { get; private set; }

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public void AtualizarDados(string nome, string descricao, decimal preco)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
    }
}