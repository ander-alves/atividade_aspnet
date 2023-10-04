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
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetById(long id)
        {
            try
            {
                var produto = await _context.Produtos
                  .Include(t => t.Id)
                  .Include(p => p.Usuario)
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
            var contemNome = await _context.Produtos
                 .Include(t => t.Nome)
                 .Include(p => p.Usuario)
                 .Where(p => p.Nome.Contains(nome))
                 .ToListAsync();
            return contemNome;
        }

        public async Task<Produto?> Create(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var buscaCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (buscaCategoria is null)
                    return null;

                produto.Categoria = buscaCategoria;

            }
            produto.Usuario = produto.Usuario is not null ? await _context.Users.FirstOrDefaultAsync(u => u.Id == produto.Usuario.Id) : null;

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> Update(Produto produto)
        {
            var produtoUpdate = await _context.Produtos.FindAsync(produto);

            if (produtoUpdate is null)
            {
                return null;
            }

            produto.Usuario = produto.Usuario is not null ? await _context.Users.FirstOrDefaultAsync(u => u.Id == produto.Usuario.Id) : null;

            _context.Entry(produtoUpdate).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}
