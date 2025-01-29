using FluentValidation.TestHelper;
using Moq;
using Teste.Application;
using Teste.Domain;
using Teste.Domain.Interfaces;

namespace Teste.Testes
{
    public class ProdutoValidatorTests
    {
        private readonly ProdutoValidator _validator;
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

        public ProdutoValidatorTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _validator = new ProdutoValidator();
        }

        [Fact]
        public async Task Nome_DeveSerObrigatorio()
        {
            var produto = new Produto("", "Descrição válida", 50);
            var resultado = await _validator.TestValidateAsync(produto);
            resultado.ShouldHaveValidationErrorFor(p => p.Nome);
        }

        [Fact]
        public async Task Nome_NaoDeveUltrapassar100Caracteres()
        {
            var produto = new Produto(new string('A', 101), "Descrição válida", 50);
            var resultado = await _validator.TestValidateAsync(produto);
            resultado.ShouldHaveValidationErrorFor(p => p.Nome);
        }

        [Fact]
        public async Task Preco_DeveSerMaiorQueZero()
        {
            var produto = new Produto("Produto", "Descrição válida", 0);
            var resultado = await _validator.TestValidateAsync(produto);
            resultado.ShouldHaveValidationErrorFor(p => p.Preco);
        }

        [Fact]
        public async Task Preco_NaoPodeSerNulo()
        {
            var produto = new Produto("Produto", "Descrição válida", default);
            var resultado = await _validator.TestValidateAsync(produto);
            resultado.ShouldHaveValidationErrorFor(p => p.Preco);
        }

        [Fact]
        public async Task Descricao_NaoDeveUltrapassar255Caracteres()
        {
            var produto = new Produto("Produto", new string('A', 256), 50);
            var resultado = await _validator.TestValidateAsync(produto);
            resultado.ShouldHaveValidationErrorFor(p => p.Descricao);
        }
    }
}