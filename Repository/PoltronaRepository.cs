using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;
public class PoltronaRepository : IPoltronaRepository
{
    private readonly VoeMaisDbContext _context;
    public PoltronaRepository(VoeMaisDbContext context)
    {
        _context = context;
    }

    public async Task<List<Poltrona>> GetAll()
    {
        return await _context.Poltronas
            .Include(p => p.Voo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Poltrona?> GetById(int idVoo, string numeroPoltrona)
    {
        return await _context.Poltronas
            .Include(p => p.Voo)
            .FirstOrDefaultAsync(p => p.IdVoo == idVoo && p.NumeroPoltrona == numeroPoltrona);
    }

    public async Task Create(Poltrona item)
    {
        _context.Poltronas.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Poltrona item)
    {
        _context.Poltronas.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Poltrona item)
    {
        _context.Poltronas.Remove(item);
        await _context.SaveChangesAsync();
    }
}
