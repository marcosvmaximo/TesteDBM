using Teste.Core.Services;

namespace Teste.Domain.Interfaces;

public interface IProdutoService : IService
{
    Task AdicionarProduto(string nome, string descricao, decimal preco);
    Task AtualizarProduto(int id, string nome, string descricao, decimal preco);
    Task RemoverProduto(int id);
    Task<bool> VerificarNomeUnico(string nome);
}