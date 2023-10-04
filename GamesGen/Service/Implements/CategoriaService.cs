using GamesGen.Data;
using GamesGen.Model;
using Microsoft.EntityFrameworkCore;

namespace GamesGen.Service.Implements
{
    public class CategoriaService : ICategoriaService
    {

        private readonly AppDbContext _context;
        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<Categoria> GetById(long id)
        {
            try
            {
                var categora = await _context.Categoria
                  .Include(t => t.Produto)
                  .FirstAsync(i => i.Id == id);

                return categora;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Categoria>> GetByName(string nome)
        {
            var tema = await _context.Categoria
                 .Include(t => t.Produto)
                 .Where(p => p.Nome.Contains(nome))
                 .ToListAsync();
            return tema;
        }

        public async Task<Categoria?> Create(Categoria categoria)
        {
            await _context.Categoria.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<Categoria> Update(Categoria categoria)
        {
            var categoriaUpdate = await _context.Categoria.FindAsync(categoria);

            if (categoriaUpdate is null)
            {
                return null;
            }
            _context.Entry(categoriaUpdate).State = EntityState.Detached;
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task Delete(Categoria categoria)
        {
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
