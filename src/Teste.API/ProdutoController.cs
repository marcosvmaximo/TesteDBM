using Microsoft.AspNetCore.Mvc;
using Teste.Application.Dtos;
using Teste.Core.Notifications;
using Teste.Domain;
using Teste.Domain.Interfaces;

namespace Teste.API;

[Route("api/produtos")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoRepository _repository;
    private readonly IProdutoService _service;
    private readonly INotifiable _notifiable;

    public ProdutoController(IProdutoRepository repository, IProdutoService service, INotifiable notifiable)
    {
        _repository = repository;
        _service = service;
        _notifiable = notifiable;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> ObterProdutos()
    {
        return Ok(await _repository.GetAllAsync());
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> ObterProduto([FromRoute]int id)
    {
        var produto = await _repository.GetByIdAsync(id);
        if (produto is null) return NotFound();

        return Ok(produto);
    }
    
    [HttpPost]
    public async Task<ActionResult> AdicionarProduto([FromBody]ProdutoRequest request)
    {
        await _service.AdicionarProduto(request.Nome, request.Descricao, request.Preco);
        if (!_notifiable.IsValid) return BadRequest(_notifiable.Notifications);

        return Ok("Produto adicionado com sucesso.");
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> AtualizarProduto([FromRoute]int id, [FromBody]ProdutoRequest request)
    {
        await _service.AtualizarProduto(id, request.Nome, request.Descricao, request.Preco);
        if (!_notifiable.IsValid) return BadRequest(_notifiable.Notifications);

        return Ok("Produto atualizado com sucesso.");
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoverProduto([FromRoute]int id)
    {
        await _service.RemoverProduto(id);
        if (!_notifiable.IsValid) return BadRequest(_notifiable.Notifications);

        return Ok("Produto removido com sucesso.");
    }
}