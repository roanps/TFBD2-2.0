using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;
public class PassagemRepository : IPassagemRepository
{
    private readonly VoeMaisDbContext _context;
    public PassagemRepository(VoeMaisDbContext context)
    {
        _context = context;
    }

    public async Task<List<Passagem>> GetAll()
    {
        return await _context.Passagens
            .Include(p => p.Passageiro)
            .Include(p => p.Voo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Passagem?> GetById(int idPassageiro, int idVoo)
    {
        return await _context.Passagens
            .Include(p => p.Passageiro)
            .Include(p => p.Voo)
            .FirstOrDefaultAsync(p => p.IdPassageiro == idPassageiro && p.IdVoo == idVoo);
    }

    public async Task Create(Passagem item)
    {
        _context.Passagens.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Passagem item)
    {
        _context.Passagens.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Passagem item)
    {
        _context.Passagens.Remove(item);
        await _context.SaveChangesAsync();
    }
}
