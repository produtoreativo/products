using Produtos.DTOs;

namespace Produtos.Repositories
{
    public interface IProdutoRepository
    {
        Task<List<ProdutoDto>> GetAll();
        Task<ProdutoDto> GetById(Guid id);
        Task Save(ProdutoDto produto);
        Task Update(ProdutoDto produto);
        Task Delete(Guid id);
    }
}
