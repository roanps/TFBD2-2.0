using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;
public class VooEscalaRepository : IVooEscalaRepository
{
    private readonly VoeMaisDbContext _context;
    public VooEscalaRepository(VoeMaisDbContext context)
    {
        _context = context;
    }

    public async Task<List<VooEscala>> GetAll()
    {
        return await _context.VooEscalas
            .Include(ve => ve.Voo)
            .Include(ve => ve.Escala)
                .ThenInclude(e => e.AeroportoEscala)   
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<VooEscala?> GetById(int idVoo, int idEscala)
    {
        return await _context.VooEscalas
            .Include(ve => ve.Voo)
            .Include(ve => ve.Escala)
                .ThenInclude(e => e.AeroportoEscala)  
            .FirstOrDefaultAsync(ve => ve.IdVoo == idVoo && ve.IdEscala == idEscala);
    }

    public async Task Create(VooEscala item)
    {
        _context.VooEscalas.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(VooEscala item)
    {
        _context.VooEscalas.Remove(item);
        await _context.SaveChangesAsync();
    }
}
