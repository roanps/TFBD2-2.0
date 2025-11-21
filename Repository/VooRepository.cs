using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;
public class VooRepository : IVooRepository
{
    private readonly VoeMaisDbContext _context;
    public VooRepository(VoeMaisDbContext context)
    {
        _context = context;
    }

    public async Task<List<Voo>> GetAll()
    {
        return await _context.Voos
            .Include(v => v.Aeronave)
            .Include(v => v.Origem)
            .Include(v => v.Destino)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Voo?> GetById(int id)
    {
        return await _context.Voos
            .Include(v => v.Aeronave)
            .Include(v => v.Origem)
            .Include(v => v.Destino)
            .FirstOrDefaultAsync(v => v.IdVoo == id);
    }

    public async Task Create(Voo item)
    {
        _context.Voos.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Voo item)
    {
        _context.Voos.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Voo item)
    {
        _context.Voos.Remove(item);
        await _context.SaveChangesAsync();
    }
}
