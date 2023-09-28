using GamesGen.Model;

namespace GamesGen.Service
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(long id);
        Task<IEnumerable<Produto>> GetByName(string nome);
        Task<Produto?> Create(Produto produto);
        Task<Produto> Update(Produto produto);
        Task Delete(Produto produto);

    }
}
