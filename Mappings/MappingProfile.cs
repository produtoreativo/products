using AutoMapper;
using Produtos.DTOs;
using Produtos.Entities;

namespace Produtos.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProdutoDto, Produto>().ReverseMap();
        }
    }
}
