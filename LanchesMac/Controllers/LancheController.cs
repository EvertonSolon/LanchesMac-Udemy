using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches = null;
            string categoriaAtual = string.Empty;

            lanches = _lancheRepository.Lanches.OrderBy(x => x.LancheId);

            if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
            {
                lanches = lanches
                    .Where(lanche => lanche.Categoria.CategoriaNome
                    .Equals("Normal"))
                    .OrderBy(l => l.Nome);
                categoriaAtual = categoria;
            }
            else if(string.Equals("Natural", categoria, StringComparison.OrdinalIgnoreCase))
            {
                lanches = lanches
                   .Where(lanche => lanche.Categoria.CategoriaNome
                   .Equals("Natural"))
                   .OrderBy(l => l.Nome);
                categoriaAtual = categoria;
            }
            else
            {
                lanches = lanches.OrderBy(x => x.LancheId);
                categoriaAtual = "Todos os lanches";
            }

            var lancheListVm = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lancheListVm);
        }
    }
}
