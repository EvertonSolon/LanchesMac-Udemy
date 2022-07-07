using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    // => é uma expressão lambda
    // E a linha debaixo é chamada de Expression Bodied Member, para tornar o código mais conciso
    public IEnumerable<Categoria> Categorias => _context.Categorias;

    public CategoriaRepository(AppDbContext contexto)
    {
        _context = contexto;
    }
    
}