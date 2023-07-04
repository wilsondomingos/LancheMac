using LanchesMac.Models;
using System.Collections.Generic;

namespace LanchesMac.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPerferidos { get; }

        Lanche GetLancheById (int lancheId);

    }
}
