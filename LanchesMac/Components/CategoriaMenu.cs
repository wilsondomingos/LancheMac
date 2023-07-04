using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly IcategoriaRepository _icategoriaRepository;

        public CategoriaMenu(IcategoriaRepository icategoriaRepository)
        {
            _icategoriaRepository = icategoriaRepository;
        }

        public IViewComponentResult Invoke()
        {

            var Categorias = _icategoriaRepository.Categorias.OrderBy(c => c.CategoriaNome);
            return View(Categorias);
        }
    }
}
