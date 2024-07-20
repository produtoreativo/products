using Microsoft.AspNetCore.Mvc;
using Produtos.DTOs;
using Produtos.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produtos.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutosController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve all products", Description = "Gets a list of all products.")]
        [SwaggerResponse(200, "Success", typeof(List<ProdutoDto>))]
        public async Task<ActionResult<List<ProdutoDto>>> GetAllProdutos()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [SwaggerOperation(Summary = "Retrieve a specific product by ID", Description = "Gets a single product by ID.")]
        [SwaggerResponse(200, "Success", typeof(ProdutoDto))]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<ProdutoDto>> GetProduto(string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return BadRequest("Id do produto inválido");
            }

            var produto = await _produtoService.GetProdutoByIdAsync(guid);
            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            return Ok(produto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new product", Description = "Creates a new product.")]
        [SwaggerResponse(201, "Created", typeof(ProdutoDto))]
        [SwaggerResponse(400, "Bad Request")]
        public async Task<ActionResult<ProdutoDto>> CreateProduto([FromBody] ProdutoDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoProduto = await _produtoService.CreateProdutoAsync(product);

            return CreatedAtRoute("GetProduct", new { id = novoProduto.Id }, novoProduto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing product", Description = "Updates an existing product.")]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> UpdateProduto(string id, [FromBody] ProdutoDto product)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return BadRequest("Id do produto inválido");
            }

            if (product == null)
            {
                return BadRequest("Dados do produto ausentes");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produtoExistente = await _produtoService.GetProdutoByIdAsync(guid);
            if (produtoExistente == null)
            {
                return NotFound("Produto não encontrado");
            }

            await _produtoService.UpdateProdutoAsync(guid, product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a product", Description = "Deletes a product by ID.")]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> DeleteProduto(string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return BadRequest("Id do produto inválido");
            }

            var produto = await _produtoService.GetProdutoByIdAsync(guid);
            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            await _produtoService.DeleteProdutoAsync(guid);

            return NoContent();
        }
    }
}
