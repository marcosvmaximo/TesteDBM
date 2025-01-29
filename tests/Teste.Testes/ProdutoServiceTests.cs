using FluentAssertions;
using Moq;
using Teste.Application;
using Teste.Core.Notifications;
using Teste.Domain;
using Teste.Domain.Interfaces;

namespace Teste.Testes
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly Mock<INotifiable> _notifiableMock;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _notifiableMock = new Mock<INotifiable>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _notifiableMock.Object);
        }

        [Fact]
        public async Task AdicionarProduto_DeveAdicionarProdutoQuandoValido()
        {
            // Arrange
            var produto = new Produto("Produto", "Descrição", 50);
            _produtoRepositoryMock.Setup(r => r.AddAsync(produto)).Returns(Task.CompletedTask);

            // Act
            await _produtoService.AdicionarProduto("Produto", "Descrição", 50);

            // Assert
            _produtoRepositoryMock.Verify(r => r.AddAsync(produto), Times.Once);
        }

        [Fact]
        public async Task AdicionarProduto_NaoDeveAdicionarQuandoInvalido()
        {
            // Arrange
            var produto = new Produto(null, "Descrição", 50); // Nome inválido
            _produtoRepositoryMock.Setup(r => r.AddAsync(produto)).Returns(Task.CompletedTask);

            // Act
            await _produtoService.AdicionarProduto(null, "Descrição", 50);

            // Assert
            _produtoRepositoryMock.Verify(r => r.AddAsync(produto), Times.Never); // O repositório não deve ser chamado
        }

        [Fact]
        public async Task AtualizarProduto_DeveAtualizarQuandoValido()
        {
            // Arrange
            var produtoExistente = new Produto("Produto Existente", "Descrição", 50) { Id = 1 };
            _produtoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produtoExistente);
            _produtoRepositoryMock.Setup(r => r.UpdateAsync(produtoExistente)).Returns(Task.CompletedTask);

            // Act
            await _produtoService.AtualizarProduto(1, "Produto Atualizado", "Descrição Atualizada", 60);

            // Assert
            _produtoRepositoryMock.Verify(r => r.UpdateAsync(produtoExistente), Times.Once);
        }

        [Fact]
        public async Task AtualizarProduto_NaoDeveAtualizarQuandoInvalido()
        {
            // Arrange
            var produtoExistente = new Produto("Produto Existente", "Descrição", 50) { Id = 1 };
            _produtoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produtoExistente);
            _produtoRepositoryMock.Setup(r => r.UpdateAsync(produtoExistente)).Returns(Task.CompletedTask);

            // Act
            await _produtoService.AtualizarProduto(1, null, "Descrição Atualizada", 60); // Nome inválido

            // Assert
            _produtoRepositoryMock.Verify(r => r.UpdateAsync(produtoExistente), Times.Never);
        }

        [Fact]
        public async Task RemoverProduto_DeveRemoverQuandoExistir()
        {
            // Arrange
            var produtoExistente = new Produto("Produto Existente", "Descrição", 50) { Id = 1 };
            _produtoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produtoExistente);
            _produtoRepositoryMock.Setup(r => r.DeleteAsync(produtoExistente)).Returns(Task.CompletedTask);

            // Act
            await _produtoService.RemoverProduto(1);

            // Assert
            _produtoRepositoryMock.Verify(r => r.DeleteAsync(produtoExistente), Times.Once);
        }

        [Fact]
        public async Task RemoverProduto_NaoDeveRemoverQuandoNaoExistir()
        {
            // Arrange
            _produtoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Produto)null);

            // Act
            await _produtoService.RemoverProduto(1);

            // Assert
            _produtoRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Produto>()), Times.Never);
        }
    }
}