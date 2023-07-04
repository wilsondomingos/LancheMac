using LanchesMac.Models;

namespace LanchesMac.Repositories.Interfaces
{
    public interface IcategoriaRepository
    {
        IEnumerable<Categoria>Categorias { get; }
    }
}
