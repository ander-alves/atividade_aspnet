using GamesGen.Data;
using GamesGen.Model;
using Microsoft.EntityFrameworkCore;

namespace GamesGen.Service.Implements
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produto.ToListAsync();
        }

        public async Task<Produto> GetById(long id)
        {
            try
            {
                var produto = await _context.Produto
                  .Include(t => t.Id)
                  .FirstAsync(i => i.Id == id);

                return produto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Produto>> GetByName(string nome)
        {
            var contemNome = await _context.Produto
                 .Include(t => t.Nome)
                 .Where(p => p.Nome.Contains(nome))
                 .ToListAsync();
            return contemNome;
        }

        public async Task<Produto?> Create(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var buscaCategoria = await _context.Categoria.FindAsync(produto.Categoria.Id);

                if (buscaCategoria is null)
                    return null;

                produto.Categoria = buscaCategoria;

            }

            await _context.Produto.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> Update(Produto produto)
        {
            var produtoUpdate = await _context.Produto.FindAsync(produto);

            if (produtoUpdate is null)
            {
                return null;
            }
            _context.Entry(produtoUpdate).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}
