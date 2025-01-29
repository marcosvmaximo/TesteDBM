using Teste.Core.Services;

namespace Teste.Domain.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<Produto?> ObterProdutoPorNome(string nome);
}