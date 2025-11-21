using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;
public class EscalaRepository : IEscalaRepository
{
    private readonly VoeMaisDbContext _context;
    public EscalaRepository(VoeMaisDbContext context)
    {
        _context = context;
    }

    public async Task<List<Escala>> GetAll()
    {
        return await _context.Escalas
            .Include(e => e.AeroportoEscala)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Escala?> GetById(int id)
    {
        return await _context.Escalas
            .Include(e => e.AeroportoEscala)
            .FirstOrDefaultAsync(e => e.IdEscala == id);
    }

    public async Task Create(Escala item)
    {
        _context.Escalas.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Escala item)
    {
        _context.Escalas.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Escala item)
    {
        _context.Escalas.Remove(item);
        await _context.SaveChangesAsync();
    }
}
