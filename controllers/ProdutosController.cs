using Microsoft.AspNetCore.Mvc;
using Products.DTOs;
using Products.Services;

namespace Products.controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;

    public ProdutosController(ProdutoService produtoService) {
        _produtoService = produtoService;
    }

    [HttpGet]
    public ActionResult<List<ProdutoDTO>> GetAllProdutos()
    {
        var produtosMockados = _produtoService.GetAllProdutos();
        return Ok(produtosMockados);
    }
    [HttpGet("{id}", Name = "GetProduct")]
    public ActionResult<ProdutoDTO> GetProduto(string id)
    {
        var produto = _produtoService.GetProdutoById(id);
        if (produto == null)
        {
            return NotFound("Produto não encontrado");
        }

        return Ok(produto);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduto(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Id do produto inválido");
        }

        var produto = _produtoService.GetProdutoById(id);
        if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

        _produtoService.DeleteProduto(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateProduto(string id, ProdutoDTO product)
    {
        if (string.IsNullOrEmpty(id) || product == null)
        {
            return BadRequest("Id do produto inválido ou dados do produto faltando");
        }

          var produtoExistente = _produtoService.GetProdutoById(id);
        if (produtoExistente == null)
        {
            return NotFound("Prodto não encontrado");
        }

        _produtoService.UpdateProduto(id, product);

        return NoContent();
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> CreateProduto(ProdutoDTO product)
    {
        if (product == null)
        {
            return BadRequest("Dados do produto ausentes");
        }

          var novoProduto = _produtoService.CreateProduto(product);

        return CreatedAtRoute("GetProduct", new { id = product.Id }, novoProduto);
    }
}
