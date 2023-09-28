using GamesGen.Model;

namespace GamesGen.Service
{
    public interface ICategoriaService

    {
        Task<IEnumerable<Categoria>> GetAll();
        Task<Categoria> GetById(long id);
        Task<IEnumerable<Categoria>> GetByName(string nome);
        Task<Categoria?> Create(Categoria categoria);
        Task<Categoria> Update(Categoria categoria);
        Task Delete(Categoria categoria);

    }
}
