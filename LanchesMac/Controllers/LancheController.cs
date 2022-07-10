﻿using LanchesMac.Models;
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
            string categoriaAtual = categoria ?? string.Empty;

            lanches = _lancheRepository.Lanches;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = lanches.OrderBy(x => x.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = lanches
                    .Where(lanche => lanche.Categoria.CategoriaNome == categoria)
                    .OrderBy(x => x.Nome);
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
