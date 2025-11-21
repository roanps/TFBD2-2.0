using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;

public class AeronaveRepository : IAeronaveRepository
{
    private readonly VoeMaisDbContext _context;
    public AeronaveRepository(VoeMaisDbContext context)
    {
        _context = context;
    }

    public async Task<List<Aeronave>> GetAll()
    {
        return await _context.Aeronaves
            .Include(a => a.EmpresaAerea)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Aeronave?> GetById(int id)
    {
        return await _context.Aeronaves
            .Include(a => a.EmpresaAerea)
            .FirstOrDefaultAsync(a => a.IdAeronave == id);
    }

    public async Task Create(Aeronave item)
    {
        _context.Aeronaves.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Aeronave item)
    {
        _context.Aeronaves.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Aeronave item)
    {
        _context.Aeronaves.Remove(item);
        await _context.SaveChangesAsync();
    }
}
