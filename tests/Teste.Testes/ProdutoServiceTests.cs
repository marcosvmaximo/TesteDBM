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
        public async Task AdicionarProduto_DeveAdicionar_QuandoProdutoForValido()
        {
            // Arrange
            _produtoRepositoryMock.Setup(r => r.ObterProdutoPorNome(It.IsAny<string>())).ReturnsAsync((Produto)null);

            // Act
            await _produtoService.AdicionarProduto("Produto", "Descrição", 50);

            // Assert
            _produtoRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task AdicionarProduto_NaoDeveAdicionar_QuandoNomeDuplicado()
        {
            // Arrange
            var produtoExistente = new Produto("Produto", "Descrição", 50);
            _produtoRepositoryMock.Setup(r => r.ObterProdutoPorNome("Produto")).ReturnsAsync(produtoExistente);

            // Act
            await _produtoService.AdicionarProduto("Produto", "Descrição", 50);

            // Assert
            _produtoRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Produto>()), Times.Never);
        }

        [Fact]
        public async Task AdicionarProduto_NaoDeveAdicionar_QuandoProdutoForInvalido()
        {
            // Act
            await _produtoService.AdicionarProduto("", "Descrição", 50);

            // Assert
            _produtoRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Produto>()), Times.Never);
        }

        [Fact]
        public async Task AtualizarProduto_DeveAtualizar_QuandoProdutoForValido()
        {
            // Arrange
            var produto = new Produto("Produto", "Descrição", 50) { Id = 1 };
            _produtoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produto);

            // Act
            await _produtoService.AtualizarProduto(1, "Novo Produto", "Nova Descrição", 60);

            // Assert
            _produtoRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarProduto_NaoDeveAtualizar_QuandoProdutoNaoExistir()
        {
            // Arrange
            _produtoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Produto)null);

            // Act
            await _produtoService.AtualizarProduto(1, "Novo Produto", "Nova Descrição", 60);

            // Assert
            _produtoRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Produto>()), Times.Never);
        }

        [Fact]
        public async Task RemoverProduto_DeveRemover_QuandoProdutoExistir()
        {
            // Arrange
            var produto = new Produto("Produto", "Descrição", 50) { Id = 1 };
            _produtoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produto);

            // Act
            await _produtoService.RemoverProduto(1);

            _produtoRepositoryMock.Verify(r => r.DeleteAsync(produto), Times.Once);
        }

        [Fact]
        public async Task RemoverProduto_NaoDeveRemover_QuandoProdutoNaoExistir()
        {
            // Arrange
            _produtoRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Produto)null);

            await _produtoService.RemoverProduto(1);

            // Assert
            _produtoRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Produto>()), Times.Never);
        }

        [Fact]
        public async Task VerificarNomeUnico_DeveRetornarTrue_QuandoNomeNaoExistir()
        {
            _produtoRepositoryMock.Setup(r => r.ObterProdutoPorNome("Novo Produto")).ReturnsAsync((Produto)null);

            var resultado = await _produtoService.VerificarNomeUnico("Novo Produto");

            resultado.Should().BeTrue();
        }

        [Fact]
        public async Task VerificarNomeUnico_DeveRetornarFalse_QuandoNomeExistir()
        {
            _produtoRepositoryMock.Setup(r => r.ObterProdutoPorNome("Produto Existente"))
                .ReturnsAsync(new Produto("Produto Existente", "Descrição", 50));

            var resultado = await _produtoService.VerificarNomeUnico("Produto Existente");

            resultado.Should().BeFalse();
        }
    }
}