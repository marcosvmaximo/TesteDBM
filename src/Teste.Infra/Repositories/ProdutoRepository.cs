using Microsoft.EntityFrameworkCore;
using Teste.Core;
using Teste.Domain;
using Teste.Domain.Interfaces;

namespace Teste.Infra.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly DataContext _context;

    public ProdutoRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto?>> GetAllAsync()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task AddAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Produto produto)
    {
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
    }

    public async Task<Produto?> ObterProdutoPorNome(string nome)
    {
        return await _context.Produtos.FirstOrDefaultAsync(x => x.Nome.ToLower() == nome.ToLower());
    }
}