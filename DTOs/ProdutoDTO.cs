using System;
using System.ComponentModel.DataAnnotations;

namespace Produtos.DTOs
{
    public class ProdutoDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        [StringLength(50, ErrorMessage = "A categoria deve ter no máximo 50 caracteres")]
        public string Categoria { get; set; }
    }
}
