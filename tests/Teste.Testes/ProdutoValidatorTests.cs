using FluentAssertions;
using Moq;
using Teste.Application;
using Teste.Domain;
using Teste.Domain.Interfaces;

namespace Teste.Testes
{
    public class ProdutoValidatorTests
    {
        private readonly ProdutoValidator _validator;
        private readonly Mock<IProdutoService> _produtoServiceMock;

        public ProdutoValidatorTests()
        {
            _produtoServiceMock = new Mock<IProdutoService>();
            _validator = new ProdutoValidator();
        }

        [Fact]
        public void Nome_DeveSerObrigatorio()
        {
            // Arrange
            var produto = new Produto(null, "Descrição", 50);

            // Act
            var resultado = _validator.Validate(produto);

            // Assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Nome_DeveSerUnico()
        {
            // Arrange
            var produto = new Produto("Produto Existente", "Descrição", 50);
            _produtoServiceMock.Setup(x => x.VerificarNomeUnico(produto.Nome)).ReturnsAsync(true); // Nome duplicado

            // Act
            var resultado = await _validator.ValidateAsync(produto);

            // Assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Preco_DeveSerMaiorQueZero()
        {
            // Arrange
            var produto = new Produto("Produto", "Descrição", -1);

            // Act
            var resultado = _validator.Validate(produto);

            // Assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Descricao_DeveTerNoMaximo255Caracteres()
        {
            // Arrange
            var produto = new Produto("Produto", new string('a', 256), 50);

            // Act
            var resultado = _validator.Validate(produto);

            // Assert
            resultado.IsValid.Should().BeFalse();
        }
    }
}