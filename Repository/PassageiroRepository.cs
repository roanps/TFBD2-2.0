using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;
public class PassageiroRepository : IPassageiroRepository
{
    private readonly VoeMaisDbContext _context;
    public PassageiroRepository(VoeMaisDbContext context)
    {
        _context = context;
    }

    public async Task<List<Passageiro>> GetAll()
    {
        return await _context.Passageiros.AsNoTracking().ToListAsync();
    }

    public async Task<Passageiro?> GetById(int id)
    {
        return await _context.Passageiros.FindAsync(id);
    }

    public async Task Create(Passageiro item)
    {
        _context.Passageiros.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Passageiro item)
    {
        _context.Passageiros.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Passageiro item)
    {
        _context.Passageiros.Remove(item);
        await _context.SaveChangesAsync();
    }
}
