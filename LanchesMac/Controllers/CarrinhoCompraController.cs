using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class CarrinhoCompraController : Controller
{
    private readonly ILancheRepository _lancheRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoCompraController(ILancheRepository lancheRepository,
        CarrinhoCompra carrinhoCompra)
    {
        _lancheRepository = lancheRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Index()
    {
        var itens = _carrinhoCompra.GetCarrinhoCompraItens();

        _carrinhoCompra.CarrinhoCompraItems = itens;

        var carrinhoCompraVM = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraVM);
    }

    [Authorize]
    public IActionResult AdicionaritemNoCarrinhoCompra(int lancheId)
    {
        ExecutarComando(AdicionarItem, lancheId);

        //Ao invés de duplicar código, é melhor delegar com Action, neste caso.
        //var lancheSelecionado = _lancheRepository.Lanches
        //                        .FirstOrDefault(p => p.LancheId == lancheId);

        //if(lancheSelecionado != null)
        //{
        //    _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
        //}

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
    {
        ExecutarComando(RemoverItem, lancheId);

        //Ao invés de duplicar código, é melhor delegar com Action, neste caso.
        //var lancheSelecionado = _lancheRepository.Lanches
        //                        .FirstOrDefault(p => p.LancheId == lancheId);

        //if (lancheSelecionado != null)
        //{
        //    _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
        //}

        return RedirectToAction(nameof(Index));
    }

    private void ExecutarComando(Action<Lanche> PersistirBD, int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches
                        .FirstOrDefault(p => p.LancheId == lancheId);

        if (lancheSelecionado != null)
        {
            PersistirBD(lancheSelecionado);
        }

        //return RedirectToAction(nameof(Index));
    }

    public void RemoverItem(Lanche lancheSelecionado)
    {
        _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
    }

    public void AdicionarItem(Lanche lancheSelecionado)
    {
        _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
    }
}
