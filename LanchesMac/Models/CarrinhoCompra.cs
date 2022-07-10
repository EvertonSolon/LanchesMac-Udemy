using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;
public class CarrinhoCompra
{
    private readonly AppDbContext _context;

    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

    public static CarrinhoCompra GetCarrinho(IServiceProvider serviceProvider) 
    { 
        //Define uma sessão
        ISession session = 
            serviceProvider.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;

        //Obtém um serviço do tipo do contexto
        var context = serviceProvider.GetService<AppDbContext>();

        //Obtém ou gera o Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ??
            Guid.NewGuid().ToString();

        //Atribui o Id do carrinho na sessão
        session.SetString("CarrinhoId", carrinhoId);

        //Retorna o carrinho com o contexto e o Id atribuído ou obtido

        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    } 

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        CarrinhoCompraItem carrinhoCompraItem = VerificarCarrinhoCompraLanche(lanche);

        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };

            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }

        _context.SaveChanges();
    }

    private CarrinhoCompraItem VerificarCarrinhoCompraLanche(Lanche lanche)
    {
        var carrinhoCompraItens = _context.CarrinhoCompraItens.SingleOrDefault(s =>
                                    s.Lanche.LancheId == lanche.LancheId &&
                                    s.CarrinhoCompraId == CarrinhoCompraId);

        return carrinhoCompraItens;
    }

    public int RemoverDoCarrinho(Lanche lanche)
    {
        CarrinhoCompraItem carrinhoCompraItem = VerificarCarrinhoCompraLanche(lanche);

        var quantidadeLocal = 0;

        if(carrinhoCompraItem != null)
        {
            if(carrinhoCompraItem.Quantidade > 1)
            {
                quantidadeLocal = carrinhoCompraItem.Quantidade--;
            }
            else
            {
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }
        }

        _context.SaveChanges();
        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
    {
            return CarrinhoCompraItems ?? 
            (CarrinhoCompraItems = GetCarrinhoCompraItemsPorCarrinhoCompraId()
                                    .Include(s => s.Lanche)
                                    .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = GetCarrinhoCompraItemsPorCarrinhoCompraId();
                            //.ToList();

        _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = GetCarrinhoCompraItemsPorCarrinhoCompraId()
                    .Select(c => c.Lanche.Preco * c.Quantidade)
                    .Sum();

        return total;
    }

    private IQueryable<CarrinhoCompraItem> GetCarrinhoCompraItemsPorCarrinhoCompraId()
    {
        var carrinhoCompraItens = _context.CarrinhoCompraItens
                            .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

        return carrinhoCompraItens;
    }
}
