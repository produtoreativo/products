using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Produtos.DTOs;
using Produtos.Entities;
using Produtos.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produtos.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoContext _context;
        private readonly IMapper _mapper;

        public ProdutoRepository(ProdutoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProdutoDto>> GetAll()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            return _mapper.Map<List<ProdutoDto>>(produtos);
        }

        public async Task<ProdutoDto> GetById(Guid id)
        {
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task Save(ProdutoDto produto)
        {
            var produtoEntity = _mapper.Map<Produto>(produto);
            await _context.Produtos.AddAsync(produtoEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProdutoDto produto)
        {
            var existingEntity = await _context.Produtos.FindAsync(produto.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            var produtoEntity = _mapper.Map<Produto>(produto);
            _context.Entry(produtoEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
