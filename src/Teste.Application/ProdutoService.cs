using FluentValidation;
using FluentValidation.Results;
using Teste.Core;
using Teste.Core.Notifications;
using Teste.Domain;
using Teste.Domain.Interfaces;

namespace Teste.Application;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;
    private readonly INotifiable _notifiable;

    public ProdutoService(IProdutoRepository repository, INotifiable notifiable)
    {
        _repository = repository;
        _notifiable = notifiable;
    }

    public async Task AdicionarProduto(string nome, string descricao, decimal preco)
    {
        var existeProduto = await _repository.ObterProdutoPorNome(nome);
        if (existeProduto is not null)
        {
            _notifiable.AddNotification($"Produto: {nome}", "Produto informado já existente.");
            return;
        }
        
        Produto produto = new Produto(nome, descricao, preco);
        
        var ehValido = await Validate<Produto, ProdutoValidator>(produto);
        if (ehValido)
        {
            await _repository.AddAsync(produto);
        }
    }

    public async Task AtualizarProduto(int id, string nome, string descricao, decimal preco)
    {
        var produto = await _repository.GetByIdAsync(id);
        
        if (produto is null)
        {
            _notifiable.AddNotification("Produto", "Produto não encontrado.");
            return;
        }
        
        produto.AtualizarDados(nome, descricao, preco);
        
        var ehValido = await Validate<Produto, ProdutoValidator>(produto);
        if (ehValido)
        {
            await _repository.UpdateAsync(produto);
        }
    }

    public async Task RemoverProduto(int id)
    {
        var produto = await _repository.GetByIdAsync(id);
        
        if (produto is null)
        {
            _notifiable.AddNotification("Produto", "Produto não encontrado.");
            return;
        }
        
        await _repository.DeleteAsync(produto);
    }

    public async Task<bool> VerificarNomeUnico(string nome)
    {
        var produto = await _repository.ObterProdutoPorNome(nome);
        if (produto is null) return true;

        return false;
    }

    public async Task<bool> Validate<TEntity, TValidator>(TEntity entity) 
        where TEntity : Entity
        where TValidator : AbstractValidator<TEntity>, new()
    {
        TValidator validator = new TValidator();
        ValidationResult results = await validator.ValidateAsync(entity);
        
        if(!results.IsValid) 
        {
            foreach(var failure in results.Errors)
            {
                _notifiable.AddNotification($"Property: {failure.PropertyName}", failure.ErrorMessage);
            }

            return false;
        }

        return true;
    }

}