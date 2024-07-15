using Produtos.DTOs;
using Produtos.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produtos.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Task<List<ProdutoDto>> GetAllProdutosAsync()
        {
            return _produtoRepository.GetAll();
        }

        public Task<ProdutoDto> GetProdutoByIdAsync(Guid id)
        {
            return _produtoRepository.GetById(id);
        }

        public Task DeleteProdutoAsync(Guid id)
        {
            return _produtoRepository.Delete(id);
        }

        public Task UpdateProdutoAsync(Guid id, ProdutoDto updatedProduct)
        {
            updatedProduct.Id = id;
            return _produtoRepository.Update(updatedProduct);
        }

        public Task<ProdutoDto> CreateProdutoAsync(ProdutoDto newProduct)
        {
            newProduct.Id = Guid.NewGuid();
            return _produtoRepository.Save(newProduct).ContinueWith(_ => newProduct);
        }
    }
}
