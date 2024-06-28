
using Products.DTOs;

namespace Products.Services;

public class ProdutoService
{

    private static readonly List<ProdutoDTO> produtos = new List<ProdutoDTO>() {
        new ProdutoDTO{ Id = "1", Nome = "Produto 1", Preco = 10.99, Categoria = "Categoria 1" }
    };

    public List<ProdutoDTO> GetAllProdutos()
    {
        return produtos.ToList();
    }

    public ProdutoDTO GetProdutoById(string id)
    {
        return produtos.FirstOrDefault(p => p.Id == id);
    }

    public void DeleteProduto(string id)
    {
        int productIndex = produtos.FindIndex(p => p.Id == id);
        if (productIndex != -1)
        {
            produtos.RemoveAt(productIndex);
        }
    }

    public void UpdateProduto(string id, ProdutoDTO updatedProduct) {
        int productIndex = produtos.FindIndex(p => p.Id == id);

        if(productIndex != -1) {
            produtos[productIndex] = updatedProduct;
        }
    }

    public ProdutoDTO CreateProduto(ProdutoDTO newProduct)
        {
            newProduct.Id = Guid.NewGuid().ToString();
            produtos.Add(newProduct);
            return newProduct;
        }
}
