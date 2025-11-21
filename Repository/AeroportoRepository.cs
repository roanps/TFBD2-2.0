using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;
public class AeroportoRepository : IAeroportoRepository
{
    private readonly VoeMaisDbContext _context;
    public AeroportoRepository(VoeMaisDbContext context)
    {
        _context = context;
    }

    public async Task<List<Aeroporto>> GetAll()
    {
        return await _context.Aeroportos.AsNoTracking().ToListAsync();
    }

    public async Task<Aeroporto?> GetById(int id)
    {
        return await _context.Aeroportos.FindAsync(id);
    }

    public async Task Create(Aeroporto item)
    {
        _context.Aeroportos.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Aeroporto item)
    {
        _context.Aeroportos.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Aeroporto item)
    {
        _context.Aeroportos.Remove(item);
        await _context.SaveChangesAsync();
    }
}
